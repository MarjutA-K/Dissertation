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

    void Start()
    {
        for (int i = 0; i < achievementsSO.Length; i++)
        {
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
    }

    private void LoadTemplates()
    {
        for (int i = 0; i < achievementsSO.Length; i++)
        {
            achievementTemplates[i].requiredAmountTxt.text = achievementsSO[i].requiredAmount.ToString();
            achievementTemplates[i].maxAmountText.text = achievementsSO[i].maxAmount.ToString();
            achievementTemplates[i].rewardAmountTxt.text = achievementsSO[i].rewardAmount.ToString();            
        }
    }
}
