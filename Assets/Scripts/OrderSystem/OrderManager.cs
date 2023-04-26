using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OrderManager : MonoBehaviour
{
    public static OrderManager instance;

    public List<PlantOrdersSO> activeOrders = new List<PlantOrdersSO>();
    public List<PlantOrderTemplate> orderTemplates = new List<PlantOrderTemplate>();

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

    public void AddActiveOrder(PlantOrdersSO order)
    {
        activeOrders.Add(order);
        Debug.Log("Add active order: " + order.name);
    }

    public void RemoveActiveOrder(PlantOrdersSO order)
    {
        activeOrders.Remove(order);
    }

    public List<PlantOrdersSO> GetActiveOrders()
    {
        return activeOrders;
    }

    public void CompleteOrder(PlantOrdersSO order)
    {
        if(order == null)
        {
            return;
        }

        activeOrders.Remove(order);

        foreach (PlantOrderTemplate template in orderTemplates)
        {
            if(template.order == order)
            {
                template.gameObject.SetActive(false);
                break;
            }
        }
    }
}
