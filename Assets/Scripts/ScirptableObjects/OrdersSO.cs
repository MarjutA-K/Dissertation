using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Order List", menuName = "Order List")]
public class OrdersSO : ScriptableObject
{
    public PlantOrdersSO[] orders;
}
