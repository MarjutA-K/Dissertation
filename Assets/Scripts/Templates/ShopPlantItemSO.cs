
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Plant", menuName = "Plant")]
public class ShopPlantItemSO : ScriptableObject
{
    //UI Template
    [Header("Shop Template")]
    public string plantTitle;
    public int buyPrice;
    public int sellPrice;
    public Sprite icon;

    //Growth
    [Header("Growth")]
    public Sprite[] growthSprite;
    public bool isGrowing = true;
    public int growthStage;
    public float growthTime;
    public int maxSize;
    public int worthPer;
    public Sprite finalProduct;

    // when player has no money deselcts the item
    public bool isVisible = true;
    public int level;
    public bool isLocked = true;

    //public float timeBtwStages;

    //public Sprite dryPlanted;
}