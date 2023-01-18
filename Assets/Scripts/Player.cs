using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public void AddXP()
    {
        XPManager.instance.AddXP(100);
    }

    public void AddMoney()
    {
        GardenManager.instance.AddMoney(50);
    }
}
