using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveOrders : MonoBehaviour
{
    public OrderManager orderManager;
    public List<PlantOrdersSO> activeOrders = new List<PlantOrdersSO>();

    private void Start()
    {
        MakeOrderActive();
    }

    public void MakeOrderActive()
    {
        foreach(PlantOrdersSO order in activeOrders)
        {
            orderManager.AddActiveOrder(order);
        }
    }
}
