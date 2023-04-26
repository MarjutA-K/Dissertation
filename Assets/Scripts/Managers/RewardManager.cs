using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardManager : MonoBehaviour
{
    public RewardSO[] rewardsSO;
    public RewardTemplate[] rewardTemplates;
    public GameObject[] rewardTemplatesSO;
    public Button[] rewardBtns;

    void Start()
    {
        for(int i = 0; i < rewardsSO.Length; i++)
        {
            rewardTemplatesSO[i].SetActive(true);
        }

        LoadTemplates();
    }

    void Update()
    {
        Check();
        LoadTemplates();
    }

    public void Check()
    {
        for(int i = 0; i < rewardsSO.Length; i++)
        {
            if(StepTracker.instance.stepCount >= rewardsSO[i].stepAmount)
            {
                rewardBtns[i].interactable = true;
            }
            else
            {
                rewardBtns[i].interactable = false;
            }
        }
    }

    public void GetReward(int btnNum)
    {
        if(StepTracker.instance.stepCount >= rewardsSO[btnNum].stepAmount)
        {
            Check();
            rewardBtns[btnNum].interactable = false;
            StepTracker.instance.stepCount -= rewardsSO[btnNum].stepAmount;
            StepTracker.instance.stepsTxt.text = StepTracker.instance.stepCount.ToString();

            if (StepTracker.instance.stepCount >= 1000)
            {
                int amountInK = StepTracker.instance.stepCount / 1000;
                StepTracker.instance.stepsTxt.text = amountInK.ToString("0.#") + "K";
            }
            else
            {
                StepTracker.instance.stepsTxt.text = StepTracker.instance.stepCount.ToString();
            }
        }
    }

    private void LoadTemplates()
    {
        for(int i = 0; i < rewardsSO.Length; i++)
        {
            rewardTemplates[i].image.sprite = rewardsSO[i].icon;
            rewardTemplates[i].stepAmountTxt.text = rewardsSO[i].stepAmount.ToString();
            rewardTemplates[i].rewardAmountText.text = rewardsSO[i].rewardAmount.ToString();

            if(rewardsSO[i].stepAmount >= 1000)
            {
                int amountInK = rewardsSO[i].stepAmount / 1000;
                rewardTemplates[i].stepAmountTxt.text = amountInK.ToString("0.#") + "K";
            }
            else
            {
                rewardTemplates[i].stepAmountTxt.text = rewardsSO[i].stepAmount.ToString();
            }
        }
    }
}
