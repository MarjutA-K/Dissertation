using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New plant Requirement", menuName = "Plant Requirement")]
public class PlantRequirementSO : ScriptableObject
{
    public PlantSO plant;
    public int quantityRequired;
    public int reward;

   /* public bool IsFullFilled(OrderInventory inventory)
    {
        int quantityInInventory = inventory.GetQuantity(plant);
        return quantityInInventory >= quantityRequired;
    }

    public void FullFill(OrderInventory inventory)
    {
        inventory.RemovePlant(plant);
    }*/
}
