
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotManager : MonoBehaviour
{

    public bool isPlanted = false;
    bool isPlaced = false;
    SpriteRenderer plant;
    SpriteRenderer item;
    BoxCollider2D plantCollider;

    int plantStage = 0;
    float timer;

    public Color availableColor = Color.green;
    public Color unavailableColor = Color.red;

    SpriteRenderer plot;

    ShopPlantItemSO selectedPlant;
    ShopItemsSO selectedItem;

    GardenManager gm;
    WOCompleted woc;
    OpenTabs op;

    public bool isDry = true;
    public Sprite drySprite;
    public Sprite normalSprite;
    public Sprite unavailableSprite;

    float speed = 1f;
    public bool isBought = true;

    // Start is called before the first frame update
    void Start()
    {
        plant = transform.GetChild(0).GetComponent<SpriteRenderer>();
        item = transform.GetChild(1).GetComponent<SpriteRenderer>();
        plantCollider = transform.GetChild(0).GetComponent<BoxCollider2D>();
        gm = transform.parent.GetComponent<GardenManager>();
        op = FindObjectOfType<OpenTabs>();
        plot = GetComponent<SpriteRenderer>();
        woc = FindObjectOfType<WOCompleted>();

        if (isBought)
        {
            plot.sprite = drySprite;
        }
        else
        {
            plot.sprite = unavailableSprite;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (woc.isWOCompleted)
        {
            isDry = false;
            UpdateStages();
        }       
    }

    public void UpdateStages()
    {
        if (isPlanted && !isDry)
        {
            timer -= speed * Time.deltaTime;

            if (timer < 0 && plantStage < selectedPlant.plantStages.Length - 1)
            {
                timer = selectedPlant.timeBtwStages;
                plantStage++;
                UpdatePlant();
            }
        }
    }

    private void OnMouseDown()
    {
        // If Item is placed but player wants to remove it
        if(isPlaced)
        {
           if (!gm.isPlacing && !gm.isSelecting)
            {
                RemoveItem();
            }
        }
      
        if (isPlanted)
        {
            if (plantStage == selectedPlant.plantStages.Length - 1 && !gm.isPlanting && !gm.isSelecting)
            {
                Harvest();
            }        
        }
        //Place Plant
        else if (gm.isPlanting && gm.selectPlant.plant.buyPrice <= gm.money && isBought)
        {
            Plant(gm.selectPlant.plant);
        }
        //Place Item
        else if (gm.isPlacing && gm.selectItem.item.buyPrice <= gm.money && isBought)
        {
            Place(gm.selectItem.item);
        }

        if (gm.isSelecting)
        {
            switch (gm.selectedTool)
            {
                case 1:
                    if (isBought)
                    {
                        isDry = false;
                        plot.sprite = normalSprite;
                        if (isPlanted) UpdatePlant();
                    }
                    break;
                case 2:
                    if (gm.money >= 10 && isBought)
                    {
                        gm.Transaction(-10);
                        if (speed < 2) speed += .2f;
                    }
                    break;
                case 3:
                    if (gm.money >= 100 && !isBought)
                    {
                        gm.Transaction(-100);
                        isBought = true;
                        plot.sprite = drySprite;
                    }
                    break;
                default:
                    break;
            }
        }
    }

    private void OnMouseOver()
    {
        if (gm.isPlanting)
        {
            if (isPlanted || gm.selectPlant.plant.buyPrice > gm.money || !isBought)
            {
                //can't buy
                plot.color = unavailableColor;            
            }
            else
            {
                //can buy
                plot.color = availableColor;
            }

            if(!isPlanted)
            {
                // Sets item invisible when player does not have money to buy it
                if (!gm.selectPlant.plant.isVisible || !op.interactable)
                {
                    gm.isPlanting = false;
                    if(gm.selectPlant != null)
                    {
                        gm.selectPlant.btnImage.color = gm.buyColor;
                        gm.selectPlant.btnTxt.text = "Buy";
                        gm.selectPlant = null;
                    }
                }
            }

        }

        // Able to place Items 
        if (gm.isPlacing)
        {
            if (isPlanted || gm.selectItem.item.buyPrice > gm.money || isBought)
            {
                //can buy
                plot.color = availableColor;
                if(!isPlaced)
                {
                    ShowItem(gm.selectItem.item);

                    // Sets item invisible when player does not have money to buy it
                    if (!gm.selectItem.item.isVisible || !op.interactable)
                    {
                        gm.isPlacing = false;
                        if(gm.selectItem != null)
                        {
                            gm.selectItem.btnImage.color = gm.buyColor;
                            gm.selectItem.btnTxt.text = "Buy";
                            gm.selectItem = null;
                        }

                    }        
                }          
            }
            else
            {
                //can't buy
                plot.color = unavailableColor;
            }
        }

        if (gm.isSelecting)
        {
            switch (gm.selectedTool)
            {
                case 1:
                case 2:
                    if (isBought && gm.money >= (gm.selectedTool - 1) * 10)
                    {
                        plot.color = availableColor;
                    }
                    else
                    {
                        plot.color = unavailableColor;
                    }
                    break;
                case 3:
                    if (!isBought && gm.money >= 100)
                    {
                        plot.color = availableColor;
                    }
                    else
                    {
                        plot.color = unavailableColor;
                    }
                    break;
                default:
                    plot.color = unavailableColor;
                    break;
            }
        }
    }

    private void OnMouseExit()
    {
        plot.color = Color.white;

        if(!isPlaced)
        {
            HideItem();
        }
    }

    private void Harvest()
    {
        isPlanted = false;
        plant.gameObject.SetActive(false);
        gm.Transaction(selectedPlant.sellPrice);
        isDry = true;
        plot.sprite = drySprite;
        speed = 1f;
        XPManager.instance.AddXP(10);
    }

    private void RemoveItem()
    {
        isPlaced = false;
        isPlanted = false;
        item.gameObject.SetActive(false);
        gm.Transaction(selectedItem.sellPrice);
    }

    private void Plant(ShopPlantItemSO newPlant)
    {
        selectedPlant = newPlant;
        isPlanted = true;
        gm.Transaction(-selectedPlant.buyPrice);
        plot.sprite = normalSprite;

        plantStage = 0;
        UpdatePlant();
        timer = selectedPlant.timeBtwStages;
        plant.gameObject.SetActive(true);
    }

    private void Place(ShopItemsSO newItem)
    {      
        selectedItem = newItem;

        isPlanted = true;
        isPlaced = true;
        gm.Transaction(-selectedItem.buyPrice);
        UpdateItem();
        item.gameObject.SetActive(true);
    }

    //Show Item in the map before it is placed
    private void ShowItem(ShopItemsSO newItem)
    {
        selectedItem = newItem;

        UpdateItem();
        item.gameObject.SetActive(true);
    }

    //Hide item from each plot when mouse it is not over it
    public void HideItem()
    {
        item.gameObject.SetActive(false);
    }

    private void UpdatePlant()
    {
        if (isDry)
        {
            plant.sprite = selectedPlant.dryPlanted;
        }
        else
        {
            plant.sprite = selectedPlant.plantStages[plantStage];
        }
        plantCollider.size = plant.sprite.bounds.size;
        plantCollider.offset = new Vector2(0, plant.bounds.size.y / 2);
    }

    private void UpdateItem()
    {
        item.sprite = selectedItem.icon;
    }
}