using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Order", menuName = "Orders")]
public class PlantOrdersSO : ScriptableObject
{
    public string orderTitle;
    public ShopPlantItemSO[] plantsRequired;
    public int[] quantityRequired;
    public int reward;
    public Sprite[] icon;
}