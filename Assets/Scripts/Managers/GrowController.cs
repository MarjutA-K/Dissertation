using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowController : MonoBehaviour
{
    [Header("Sprite Renderer")]
    public SpriteRenderer sr;
    [Header("Vegatable Scritable Object")]
    public ShopPlantItemSO vg;

    private PlayerStats playerStats;

    public Sprite emptyPlot;

    private float timer;

    private bool isGrowing;
    private int growthStage;
    private float growthTime;
    private int maxSize;

    private float interactRange;
    public float playerDistance;
    public bool interactable;

    private void Start()
    {
        isGrowing = vg.isGrowing;
        growthStage = vg.growthStage;
        growthTime = vg.growthTime;
        maxSize = vg.maxSize;

        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        interactRange = playerStats.interactRange;
    }

    private void FixedUpdate()
    {
        playerDistance = Vector2.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);

        if (playerDistance <= interactRange)
        {
            interactable = true;
        }
        else
        {
            interactable = false;
        }
    }

    private void Update()
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
                sr.sprite = vg.growthSprite[growthStage];
                isGrowing = false;
                StartCoroutine(FinishGrowing());
            }
        }

        // Change apperance
        if (growthStage != -1)
        {
            if (isGrowing)
            {
                sr.sprite = vg.growthSprite[growthStage];
            }
        }
        else
        {
            sr.sprite = emptyPlot;
        }
    }
    IEnumerator FinishGrowing()
    {
        yield return new WaitForSeconds(growthTime);
        sr.sprite = emptyPlot;
        DropItems();
    }

    void DropItems()
    {
        GameObject go = new GameObject(vg.name + "Drop");
        go.tag = "VegetableDrop";
        DropController dc = go.AddComponent<DropController>();
        dc.worth = vg.worthPer;

        CircleCollider2D col = go.AddComponent<CircleCollider2D>();
        col.isTrigger = true;
        SpriteRenderer ren = go.AddComponent<SpriteRenderer>();
        ren.sprite = vg.finalProduct;
        ren.sortingOrder = 1;
        go.transform.position = new Vector3(transform.position.x, transform.position.y, -2);

        Vector2 dropDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        Rigidbody2D rb = go.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.drag = 5;
        rb.AddForce(dropDirection * 2f, ForceMode2D.Impulse);
    }

    private void OnMouseDown()
    {
        if (!isGrowing && interactable && playerStats.activeVegetable)
        {
            Debug.Log("Pressed");

            InventoryManager.instance.GetSelectedVegetable(true);

            vg = playerStats.activeVegetable;

            isGrowing = vg.isGrowing;
            growthStage = vg.growthStage;
            growthTime = vg.growthTime;
            maxSize = vg.maxSize;

            isGrowing = true;
            growthStage = 0;
        }
    }
}
