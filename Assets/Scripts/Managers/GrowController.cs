using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
    public int growthStage;
    public float growthTime;
    public int maxSize;

    public bool orderCompleted;

    private OrderInventory orderInventory;
    AchievementManager achievementManager;

    public Slider slider;

    private void Start()
    {
       isGrowing = plant.isGrowing;
       growthStage = plant.growthStage;
       growthTime = plant.growthSteps;

       maxSize = plant.maxSize;   
        
        /*if(plant.plantTitle == "Black Rose" || plant == null)
        {
            slider.gameObject.SetActive(false);
        }*/

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        orderInventory = FindObjectOfType<OrderInventory>();
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
        slider.maxValue = maxSize;
    }

    private void Growing()
    {
        // Grow plant
        if (StepTracker.instance.stepCount >= growthTime * growthStage && isGrowing)
        {
            growthStage++;

            if (growthStage >= maxSize)
            {
                growthStage = maxSize;
                sr.sprite = plant.growthSprite[growthStage];

                isGrowing = false;
                orderInventory.AddPlant(plant);
            }
            slider.value = growthStage;
        }

        // Change apperance
        if (growthStage < 0)
        {
            sr.sprite = emptyPlot;
            slider.value = 0f;
            slider.gameObject.SetActive(false);            
        }
        else
        {
            if (isGrowing)
            {
                sr.sprite = plant.growthSprite[growthStage];
            }
        }
        //PatchManager.instance.RefreshPatches();
    }

    /*public void LoadFromData(PlantSO loaded, int _growStage, float _growTime)
    {
        plant = loaded;
        growthStage = _growStage;
        growthTime = _growTime;
   
        isGrowing = plant.isGrowing;
        sr.sprite = plant.growthSprite[_growStage];

        if(_growStage >= plant.maxSize)
        {
            slider.gameObject.SetActive(false);
            //orderInventory.AddPlant(plant);
        }

        slider.gameObject.SetActive(true);
    }*/

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
                    growthTime = plant.growthSteps;
                    maxSize = plant.maxSize;           
                    isGrowing = true;

                    achievementManager.CheckAchievement(0);

                    slider.gameObject.SetActive(true);
                    PatchManager.instance.RefreshPatches();
                    
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

                        if (count == 0)
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
        //PatchManager.instance.RefreshPatches(); 
    }
    
}
