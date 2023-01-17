using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class ShopManager : MonoBehaviour
{
    public int coins;
    public TMP_Text coinsTxt;
    public ShopItemsSO[] shopItemsSO;
    public GameObject[] shopPanelsSO;
    public ItemTemplate[] shopPanels;
    public Button[] purchasaBtns;

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
            shopPanels[i].titleTxt.text = shopItemsSO[i].itemTitle;
            shopPanels[i].priceTxt.text = shopItemsSO[i].buyPrice.ToString();
        }
    }
}

