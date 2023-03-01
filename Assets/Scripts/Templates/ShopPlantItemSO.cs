
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

    [Header("Level to be Unlocked")]
    public int level;

    //public float timeBtwStages;
    //public bool isVisible = true;
    //public Sprite dryPlanted;
    //public bool isLocked = true;
}