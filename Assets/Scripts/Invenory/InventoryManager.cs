using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    public ShopPlantItemSO[] startVegetables;

    public int maxStackedItems = 4;
    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;

    int selectedSlot = -1;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        ChangeSelectedSlot(0);

        foreach(var vegetable in startVegetables)
        {
            AddItem(vegetable);
        }
    }

    private void Update()
    {
        // Check for mouse click on inventory slot
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (Input.GetMouseButtonDown(0) && RectTransformUtility.RectangleContainsScreenPoint(inventorySlots[i].GetComponent<RectTransform>(), Input.mousePosition))
            {
                ChangeSelectedSlot(i);
                break;
            }
        }

    }

    void ChangeSelectedSlot(int newValue)
    {
        if(selectedSlot >= 0)
        {
            inventorySlots[selectedSlot].Deselect();
        }

        inventorySlots[newValue].Select();
        selectedSlot = newValue;
    }

    public bool AddItem(ShopPlantItemSO plant)
    {
        // Check if any slot has the same item with count lower than max
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null && itemInSlot.plant == plant 
                && itemInSlot.count < maxStackedItems
                /*&& itemInSlot.vegetable.stackable == true*/)
            {
                itemInSlot.count++;
                itemInSlot.RefreshCount();
                return true;
            }
        }

        // Find an empty slot
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if(itemInSlot == null)
            {
                SpawnNewItem(plant, slot);
                return true;
            }
        }

        return false;
    }

    void SpawnNewItem(ShopPlantItemSO plant, InventorySlot slot)
    {
        GameObject newPlantGo = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newPlantGo.GetComponent<InventoryItem>();
        inventoryItem.InitializePlant(plant);
    }

    public ShopPlantItemSO GetSelectedPlant(bool use)
    {
        InventorySlot slot = inventorySlots[selectedSlot];
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
        if (itemInSlot != null)
        {
            ShopPlantItemSO plant = itemInSlot.plant;
            if(use)
            {
                itemInSlot.count--;
                if(itemInSlot.count <= 0)
                {
                    Destroy(itemInSlot.gameObject);
                }
                else
                {
                    itemInSlot.RefreshCount();
                }
            }
            return plant;
        }

        return null;
    }
}
