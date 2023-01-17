
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "New Plant", menuName = "Plant")]
public class ShopPlantItemSO : ScriptableObject
{
    public string plantTitle;
    public Sprite[] plantStages;
    public float timeBtwStages;
    public int buyPrice;
    public int sellPrice;
    public Sprite icon;
    public Sprite dryPlanted;
    // when player has no money deselcts the item
    public bool isVisible = true;
    public int level;
    public bool isLocked = true;
}