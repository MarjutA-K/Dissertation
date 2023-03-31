using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    //public static ShopManager instance;
    private XPManager _xp;
    public InventoryManager InventoryManager;
    [SerializeField]
    public TempLoadSave _saveManager;

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

    /*private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }*/

    private void Start()
    {
        _xp = FindObjectOfType<XPManager>();

        for (int i = 0; i < shopItemsSO.Length; i++)
        {
            shopPanelsSO[i].SetActive(true);
        }

        moneyTxt.text = money.ToString();
        diamondsTxt.text = diamonds.ToString();

        LoadPanels();
        CheckPurchaseable();

        shopIsActive = shopObject.activeSelf;
    }

    private void Update()
    {
        CheckPurchaseable();

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

    public void CheckPurchableDiamonds()
    {
        //
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
            moneyTxt.text = money.ToString();
            CheckPurchaseable();
            _saveManager.moneyChanged.Invoke(money);
            BoughtPlant(btnNo);
        }
    }

    public void LoadPanels()
    {
        for(int i = 0; i < shopItemsSO.Length; i++)
        {
            
            shopPanels[i].titleTxt.text = shopItemsSO[i].plantTitle;

            if (_xp.level >= shopItemsSO[i].level)
            {         
                shopPanels[i].priceTxt.text = shopItemsSO[i].buyPrice.ToString();
                shopPanels[i].priceTxtActive.SetActive(true);
                shopPanels[i].titleActive.SetActive(true);
                shopPanels[i].icon.sprite = shopItemsSO[i].icon;
            }
            else
            {
                shopPanels[i].icon.sprite = shopItemsSO[i].lockIcon;
                shopPanels[i].priceTxtActive.SetActive(false);
                shopPanels[i].titleActive.SetActive(false);
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

