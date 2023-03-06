using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public ShopPlantItemSO activePlant;

    private void Update()
    {
        activePlant = InventoryManager.instance.GetSelectedVegetable(false);
    }

    public void AddXP()
    {
        XPManager.instance.AddXP(100);
    }

    public void AddMoney()
    {
        ShopManager.instance.AddMoney(50);
    }


}
