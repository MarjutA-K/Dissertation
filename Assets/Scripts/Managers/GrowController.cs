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
    [Header("Plant Scritable Object")]
    public PlantSO plant;

    private Player player;
    private OrderInventory orderInventory;
    private AchievementManager achievementManager;

    public Sprite emptyPlot;

    public bool isGrowing;
    public int growthStage;
    public float growthTime;
    public int maxSize;
    public bool orderCompleted;

    public Slider slider;

    private void Start()
    {
        isGrowing = plant.isGrowing;
        maxSize = plant.maxSize;

        if (plant.plantTitle == "Black Rose" || plant == null)
        {
            slider.gameObject.SetActive(false);
        }

        if (orderInventory == null)
        {
            orderInventory = FindObjectOfType<OrderInventory>();
        }

        achievementManager = FindObjectOfType<AchievementManager>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void Update()
    {
        Growing();        

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
                slider.gameObject.SetActive(false);
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

        PatchManager.instance.RefreshPatches();
    }

    public void LoadFromData(PlantSO loaded, int _growStage, float _growTime)
    {
        if(orderInventory == null)
        {
            orderInventory = FindObjectOfType<OrderInventory>();
        }

        plant = loaded;
        growthStage = _growStage;
        growthTime = _growTime;
        isGrowing = plant.isGrowing;
        sr.sprite = plant.growthSprite[_growStage];
       
        if(_growStage >= 5)
        {
            slider.gameObject.SetActive(false);
            isGrowing = false;
            orderInventory.AddPlant(plant);
        }
        else
        {
            slider.gameObject.SetActive(true);
        }

        slider.value = growthStage; 
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
}
