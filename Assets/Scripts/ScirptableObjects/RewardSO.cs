using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Reward", menuName = "Rewards")]
public class RewardSO : ScriptableObject
{
    public int stepAmount;
    public int rewardAmount;
    public Sprite icon;
}
