using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Achievement", menuName = "Achievements")]
public class AchievementSO : ScriptableObject
{
    public string description;
    public int requiredAmount;
    public int currentAmount;
    public int rewardAmount;
    public bool claimed;

    public int id;
}
