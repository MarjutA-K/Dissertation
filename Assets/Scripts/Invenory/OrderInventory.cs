using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OrderInventory : MonoBehaviour
{
    public static OrderInventory instance;

    public List<PlantSO> plants = new List<PlantSO>();

    private void Awake()
    {
        instance = this;
    }

    public void AddPlant(PlantSO plant)
    {
        plants.Add(plant);
        /*.Log("Plant List Count: " + plants.Count);
        foreach (var plants in plants)
        {
            Debug.Log("Plant: " + plants.name);
        }*/
    }

    public void RemovePlant(PlantSO plant)
    {
        plants.Remove(plant);
    }

    public bool HasPlant(PlantSO plant)
    {
        return plants.Contains(plant);
    }

    public int CountPlant(PlantSO plant)
    {
        int count = 0;

        foreach(PlantSO p in plants)
        {
            if(p == plant)
            {
                count++;
            }
        }
        Debug.Log(count + plant.name);
        return count;
    }
}
