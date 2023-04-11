using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlantSO activePlant;
    public ShopManager shopManager;

    private void Start()
    {
        shopManager = FindObjectOfType<ShopManager>();
    }

    private void Update()
    {
        activePlant = InventoryManager.instance.GetSelectedPlant(false);
    }

    public void AddXP()
    {
        XPManager.instance.AddXP(100);
    }

    public void AddMoney()
    {
        shopManager.AddMoney(50);
    }

    public void AddRewardMoney(RewardSO reward)
    {
        shopManager.AddMoney(reward.rewardAmount);
    }

    public void AddRewardDiamonds(RewardSO reward)
    {
        shopManager.AddDiamonds(reward.rewardAmount);
    }

    public void AddAchievementReward(AchievementSO reward)
    {
        shopManager.AddDiamonds(reward.rewardAmount);
    }

    public void OrderReward(PlantOrdersSO reward)
    {
        shopManager.AddDiamonds(reward.rewardAmount);
    }
}
