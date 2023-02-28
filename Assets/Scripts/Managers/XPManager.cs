using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class XPManager : MonoBehaviour
{
    public TextMeshProUGUI currentXPTxt, currentXPTxt1, targetXPTxt, targetXPTxt1, levelTxt, levelTxt1, levelTxt2;
    public int currentXP, targetXP, level;

    public static XPManager instance;

    [SerializeField]
    TempLoadSave saveManager;

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
        currentXPTxt1.text = currentXP.ToString();
        targetXPTxt.text = targetXP.ToString();
        targetXPTxt1.text = targetXP.ToString();
        levelTxt.text = "Lvl. " + level.ToString();
        levelTxt1.text = "Lvl. " + level.ToString();
        levelTxt2.text = "Lvl. " + level.ToString();
    }

    public void AddXP(int xp)
    {
        currentXP += xp/level;

        // Level Up
        while(currentXP >= targetXP)
        {
            currentXP = currentXP - targetXP;
            level++;
            // Target XP higher each time player levels up
            targetXP += targetXP / 20;

            levelTxt.text = "Lvl. " + level.ToString();
            levelTxt1.text = "Lvl. " + level.ToString();
            levelTxt2.text = "Lvl. " + level.ToString();
            targetXPTxt.text = targetXP.ToString();
            targetXPTxt1.text = targetXP.ToString();
        }

        currentXPTxt.text = currentXP.ToString();
        currentXPTxt1.text = currentXP.ToString();
        saveManager.xpChanged.Invoke(currentXP, level);

    }
}
