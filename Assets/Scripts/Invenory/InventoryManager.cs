/*Inventory system was build using the following tutorial: https://www.youtube.com/watch?v=oJAE6CbsQQA&t=400s */

using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    [SerializeField] public PlantArrayWrapper plants;

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

        for (int i = 0; i < plants.startPlant.Length; i++)
        {
            if (plants.startPlant[i] != null)
            {
                AddItemFromSave(plants.startPlant[i], i, plants.plantCounts[i]);
           }

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

    public bool AddItem(PlantSO plant)
    {
        // Check if any slot has the same item with count lower than max
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null && itemInSlot.plant == plant 
                && itemInSlot.count < maxStackedItems
                /*&& itemInSlot.plant.stackable == true*/)
            {
                plants.plantCounts[i]++;
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
                plants.startPlant[i] = plant;
                plants.plantCounts[i]++;
                SpawnNewItem(plant, slot);
                return true;
            }
        }

        return false;
    }

    public bool AddItemFromSave(PlantSO plant, int index, int count)
    {
        InventorySlot slot = inventorySlots[index];
        if(plants.startPlant[index] == null)
        {
            return false;
        }
        else
        {
            GameObject newPlantGo = Instantiate(inventoryItemPrefab, slot.transform);
            InventoryItem item = newPlantGo.GetComponent<InventoryItem>();
            item.InitializeFromSave(plant, count);
          
            return true;
        }
    }

    void SpawnNewItem(PlantSO plant, InventorySlot slot)
    {
        GameObject newPlantGo = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newPlantGo.GetComponent<InventoryItem>();
        inventoryItem.InitializePlant(plant);
    }

    public PlantSO GetSelectedPlant(bool use)
    {
        InventorySlot slot = inventorySlots[selectedSlot];
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
        if (itemInSlot != null)
        {
            PlantSO plant = itemInSlot.plant;
            if(use)
            {
                itemInSlot.count--;
                plants.plantCounts[selectedSlot]--;

                if(itemInSlot.count <= 0)
                {
                    plants.startPlant[selectedSlot] = null;
                
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
