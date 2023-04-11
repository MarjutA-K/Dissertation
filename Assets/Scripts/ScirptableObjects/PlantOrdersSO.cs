using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Order", menuName = "Orders")]
public class PlantOrdersSO : ScriptableObject
{
    public string orderTitle;
    public int quantity;
    public PlantSO[] plantsRequired;
    public int[] quantityRequired;
    public int rewardAmount;
    public Sprite[] icon;
}
