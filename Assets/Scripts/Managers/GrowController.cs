using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Start()
    {
        isGrowing = plant.isGrowing;
        growthStage = plant.growthStage;
        growthTime = plant.growthTime;
        maxSize = plant.maxSize;

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void Update()
    {
        ClickPlot();
        //GrowTimer();
        Growing();
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
                DropItems();
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

    public void GrowTimer()
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
                Debug.Log(hit.collider.gameObject.name);

                if (!isGrowing && player.activePlant)
                {
                    InventoryManager.instance.GetSelectedPlant(true);

                    plant = player.activePlant;

                    isGrowing = plant.isGrowing;
                    growthStage = plant.growthStage;
                    growthTime = plant.growthTime;
                    maxSize = plant.maxSize;

                    isGrowing = true;
                    growthStage = 0;
                }
            }
        }
    }

    IEnumerator FinishGrowing()
    {
        yield return new WaitForSeconds(growthTime);
        //sr.sprite = emptyPlot;
        //DropItems();
    }

    void DropItems()
    {
        GameObject go = new GameObject(plant.name + "Drop");
        go.tag = "Drop";
        DropController dc = go.AddComponent<DropController>();
        //dc.worth = vg.worthPer;

        CircleCollider2D col = go.AddComponent<CircleCollider2D>();
        col.isTrigger = true;
        SpriteRenderer ren = go.AddComponent<SpriteRenderer>();
        ren.sprite = plant.finalProduct;
        ren.sortingOrder = 1;
        go.transform.position = new Vector3(transform.position.x, transform.position.y, -2);

        OrderInventory orderInventory = FindObjectOfType<OrderInventory>();
        orderInventory.AddPlant(plant);
    }
}
