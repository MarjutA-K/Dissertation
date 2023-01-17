using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlantItemTemplate : MonoBehaviour
{
    public ShopPlantItemSO plant;

    public TMP_Text titleTxt;
    public TMP_Text priceTxt;
    public Image icon;

    public Image btnImage;
    public TMP_Text btnTxt;

    private bool interactable;
    private bool isOpen = false;

    int level;

    private Color lockedColor = Color.gray;
    private Color buyColor = Color.green;

    GardenManager gm;
    XPManager xpm;

    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GardenManager>();
        xpm = FindObjectOfType<XPManager>();
        InitializeUI();
        plant.isLocked = true;
    }

    private void Update()
    {
        if(plant.level <= xpm.level)
        {
            plant.isLocked = false;

            if(!isOpen)
            {
                callFunction();
            }
        }

        if(plant.isLocked)
        {
            Locked();
        }
        else
        {
            InitializeUI();
        }
       
    }

    void callFunction()
    {
        isOpen = true;
        btnImage.color = buyColor;
        btnTxt.text = "Buy";
    }

    public void BuyPlant()
    {     
       if(interactable)
        {
            gm.SelectPlant(this);
            Debug.Log("Bought " + plant.plantTitle);
        }
    }

    void Locked()
    {
        btnImage.GetComponent<Selectable>().interactable = false;
        btnImage.color = lockedColor;
        btnTxt.text = "Item locked";
    }

    void InitializeUI()
    {
        titleTxt.text = plant.plantTitle;
        priceTxt.text = "$" + plant.buyPrice;
        icon.sprite = plant.icon;


        if (gm.money < plant.buyPrice)
        {
            btnImage.GetComponent<Selectable>().interactable = false;
            interactable = false;
            btnImage.color = buyColor;
            btnTxt.text = "Buy";
            plant.isVisible = false;
        }
        else
        {
            btnImage.GetComponent<Selectable>().interactable = true;
            interactable = true;
            plant.isVisible = true;
        }
    }

}