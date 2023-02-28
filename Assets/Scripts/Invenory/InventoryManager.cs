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
        if(Input.inputString != null)
        {
            bool isNumber = int.TryParse(Input.inputString, out int number);
            if(isNumber && number > 0 && number < 8)
            {
                ChangeSelectedSlot(number - 1);
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

    public bool AddItem(ShopPlantItemSO vegetable)
    {
        // Check if any slot has the same item with count lower than max
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null && itemInSlot.vegetable == vegetable 
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
                SpawnNewItem(vegetable, slot);
                return true;
            }
        }

        return false;
    }

    void SpawnNewItem(ShopPlantItemSO vegetable, InventorySlot slot)
    {
        GameObject newVegetableGo = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newVegetableGo.GetComponent<InventoryItem>();
        inventoryItem.InitializeVegtable(vegetable);
    }

    public ShopPlantItemSO GetSelectedVegetable(bool use)
    {
        InventorySlot slot = inventorySlots[selectedSlot];
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
        if (itemInSlot != null)
        {
            ShopPlantItemSO vegetable = itemInSlot.vegetable;
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
            return vegetable;
        }

        return null;
    }
}
