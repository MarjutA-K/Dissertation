/* XP Manager was build using the following tutorial: https://www.youtube.com/watch?v=57sak_5DJbM */

using UnityEngine;
using TMPro;

public class XPManager : MonoBehaviour
{
    public TextMeshProUGUI currentXPTxt, targetXPTxt, levelTxt, levelTxt1, levelTxt2, rewardTxt;
    public int currentXP, targetXP, level;
    public int reward;

    public static XPManager instance;
    private ShopManager shopManager;
    [SerializeField] LoadSave saveManager;

    public GameObject levelReachedNote;

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
        levelTxt.text = level.ToString();

        levelReachedNote.SetActive(false);

        reward = 50;

        shopManager = FindObjectOfType<ShopManager>();
    }

    public void CloseNote()
    {
        levelReachedNote.SetActive(false);
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
            reward *= 2;
            shopManager.money = shopManager.money + reward;
            shopManager.moneyTxt.text = shopManager.money.ToString();

            levelTxt.text = level.ToString();
            levelTxt1.text = level.ToString();
            levelTxt2.text = "YOU'VE REACHED LEVEL " + level.ToString();
            rewardTxt.text = reward.ToString();
            targetXPTxt.text = targetXP.ToString();

            levelReachedNote.SetActive(true);

            saveManager.moneyChanged.Invoke(shopManager.money);
        }
        currentXPTxt.text = currentXP.ToString();
        saveManager.xpChanged.Invoke(currentXP, level);
    }
}
