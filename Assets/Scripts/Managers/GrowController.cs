using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class GrowController : MonoBehaviour
{
    [Header("Sprite Renderer")]
    public SpriteRenderer sr;
    [Header("Vegatable Scritable Object")]
    public PlantSO plant;

    private Player player;

    public Sprite emptyPlot;

    public float timer;

    public bool isGrowing;
    private int growthStage;
    private float growthTime;
    private int maxSize;

    public bool orderCompleted;

    private DestroyGameObjects objectsToDestroy;
    private OrderInventory orderInventory;
    AchievementManager achievementManager;

    //GameObject go;

    private void Start()
    {
        isGrowing = plant.isGrowing;
        growthStage = plant.growthStage;
        growthTime = plant.growthTime;
        maxSize = plant.maxSize;
        //orderCompleted = false;

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        orderInventory = FindObjectOfType<OrderInventory>();
        objectsToDestroy = FindObjectOfType<DestroyGameObjects>();
        achievementManager = FindObjectOfType<AchievementManager>();
    }

    private void Update()
    {
        Growing();

        // SEEMS TO WORK NOW?? -> Prevents clicking thru UI elements BUT stops gameobjects from updating in real time when clicking a button (Might not be a problem when using pedometer?)
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        ClickPlot();
        //GrowTimer();
    }

    private void Growing()
    {
        // Grow plant
        if (TestPedometer.instance.stepCount >= growthTime * growthStage && isGrowing)
        {
            sr.color = new Color(255, 255, 255, 255);

            growthStage++;

            if (growthStage >= maxSize)
            {
                growthStage = maxSize;
                sr.sprite = plant.growthSprite[growthStage];
                isGrowing = false;
                //DropItems();
                orderInventory.AddPlant(plant);
            }
        }

        // Change apperance
        if (growthStage != -1)
        {
            if (isGrowing)
            {
                sr.sprite = plant.growthSprite[growthStage];
            }
        }
        else
        {
            sr.sprite = emptyPlot;
        }
    }

    public void ClickPlot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                if (!isGrowing && player.activePlant && growthStage < maxSize)
                {
                    InventoryManager.instance.GetSelectedPlant(true);

                    plant = player.activePlant;

                    isGrowing = plant.isGrowing;
                    growthStage = plant.growthStage;
                    growthTime = plant.growthTime;
                    maxSize = plant.maxSize;

                    isGrowing = true;
                    growthStage = 0;
                
                    achievementManager.CheckAchievement(0);
                }
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
            if(!orderInventory.HasPlant(order.plantsRequired[i]))
            {
                orderCompleted = false;
                break;
            }
        }

        if (orderCompleted)
        {
            //achievementManager.OrdersCompletedAchievement(2);
            achievementManager.CheckAchievement(2);

            for (int i = 0; i < order.plantsRequired.Length; i++)
            {
                orderInventory.RemovePlant(order.plantsRequired[i]);

                PlantSO plant = order.plantsRequired[i];
                int count = orderInventory.CountPlant(plant);

                GrowController[] plotGameObjects = FindObjectsOfType<GrowController>();
                foreach (GrowController plot in plotGameObjects)
                {
                    if(plot.plant == plant)
                    {
                        plot.sr.sprite = plot.emptyPlot;
                        plot.isGrowing = false;
                        plot.growthStage = -1;
                        plot.plant = null;
                        count--;

                        if(count == 0)
                        {
                            break;
                        }
                    }
                }
            }


            Debug.Log("Order completed");
            OrderManager orderManager = FindObjectOfType<OrderManager>();
            orderManager.CompleteOrder(order);

            /*GrowController[] plotGameObjects = FindObjectsOfType<GrowController>();

            foreach (GrowController plot in plotGameObjects)
            {
                if (order.plantsRequired.Contains(plot.plant))
                {
                    plot.sr.sprite = plot.emptyPlot;
                    plot.isGrowing = false;
                    plot.growthStage = -1;
                    plot.plant = null;
                    Debug.Log("wot");
                    //break;
                }
            }*/
        }        
    }

    /*void DropItems()
    {
        orderInventory.AddPlant(plant);
    }*/
}

/*public void CheckOrder()
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
              for (int i = 0; i < order.plantsRequired.Length; i++)
              {
                  orderInventory.RemovePlant(order.plantsRequired[i]);
              }

              Debug.Log("Order completed");
              activeOrders.Remove(order);
              //sr.sprite = emptyPlot;     
              break;
          }
      }
  }*/

/*void DropItems()
{
    //go = new GameObject(plant.name + "Drop");
    //go.tag = "Drop";
    //DropController dc = go.AddComponent<DropController>();
    //dc.worth = vg.worthPer;

    //CircleCollider2D col = go.AddComponent<CircleCollider2D>();
    //col.isTrigger = true;
    //SpriteRenderer ren = go.AddComponent<SpriteRenderer>();
    //ren.sprite = plant.finalProduct;
    //ren.sortingOrder = 1;
    //go.transform.position = new Vector3(transform.position.x, transform.position.y, -2);

    //objectsToDestroy.AddPlant(go);

    orderInventory.AddPlant(plant);
    //sr.sprite = emptyPlot;

    CheckOrder();
}*/


/*foreach (GameObject obj in objectsToDestroy.objects)
{
    Destroy(obj);
    objectsToDestroy.objects.Clear();
}*/


/*foreach(PlantOrdersSO order in activeOrders)
       {
           if(orderInventory.HasPlant(plant) && System.Array.Exists(order.plantsRequired, p => p == plant))
           {
               int plantIndex = System.Array.IndexOf(order.plantsRequired, plant);
               order.quantityRequired[plantIndex] -= 1;
               if(order.quantityRequired[plantIndex] == 0)
               {
                   Debug.Log("Order completed");
                   break;
               }
               else
               {
                   Debug.Log("Order NOT completed");
               }
           }
       }*/


/*public void GrowTimer()
{
    timer += Time.deltaTime;

    // Grow plant
    if (timer >= growthTime && isGrowing)
    {
        sr.color = new Color(255, 255, 255, 255);

        timer = 0f;
        growthStage++;

        if (growthStage >= maxSize)
        {
            growthStage = maxSize;
            sr.sprite = plant.growthSprite[growthStage];
            isGrowing = false;
            StartCoroutine(FinishGrowing());
        }
    }

    // Change apperance
    if (growthStage != -1)
    {
        if (isGrowing)
        {
            sr.sprite = plant.growthSprite[growthStage];
        }
    }
    else
    {
        sr.sprite = emptyPlot;
    }
}*/

/*IEnumerator FinishGrowing()
{
    yield return new WaitForSeconds(growthTime);
    //sr.sprite = emptyPlot;
    //DropItems();
}*/

/*void DestroyObjects()
{
foreach (GameObject obj in objectsToDestroy)
{
    Destroy(obj);
}
objectsToDestroy.Clear();
}*/
