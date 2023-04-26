using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PlantWrapper", menuName = "Wrappers")]
public class PlantArrayWrapper : ScriptableObject
{
    public PlantSO[] startPlant;
    public int[] plantCounts;
    public int[] growthStage;
    public float[] growthSteps;
}
