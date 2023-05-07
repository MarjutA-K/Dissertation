using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

public class LoadSave : MonoBehaviour
{
    [SerializeField] private XPManager xpManager;
    [SerializeField] private ShopManager shopManager;

    [SerializeField] private string saveFileName = "JsonData/SaveData.json";

    public UnityEvent<int, int> xpChanged;
    public UnityEvent<int> moneyChanged;
    public UnityEvent<int> diamondsChanged;

    SaveData saveData;

    int streak;

    void Awake()
    {
        xpChanged.AddListener(XpChangedEvent);
        moneyChanged.AddListener(MoneyChangedEvent);
        diamondsChanged.AddListener(DiamondsChangedEvent);

        LoadGameData();              
    }

    private void OnDestroy()
    {
        SaveGameData();
        Debug.Log("Data Saved");
    }

    private void LoadGameData()
    {
        string saveDataJson = File.ReadAllText(saveFileName);
        saveData = JsonUtility.FromJson<SaveData>(saveDataJson);

        xpManager.currentXP = saveData.currentXP;
        xpManager.level = saveData.level;
        shopManager.money = saveData.money;
        shopManager.diamonds = saveData.diamonds;
        CalculateStreak();

    }

    private void SaveGameData()
    {
        saveData.currentXP = xpManager.currentXP;
        saveData.level = xpManager.level;
        saveData.money = shopManager.money;
        saveData.diamonds = shopManager.diamonds;
        saveData.lastLoginTimestamp = System.DateTime.Now.ToBinary();
        saveData.streak = streak;

        string saveDataJson = JsonUtility.ToJson(saveData);
        File.WriteAllText(saveFileName, saveDataJson);
    }

    private void CalculateStreak()
    {
        System.DateTime lastLoginTime = System.DateTime.FromBinary(saveData.lastLoginTimestamp);
        System.TimeSpan timeSinceLastLogin = System.DateTime.Now - lastLoginTime;

        if (timeSinceLastLogin.TotalDays < 1)
        {
            Debug.Log("Current streak: " + streak);
        }
        else if(timeSinceLastLogin.TotalDays >= 1.0 && timeSinceLastLogin.TotalDays < 2.0)
        {
            streak++;
            Debug.Log("Streak increased to: " + streak);
        }
        else
        {
            streak = 0;
            Debug.Log("Streak reset");
        }
    }

    public void XpChangedEvent(int xp, int level)
    {
        saveData.currentXP = xp;
        saveData.level = level;
    }
    public void MoneyChangedEvent(int _money)
    {
        saveData.money = _money;
    }

    public void DiamondsChangedEvent(int _diamonds)
    {
        saveData.diamonds = _diamonds;
    }
}
