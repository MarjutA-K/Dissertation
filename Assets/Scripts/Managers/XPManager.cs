using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class XPManager : MonoBehaviour
{
    public TextMeshProUGUI currentXPTxt, targetXPTxt, levelTxt;
    public int currentXP, targetXP, level;

    public static XPManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        currentXPTxt.text = currentXP.ToString();
        targetXPTxt.text = targetXP.ToString();
        levelTxt.text = "Lvl. " + level.ToString();
    }

    public void AddXP(int xp)
    {
        currentXP += xp;

        // Level Up
        while(currentXP >= targetXP)
        {
            currentXP = currentXP - targetXP;
            level++;
            // Target XP higher each time player levels up
            targetXP += targetXP / 20;

            levelTxt.text = "Lvl. " + level.ToString();
            targetXPTxt.text = targetXP.ToString();
        }

        currentXPTxt.text = currentXP.ToString();
    }
}
