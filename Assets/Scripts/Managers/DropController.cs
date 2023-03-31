using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DropController : MonoBehaviour
{
    public bool isClicked = true;
    private GrowController gr;
    private DestroyGameObjects objectsToDestroy;
    private OrderInventory orderInventory;
    public bool orderCompleted = true;

    public PlantOrdersSO associatedOrder;

    private void Start()
    {
        gr = FindObjectOfType<GrowController>();
        objectsToDestroy = FindObjectOfType<DestroyGameObjects>();
        orderInventory = FindObjectOfType<OrderInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isClicked)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                Debug.Log("clicked" + hit.collider.gameObject.name);
                XPManager.instance.AddXP(100);
                //ShopManager.instance.AddMoney(50);

                isClicked = false;         
            }
        }

        CheckOrder();
    }

    void CheckOrder()
    {
        OrderManager orderManager = FindObjectOfType<OrderManager>();
        List<PlantOrdersSO> activeOrders = orderManager.GetActiveOrders();

        foreach (PlantOrdersSO order in activeOrders)
        {
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
                var dropControllersToDestroy = FindObjectsOfType<DropController>().Where(dc => dc.associatedOrder == order);

                foreach(var dropController in dropControllersToDestroy)
                {
                    Destroy(dropController.gameObject);
                }


                for (int i = 0; i < order.plantsRequired.Length; i++)
                {
                    orderInventory.RemovePlant(order.plantsRequired[i]);
                }

                Debug.Log("Order completed");
                activeOrders.Remove(order);    
                break;
            }         
        }
    }
}
