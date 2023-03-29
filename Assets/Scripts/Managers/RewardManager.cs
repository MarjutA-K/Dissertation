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
    }

    public void Check()
    {
        for(int i = 0; i < rewardsSO.Length; i++)
        {
            if(TestPedometer.instance.stepCount >= rewardsSO[i].stepAmount)
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
        if(TestPedometer.instance.stepCount >= rewardsSO[btnNum].stepAmount)
        {
            Check();
            Debug.Log("Got reward");
        }
    }

    private void LoadTemplates()
    {
        for(int i = 0; i < rewardsSO.Length; i++)
        {
            rewardTemplates[i].image.sprite = rewardsSO[i].icon;
            rewardTemplates[i].stepAmountTxt.text = rewardsSO[i].stepAmount.ToString();
            rewardTemplates[i].rewardAmountText.text = rewardsSO[i].rewardAmount.ToString();

            if(rewardsSO[i].stepAmount >= 3000)
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
