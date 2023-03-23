using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OrderInventory : MonoBehaviour
{
    private List<PlantSO> plants = new List<PlantSO>();

    public void AddPlant(PlantSO plant)
    {
        plants.Add(plant);
        Debug.Log("Plant List Count: " + plants.Count);
        foreach (var plants in plants)
        {
            Debug.Log("Plant: " + plants.name);
        }
    }

    public void RemovePlant(PlantSO plant)
    {
        plants.Remove(plant);
    }

    public bool HasPlant(PlantSO plant)
    {
        return plants.Contains(plant);
    }

    public int GetQuantity(PlantSO plant)
    {
        int count = 0;
       /*foreach(PlantSO plant in plants)
        {
            if(plant.plantType == plantType)
            {
                count++;
            }
        }*/

        return count;
    }
}


/*[System.Serializable]
public class Plant
{
    public PlantType type;
    public int quantity;
}*/

/*public class PlantInventory : MonoBehaviour
{
    public List<Plant> plants = new List<Plant>();
}*/

/*public class OrderManager : MonoBehaviour
{
    public List<Order> orders = new List<Order>();
}*/
