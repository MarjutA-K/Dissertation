using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class ShopManager : MonoBehaviour
{
    public int coins;
    public TMP_Text coinsTxt;
    public ShopPlantItemSO[] shopItemsSO;
    public GameObject[] shopPanelsSO;
    public PlantItemTemplate[] shopPanels;
    public Button[] purchasaBtns;

    public InventoryManager InventoryManager;

    private void Start()
    {
        for (int i = 0; i < shopItemsSO.Length; i++)
        {
            shopPanelsSO[i].SetActive(true);
        }

        coinsTxt.text = "Coins: " + coins.ToString();
        LoadPanels();
        CheckPurchaseable();
    }

    public void AddCoins()
    {
        coins++;
        coinsTxt.text = "Coins: " + coins.ToString();
        CheckPurchaseable();
    }

   public void CheckPurchaseable()
    {
        for (int i = 0; i < shopItemsSO.Length; i++)
        {
            if(coins >= shopItemsSO[i].buyPrice)
            {
                purchasaBtns[i].interactable = true;
            }
            else
            {
                purchasaBtns[i].interactable = false;
            }
        }
    }

    public void PurchaseItem(int btnNo)
    {
        if (coins >= shopItemsSO[btnNo].buyPrice)
        {
            coins = coins - shopItemsSO[btnNo].buyPrice;
            coinsTxt.text = "Coins: " + coins.ToString();
            CheckPurchaseable();
        }
    }

    public void LoadPanels()
    {
        for(int i = 0; i < shopItemsSO.Length; i++)
        {
            shopPanels[i].titleTxt.text = shopItemsSO[i].plantTitle;
            shopPanels[i].priceTxt.text = shopItemsSO[i].buyPrice.ToString();
        }
    }

    public void BoughtVegetables(int id)
    {
        bool result = InventoryManager.AddItem(shopItemsSO[id]);
        if (result == true)
        {
            Debug.Log("Item added");
        }
        else
        {
            Debug.Log("ITEM NOT ADDED");
        }
    }

    public void GetSelectedVegetable()
    {
        ShopPlantItemSO receivedVegetable = InventoryManager.GetSelectedVegetable(false);
        if (receivedVegetable != null)
        {
            Debug.Log("Received item: " + receivedVegetable);

        }
        else
        {
            Debug.Log("Item NOT received");
        }
    }

    public void UseSelectedVegetable()
    {
        ShopPlantItemSO receivedVegetable = InventoryManager.GetSelectedVegetable(true);
        if (receivedVegetable != null)
        {
            Debug.Log("Used item: " + receivedVegetable);
        }
        else
        {
            Debug.Log("Item NOT USED");
        }
    }
}

