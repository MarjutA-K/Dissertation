using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlantSO activePlant;

    private void Update()
    {
        activePlant = InventoryManager.instance.GetSelectedPlant(false);
    }

    public void AddXP()
    {
        XPManager.instance.AddXP(100);
    }

    public void AddMoney()
    {
        ShopManager.instance.AddMoney(50);
    }

    public void AddRewardMoney1()
    {
        ShopManager.instance.AddMoney(50);
    }

    public void AddRewardMoney2()
    {
        ShopManager.instance.AddMoney(100);
    }
}
