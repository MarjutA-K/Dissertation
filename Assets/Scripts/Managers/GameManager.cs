using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public OrdersSO orderList;

    void Start()
    {
       /* // Create a new instance of the OrderList Scriptable Object
        orderList = ScriptableObject.CreateInstance<OrdersSO>();

        // Add some orders to the OrderList
        PlantOrdersSO order1 = ScriptableObject.CreateInstance<PlantOrdersSO>();
        order1.orderTitle = "Order 1";
        order1.plantsRequired = new ShopPlantItemSO[] { ShopPlantItemSO.Sunflower, ShopPlantItemSO. };
        order1.quantityRequired = new int[] { 5, 10 };
        order1.reward = 50;

        Order order2 = ScriptableObject.CreateInstance<Order>();
        order2.orderName = "Order 2";
        order2.requiredPlants = new Plant[] { Plant.Corn, Plant.Carrot };
        order2.requiredQuantities = new int[] { 8, 6 };
        order2.reward = 75;

        // Add the orders to the OrderList
        orderList.orders = new Order[] { order1, order2 };*/
    }
}
