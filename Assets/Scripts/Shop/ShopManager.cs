using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    private XPManager _xp;
    public InventoryManager InventoryManager;
    private AchievementManager achievementManager;
    [SerializeField]
    public LoadSave _saveManager;

    public int money;
    public int diamonds;
    public TMP_Text diamondsTxt;
    public TMP_Text moneyTxt;

    public PlantSO[] shopItemsSO;
    public GameObject[] shopPanelsSO;
    public PlantItemTemplate[] shopPanels;
    public Button[] purchasaBtns;

    public GameObject shopObject;
    private bool shopIsActive;

    private void Start()
    {
        _xp = FindObjectOfType<XPManager>();
        achievementManager = FindObjectOfType<AchievementManager>();

        for (int i = 0; i < shopItemsSO.Length; i++)
        {
            shopPanelsSO[i].SetActive(true);
        }

        moneyTxt.text = money.ToString();
        diamondsTxt.text = diamonds.ToString();

        LoadPanels();
        CheckPurchaseable();
        CheckLockedItem();

        shopIsActive = shopObject.activeSelf;
    }

    private void Update()
    {
        CheckPurchaseable();
        CheckLockedItem();
        LoadPanels();

        if (shopIsActive != shopObject.activeSelf)
        {
            shopIsActive = shopObject.activeSelf;
        }
    }

    public void AddMoney(int _money)
    {
        money += _money;
        moneyTxt.text = money.ToString();
        CheckPurchaseable();
        _saveManager.moneyChanged.Invoke(money);
    }

    public void AddDiamonds(int _diamonds)
    {
        diamonds += _diamonds;
        diamondsTxt.text = diamonds.ToString();
        _saveManager.diamondsChanged.Invoke(diamonds);
    }

    public void CheckLockedItem()
    {
        for (int i = 0; i < shopItemsSO.Length; i++)
        {
 
                if (diamonds >= shopItemsSO[i].unlockPrice && _xp.level >= shopItemsSO[i].level && !shopItemsSO[i].unlocked)
                {
                    purchasaBtns[i].interactable = true;
                }
                else if (diamonds <= shopItemsSO[i].unlockPrice && _xp.level <= shopItemsSO[i].level && !shopItemsSO[i].unlocked)
                {
                    purchasaBtns[i].interactable = false;
                }
                    
        }
    }

    public void UnlockItem(int btnNUm)
    {
        if(diamonds >= shopItemsSO[btnNUm].unlockPrice && !shopItemsSO[btnNUm].unlocked)
        {
            shopItemsSO[btnNUm].unlocked = true;
            diamonds = diamonds - shopItemsSO[btnNUm].unlockPrice;
            diamondsTxt.text = diamonds.ToString();
            _saveManager.diamondsChanged.Invoke(diamonds);
        }
    }

    // Check if Item is purchable
   public void CheckPurchaseable()
    {
        for (int i = 0; i < shopItemsSO.Length; i++)
        {
            if (money >= shopItemsSO[i].buyPrice && shopItemsSO[i].unlocked)
            {
                purchasaBtns[i].interactable = true;
            }
            else if (money <= shopItemsSO[i].buyPrice && shopItemsSO[i].unlocked)
            {
                purchasaBtns[i].interactable = false;
            }
        }
    }

    public void PurchaseItem(int btnNo)
    {
        if (money >= shopItemsSO[btnNo].buyPrice && shopItemsSO[btnNo].unlocked)
        {
            money = money - shopItemsSO[btnNo].buyPrice;
            moneyTxt.text = money.ToString();
            //CheckPurchaseable();
            _saveManager.moneyChanged.Invoke(money);
            BoughtPlant(btnNo);
        }
    }

    public void LoadPanels()
    {
        for(int i = 0; i < shopItemsSO.Length; i++)
        {                       

            if (shopItemsSO[i].unlocked)
            {         
                shopPanels[i].priceTxt.text = shopItemsSO[i].buyPrice.ToString();
                shopPanels[i].titleTxt.text = shopItemsSO[i].plantTitle;
                shopPanels[i].icon.sprite = shopItemsSO[i].icon;
                shopPanels[i].stepsToGrow.SetActive(true);
                shopPanels[i].stepsToGrowTxt.gameObject.SetActive(true);
                shopPanels[i].stepsToGrowTxt.text = (shopItemsSO[i].growthSteps * 5).ToString();
                shopPanels[i].currencyIcon.gameObject.SetActive(true);
                shopPanels[i].diamondsIcon.gameObject.SetActive(false);

                achievementManager.UnlockItemsAchievement(1);
            }
            else
            {
                shopPanels[i].titleTxt.text = "Reach level " + shopItemsSO[i].level.ToString();
                shopPanels[i].icon.sprite = shopItemsSO[i].lockIcon;
                shopPanels[i].priceTxt.text = shopItemsSO[i].unlockPrice.ToString();
                shopPanels[i].diamondsIcon.gameObject.SetActive(true);
                shopPanels[i].currencyIcon.gameObject.SetActive(false);
                shopPanels[i].stepsToGrow.SetActive(false);
                shopPanels[i].stepsToGrowTxt.gameObject.SetActive(false);
            }
        }
    }

    // Add item to Inventory
    public void BoughtPlant(int id)
    {
        bool result = InventoryManager.AddItem(shopItemsSO[id]);
        if (result == true)
        {
            Debug.Log("Item added to inventory");
        }
        else
        {
            Debug.Log("ITEM NOT ADDED");
        }
    }

    public void GetSelectedPlant()
    {
        PlantSO receivedVegetable = InventoryManager.GetSelectedPlant(false);
        if (receivedVegetable != null)
        {
            Debug.Log("Received item: " + receivedVegetable);

        }
        else
        {
            Debug.Log("Item NOT received");
        }
    }

    public void UseSelectedPlant()
    {
        PlantSO receivedPlant = InventoryManager.GetSelectedPlant(true);
        if (receivedPlant != null)
        {
            Debug.Log("Used item: " + receivedPlant);
        }
        else
        {
            Debug.Log("Item NOT USED");
        }
    }
}

