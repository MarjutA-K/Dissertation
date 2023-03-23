
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreManager : MonoBehaviour
{
    public GameObject plantItem;
    public GameObject items;
    List<PlantSO> plantObjects = new List<PlantSO>();
    List<ShopItemsSO> itemObjects = new List<ShopItemsSO>();
    private void Awake()
    {
        //Assets/Resources/Plants
        /*var loadPlants = Resources.LoadAll("ScriptableObjects", typeof(ShopPlantItemSO));
        foreach (var plant in loadPlants)
        {
            plantObjects.Add((ShopPlantItemSO)plant);
        }
        plantObjects.Sort(SortByPrice);

        foreach (var plant in plantObjects)
        {
            PlantItemTemplate newPlant = Instantiate(plantItem, transform).GetComponent<PlantItemTemplate>();
            newPlant.plant = plant;
        }

        // Items
        var loadItems = Resources.LoadAll("ScriptableObjects", typeof(ShopItemsSO));
        foreach (var item in loadItems)
        {
            itemObjects.Add((ShopItemsSO)item);
        }
        itemObjects.Sort(SortItemsByPrice);

        foreach (var item in itemObjects)
        {
            ItemTemplate newItem = Instantiate(items, transform).GetComponent<ItemTemplate>();
            newItem.item = item;
        }*/
    }

    private void Start()
    {
        
    }

    /*int SortByPrice(ShopPlantItemSO plantObject1, ShopPlantItemSO plantObject2)
    {
        return plantObject1.buyPrice.CompareTo(plantObject2.buyPrice);
    }*/

    //int SortByTime(ShopPlantItemSO plantObject1, ShopPlantItemSO plantObject2)
    //{
        //return plantObject1.timeBtwStages.CompareTo(plantObject2.timeBtwStages);
    //}

    /*int SortItemsByPrice(ShopItemsSO itemObject1, ShopItemsSO itemObject2)
    {
        return itemObject1.buyPrice.CompareTo(itemObject2.buyPrice);
    }*/
}