using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementManager : MonoBehaviour
{
    public AchievementSO[] achievementsSO;
    public AchievementTemplate[] achievementTemplates;
    public GameObject[] achievementTemplatesSO;
    public Button[] achievementBtns;

    public GameObject achievementsObject;
    private bool achievementsIsActive;

    public PlantSO[] plant;

    private Dictionary<int, AchievementSO> achievements = new Dictionary<int, AchievementSO>();

    void Start()
    {
        for (int i = 0; i < achievementsSO.Length; i++)
        {
            achievements.Add(achievementsSO[i].id, achievementsSO[i]);
            achievementTemplatesSO[i].SetActive(true);
        }

        LoadTemplates();

        achievementsIsActive = achievementsObject.activeSelf;
    }

    void Update()
    {
        if (achievementsIsActive != achievementsObject.activeSelf)
        {
            achievementsIsActive = achievementsObject.activeSelf;
        }

        MoneyAchievement(4);
        CheckBtns();
    }

    private void LoadTemplates()
    {
        for (int i = 0; i < achievementsSO.Length; i++)
        {
            AchievementSO achievement = achievements[achievementTemplates[i].achievement.id];

            achievementTemplates[i].requiredAmountTxt.text = achievementsSO[i].requiredAmount.ToString();
            achievementTemplates[i].currentAmountTxt.text = achievementsSO[i].currentAmount.ToString();
            achievementTemplates[i].rewardAmountTxt.text = achievementsSO[i].rewardAmount.ToString();
            achievementTemplates[i].descriptionTxt.text = achievementsSO[i].description;
            achievementsSO[i].currentAmount = 0;
            achievementTemplates[i].completed.SetActive(false);
            achievementsSO[i].claimed = false;
        }
    }

    private void CheckBtns()
    {
        for(int i = 0; i < achievementsSO.Length; i++)
        {
            if(achievementsSO[i].currentAmount >= achievementsSO[i].requiredAmount && !achievementsSO[i].claimed)
            {
                achievementBtns[i].gameObject.SetActive(true);
            }
            else
            {
                achievementBtns[i].gameObject.SetActive(false);
            }
        }
    }

    public void Getreward(int achievementId)
    {
        AchievementSO achievement = achievements[achievementId];
        if (achievement.currentAmount >= achievement.requiredAmount && !achievement.claimed)
        {
            CheckBtns();
            achievement.claimed = true;
            achievementBtns[achievementId].gameObject.SetActive(false);
            achievementTemplates[achievementId].completed.SetActive(true);
            Debug.Log("Got reward " + achievementId);
        }
    }

    public void CheckAchievement(int achievementId)
    {
        AchievementSO achievement = achievements[achievementId];
        achievement.currentAmount++;    

        achievementTemplates[achievementId].currentAmountTxt.text = achievement.currentAmount.ToString();
        achievementTemplates[achievementId].slider.maxValue = achievement.requiredAmount;
        achievementTemplates[achievementId].slider.value = achievement.currentAmount;

        if (achievement.currentAmount >= achievement.requiredAmount && !achievementsSO[achievementId].claimed)
        {
            achievementTemplates[achievementId].currentAmount.SetActive(false);
            achievementTemplates[achievementId].slash.SetActive(false);
            achievementTemplates[achievementId].requireAmount.SetActive(false);
        }
    }

    public void UnlockItemsAchievement(int achievementId)
    {
        AchievementSO achievement = achievements[achievementId];       
        XPManager xp = FindObjectOfType<XPManager>();

        achievement.currentAmount = 0;

        for(int i = 0; i < plant.Length; i++)
        {
            if (xp.level >= plant[i].level && achievement.currentAmount < achievement.requiredAmount)
            {
                achievement.currentAmount++;
                achievementTemplates[achievementId].currentAmountTxt.text = achievement.currentAmount.ToString();
                achievementTemplates[achievementId].slider.maxValue = achievement.requiredAmount;
                achievementTemplates[achievementId].slider.value = achievement.currentAmount;

                if (achievement.currentAmount >= achievement.requiredAmount && !achievementsSO[achievementId].claimed)
                {
                    achievementTemplates[achievementId].currentAmount.SetActive(false);
                    achievementTemplates[achievementId].slash.SetActive(false);
                    achievementTemplates[achievementId].requireAmount.SetActive(false);
                }
            }              
        }
    }

    public void MoneyAchievement(int achievementId)
    {
        AchievementSO achievement = achievements[achievementId];
        ShopManager shopManager = FindObjectOfType<ShopManager>();

        achievement.currentAmount = shopManager.money;
        achievementTemplates[achievementId].currentAmountTxt.text = achievement.currentAmount.ToString();
        achievementTemplates[achievementId].slider.maxValue = achievement.requiredAmount;
        achievementTemplates[achievementId].slider.value = achievement.currentAmount;

        if (achievement.currentAmount >= achievement.requiredAmount && !achievementsSO[achievementId].claimed)
        {
            achievementTemplates[achievementId].currentAmount.SetActive(false);
            achievementTemplates[achievementId].slash.SetActive(false);
            achievementTemplates[achievementId].requireAmount.SetActive(false);
        }
    }
}
