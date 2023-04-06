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

    private Dictionary<int, AchievementSO> achievements = new Dictionary<int, AchievementSO>();

    void Start()
    {
        for (int i = 0; i < achievementsSO.Length; i++)
        {
            achievements.Add(achievementsSO[i].id, achievementsSO[i]);
            achievementTemplatesSO[i].SetActive(true);
        }

        LoadTemplates();
        SeedsPlantedAchievement(0);

        achievementsIsActive = achievementsObject.activeSelf;
    }

    void Update()
    {
        if (achievementsIsActive != achievementsObject.activeSelf)
        {
            achievementsIsActive = achievementsObject.activeSelf;
        }
    }

    public void SeedsPlantedAchievement(int achievementId)
    {
        AchievementSO achievement = achievements[achievementId];
        achievement.currentAmount++;

        achievementTemplates[achievementId].currentAmountTxt.text = achievement.currentAmount.ToString();
        achievementTemplates[achievementId].claimBtn.SetActive(false);

        if (achievement.currentAmount >= achievement.requiredAmount)
        {
            achievementTemplates[achievementId].claimBtn.SetActive(true);
        }

        //achievementTemplates[0].currentAmount.text = achievementsSO[0].currentAmount.ToString();
        //achievementTemplates[0].claimBtn.SetActive(false);

        /*if (achievementsSO[0].currentAmount >= achievementsSO[0].requiredAmount)
        {           
            Debug.Log("Achievement unlocked: Plant 50 seeds");
            achievementTemplates[0].claimBtn.SetActive(true);
        }*/
    }

    private void LoadTemplates()
    {
        for (int i = 0; i < achievementsSO.Length; i++)
        {
            AchievementSO achievement = achievements[achievementTemplates[i].achievement.id];

            achievementTemplates[i].requiredAmountTxt.text = achievementsSO[i].requiredAmount.ToString();
            //achievementTemplates[i].currentAmount.text = achievementsSO[i].currentAmount.ToString();
            achievementTemplates[i].rewardAmountTxt.text = achievementsSO[i].rewardAmount.ToString();
            achievementTemplates[i].descriptionTxt.text = achievementsSO[i].description;
        }
    }
}
