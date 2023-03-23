using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlantOrderTemplate : MonoBehaviour
{
    public OrdersSO orderList;

    public TMP_Text orderTitleTxt;
    //public TMP_Text[] plantsRequiredTxt;
    public TMP_Text[] quantityRequiredTxt;
    public TMP_Text rewardTxt;

    public Image[] image;

    private void Start()
    {
        UpdateUI(orderList.orders[0]);
        UpdateUI(orderList.orders[1]);
    }

    public void UpdateUI(PlantOrdersSO order)
    {
        orderTitleTxt.text = order.orderTitle;

        for (int i = 0; i < order.plantsRequired.Length; i++)
        {
            //plantsRequiredTxt[i].text = order.plantsRequired[i].plantTitle;
            quantityRequiredTxt[i].text = order.quantityRequired[i].ToString();
            image[i].sprite = order.icon[i];
        }

        rewardTxt.text = order.reward.ToString();
    }
}
