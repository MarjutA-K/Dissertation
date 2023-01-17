using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemTemplate : MonoBehaviour
{
    public ShopItemsSO item;

    public TMP_Text titleTxt;
    public TMP_Text priceTxt;
    public Image icon;

    public Image btnImage;
    public TMP_Text btnTxt;

    private int level;

    private Color buyColor = Color.green;
    private Color lockedColor = Color.gray;

    GardenManager gm;
    XPManager xpm;

    private bool interactable;
    private bool isOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GardenManager>();
        xpm = FindObjectOfType<XPManager>();
        InitializeUI();
        item.isLocked = true;
    }

    private void Update()
    {
        if (item.level <= xpm.level)
        {
            item.isLocked = false;
            if(!isOpen)
            {
                callFunction();
            }

        }

        if (item.isLocked)
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
        if (interactable)
        {
            Debug.Log("Bought " + item.itemTitle);
            gm.SelectItem(this);
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
        titleTxt.text = item.itemTitle;
        priceTxt.text = "$" + item.buyPrice;
        icon.sprite = item.icon;

        if (gm.money < item.buyPrice)
        {
            btnImage.GetComponent<Selectable>().interactable = false;
            interactable = false;
            btnImage.color = buyColor;
            btnTxt.text = "Buy";
            item.isVisible = false;
        }
        else
        {
            btnImage.GetComponent<Selectable>().interactable = true;
            interactable = true;
            item.isVisible = true;
        }
    }
}
