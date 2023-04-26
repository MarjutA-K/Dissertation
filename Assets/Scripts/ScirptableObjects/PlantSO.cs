
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Plant", menuName = "Plant")]
public class PlantSO : ScriptableObject
{
    //UI Template
    [Header("Shop Template")]
    public string plantTitle;
    public int buyPrice;
    public int unlockPrice;
    public Sprite icon;
    public Sprite lockIcon;

    //Growth
    [Header("Growth")]
    public Sprite[] growthSprite;
    public bool isGrowing = true;
    public int growthStage;
    public int growthSteps;
    public int maxSize;

    [Header("Level to be Unlocked")]
    public int level;
    public bool unlocked;
}