using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OrderManager : MonoBehaviour
{
    private List<ShopPlantItemSO> _inventory = new List<ShopPlantItemSO>();

    public void AddPlant(ShopPlantItemSO plant)
    {
        _inventory.Add(plant);
        Debug.Log("Plant List Count: " + _inventory.Count);
        foreach (var plants in _inventory)
        {
            Debug.Log("Plant: " + plants.name);
        }
    }

    public void RemovePlant(ShopPlantItemSO plant)
    {
        _inventory.Remove(plant);
    }

    public bool HasPlant(ShopPlantItemSO plant)
    {
        return _inventory.Contains(plant);
    }
}
