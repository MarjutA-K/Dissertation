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
            achievementsSO[i].currentAmount = 0;
            achievementTemplates[i].claimBtn.SetActive(false);
        }

        LoadTemplates();
        //SeedsPlantedAchievement(0);

        achievementsIsActive = achievementsObject.activeSelf;
    }

    void Update()
    {
        if (achievementsIsActive != achievementsObject.activeSelf)
        {
            achievementsIsActive = achievementsObject.activeSelf;
        }

        MoneyAchievement(4);
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
        }
    }

    public void Getreward(int achievementId)
    {
        AchievementSO achievement = achievements[achievementId];
        if (achievement.currentAmount >= achievement.requiredAmount && !achievement.claimed)
        {
            achievement.claimed = true;
            //achievementTemplates[achievementId].claimBtn.SetActive(false);
            achievementBtns[achievementId].gameObject.SetActive(false);
            Debug.Log("Got reward " + achievementId);
        }
    }

    public void CheckAchievement(int achievementId)
    {
        AchievementSO achievement = achievements[achievementId];
        achievement.currentAmount++;    

        achievementTemplates[achievementId].currentAmountTxt.text = achievement.currentAmount.ToString();

        if (achievement.currentAmount >= achievement.requiredAmount && !achievementsSO[achievementId].claimed)
        {
            //achievementTemplates[achievementId].claimBtn.SetActive(true);
            achievementBtns[achievementId].gameObject.SetActive(true);
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

                if (achievement.currentAmount >= achievement.requiredAmount && !achievementsSO[achievementId].claimed)
                {
                    //achievementTemplates[achievementId].claimBtn.SetActive(true);
                    achievementBtns[achievementId].gameObject.SetActive(true);
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

        if (achievement.currentAmount >= achievement.requiredAmount && !achievementsSO[achievementId].claimed)
        {
            //achievementTemplates[achievementId].claimBtn.SetActive(true);
            achievementBtns[achievementId].gameObject.SetActive(true);
            achievementTemplates[achievementId].currentAmount.SetActive(false);
            achievementTemplates[achievementId].slash.SetActive(false);
            achievementTemplates[achievementId].requireAmount.SetActive(false);
        }
    }
}
