using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OrderManager : MonoBehaviour
{
    public static OrderManager instance;

    public List<PlantOrdersSO> activeOrders = new List<PlantOrdersSO>();
    public List<PlantOrderTemplate> orderTemplates = new List<PlantOrderTemplate>();

    private OrderInventory orderInventory;
    private AchievementManager achievementManager;

    private bool orderCompleted = false;

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

        if (orderInventory == null)
        {
            orderInventory = FindObjectOfType<OrderInventory>();
        }

        achievementManager = FindObjectOfType<AchievementManager>();
    }

    public void AddActiveOrder(PlantOrdersSO order)
    {
        activeOrders.Add(order);
        //Debug.Log("Add active order: " + order.name);
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

     public void CheckOrder(PlantOrdersSO order)
    {
        if (order == null)
        {
            return;
        }

        orderCompleted = true;

        for (int i = 0; i < order.plantsRequired.Length; i++)
        {
            if (!orderInventory.HasPlant(order.plantsRequired[i]))
            {
                orderCompleted = false;
                break;
            }
        }

        if (orderCompleted)
        {
            achievementManager.CheckAchievement(2);
            GrowController[] plotGameObjects = FindObjectsOfType<GrowController>();
            for (int i = 0; i < order.plantsRequired.Length; i++)
            {
                orderInventory.RemovePlant(order.plantsRequired[i]);

                PlantSO _plant = order.plantsRequired[i];
                int count = orderInventory.CountPlant(_plant);
                bool filled = false;
               
                foreach (GrowController plot in plotGameObjects)
                {
                    if (plot.plant == _plant && !filled)
                    {
                        plot.sr.sprite = plot.emptyPlot;
                        plot.isGrowing = false;
                        plot.growthStage = -1;
                        count--;
                        filled = true;
                        
                        if (count == 0 )
                        {

                            break;
                        }
                    }

                }

            }

            Debug.Log("Order completed");
            OrderManager orderManager = FindObjectOfType<OrderManager>();
            orderManager.CompleteOrder(order);
        }

        PatchManager.instance.RefreshPatches();
    }
}
