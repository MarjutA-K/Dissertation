
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GardenManager : MonoBehaviour
{
    public PlantItemTemplate selectPlant;
    public ItemTemplate selectItem;

    public static GardenManager instance;

    public bool isPlanting = false;
    public bool isPlacing = false;

    public int money;
    public TMP_Text moneyTxt;

    public Color buyColor = Color.green;
    public Color cancelColor = Color.red;

    public bool isSelecting = false;
    public int selectedTool = 0;
    // 1- water 2- Fertilizer 3- Buy plot

    public Image[] buttonsImg;
    public Sprite normalButton;
    public Sprite selectedButton;
    [SerializeField]
    TempLoadSave saveManager;


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

    // Start is called before the first frame update
    void Start()
    {
        moneyTxt.text = "$" + money;

    }


    public void SelectPlant(PlantItemTemplate newPlant)
    {
        if (selectPlant == newPlant)
        {
            CheckSelection();
            CheckItemSelection();
        }
        else
        {
            CheckSelection();
            CheckItemSelection();
            selectPlant = newPlant;
            selectPlant.btnImage.color = cancelColor;
            selectPlant.btnTxt.text = "Cancel";
            isPlanting = true;        
        }
    }

    public void SelectItem(ItemTemplate newItem)
    {
        if (selectItem == newItem)
        {
            CheckItemSelection();
            CheckSelection();

        }
        else
        {
            CheckItemSelection();
            CheckSelection();
            selectItem = newItem;
            selectItem.btnImage.color = cancelColor;
            selectItem.btnTxt.text = "Cancel";
            isPlacing = true;
        }
    }

    public void SelectTool(int toolNumber)
    {
        if (toolNumber == selectedTool)
        {
            //deselect
            CheckSelection();
            CheckItemSelection();
        }
        else
        {
            //select tool number and check to see if anything was also selected
            CheckSelection();
            CheckItemSelection();
            isSelecting = true;
            selectedTool = toolNumber;
            buttonsImg[toolNumber - 1].sprite = selectedButton;
        }
    }

    public void CheckSelection()
    {    
        if (isPlanting)
        {
            isPlanting = false;
            if (selectPlant != null)
            {
                selectPlant.btnImage.color = buyColor;
                selectPlant.btnTxt.text = "Buy";
                selectPlant = null;
            }
        }

        if (isSelecting)
        {
            if (selectedTool > 0)
            {
                buttonsImg[selectedTool - 1].sprite = normalButton;
            }
            isSelecting = false;
            selectedTool = 0;
        }
    }

    void CheckItemSelection()
    {
        if (isPlacing)
        {
            isPlacing = false;
            if (selectItem != null)
            {
                selectItem.btnImage.color = buyColor;
                selectItem.btnTxt.text = "Buy";
                selectItem = null;
            }
        }
        if (isSelecting)
        {
            if (selectedTool > 0)
            {
                buttonsImg[selectedTool - 1].sprite = normalButton;
            }
            isSelecting = false;
            selectedTool = 0;
        }
    }

    public void AddMoney(int _money)
    {
        money += _money;
        moneyTxt.text = "$" + money;
        saveManager.moneyChanged.Invoke(money);
    }

    public void Transaction(int value)
    {
        money += value;
        moneyTxt.text = "$" + money;
        saveManager.moneyChanged.Invoke(money);
    }

}