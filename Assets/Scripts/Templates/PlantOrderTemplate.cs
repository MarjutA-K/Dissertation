using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlantOrderTemplate : MonoBehaviour
{
    public PlantOrdersSO order;
    public TMP_Text orderTitleTxt;
    public TMP_Text[] quantityRequiredTxt;
    public TMP_Text[] currentQuantityTxt;
    public TMP_Text rewardTxt;
    public Image[] image;

    private void Start()
    {
        UpdateUI();
    }

    private void Update()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        orderTitleTxt.text = order.orderTitle;
        rewardTxt.text = order.rewardAmount.ToString();

        for (int i = 0; i < order.plantsRequired.Length; i++)
        {
            order.quantity = OrderInventory.instance.CountPlant(order.plantsRequired[i]);
            currentQuantityTxt[i].text = order.quantity.ToString();
            quantityRequiredTxt[i].text = order.quantityRequired[i].ToString();
            image[i].sprite = order.icon[i];         
        }
    }
}