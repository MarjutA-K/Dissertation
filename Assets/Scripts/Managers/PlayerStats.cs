using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int money = 0;

    public float interactRange;

    public ShopPlantItemSO activeVegetable;

    private void Update()
    {
        activeVegetable = InventoryManager.instance.GetSelectedVegetable(false);
    }
}
