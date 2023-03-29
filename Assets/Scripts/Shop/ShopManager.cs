using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;
    private XPManager _xp;
    public InventoryManager InventoryManager;
    [SerializeField]
    public TempLoadSave _saveManager;

    public int money;
    public TMP_Text moneyTxt;

    public PlantSO[] shopItemsSO;
    public GameObject[] shopPanelsSO;
    public PlantItemTemplate[] shopPanels;
    public Button[] purchasaBtns;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        _xp = FindObjectOfType<XPManager>();

        for (int i = 0; i < shopItemsSO.Length; i++)
        {
            shopPanelsSO[i].SetActive(true);
        }

        moneyTxt.text = "Money: " + money.ToString();
        LoadPanels();
        CheckPurchaseable();
    }

    private void Update()
    {
        CheckPurchaseable();
    }

    public void AddMoney(int _money)
    {
        money += _money;
        moneyTxt.text = "Coins: " + money.ToString();
        CheckPurchaseable();
        _saveManager.moneyChanged.Invoke(money);
    }

    // Check if Item is purchable
   public void CheckPurchaseable()
    {
        for (int i = 0; i < shopItemsSO.Length; i++)
        {
            if (money >= shopItemsSO[i].buyPrice && _xp.level >= shopItemsSO[i].level)
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
        if (money >= shopItemsSO[btnNo].buyPrice)
        {
            money = money - shopItemsSO[btnNo].buyPrice;
            moneyTxt.text = "Coins: " + money.ToString();
            CheckPurchaseable();
            _saveManager.moneyChanged.Invoke(money);
            BoughtPlant(btnNo);
        }
    }

    public void LoadPanels()
    {
        for(int i = 0; i < shopItemsSO.Length; i++)
        {
            shopPanels[i].icon.sprite = shopItemsSO[i].icon;
            shopPanels[i].titleTxt.text = shopItemsSO[i].plantTitle;

            if (_xp.level >= shopItemsSO[i].level)
            {         
                shopPanels[i].priceTxt.text = "£" + shopItemsSO[i].buyPrice.ToString();
                shopPanels[i].lockedTxtActive.SetActive(false);
                shopPanels[i].priceTxtActive.SetActive(true);
            }
            else
            {
                shopPanels[i].lockedTxtActive.SetActive(true);
                shopPanels[i].priceTxtActive.SetActive(false);
                shopPanels[i].lockedTxt.text = "Reach Level " + shopItemsSO[i].level.ToString() + " to Unlock plant";
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
        PlantSO receivedVegetable = InventoryManager.GetSelectedPlant(true);
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

