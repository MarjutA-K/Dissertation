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
        CalculateStreak();            
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
        else if(timeSinceLastLogin.TotalDays >= 1 && timeSinceLastLogin.TotalDays < 2)
        {
            streak++;
            Debug.Log("Streak increased to: " + streak);
        }
        else
        {
            streak = 0;
            Debug.Log("Streak reset");
        }

        //Debug.Log("Current time " + currentTime.ToString());

        //System.DateTime oldTime = System.DateTime.FromBinary(saveData.lastOnlineDate);
        //System.DateTime currentTime = System.DateTime.Today.AddDays(2.0f);
        //System.DateTime currentTime = System.DateTime.Now;
        //double timeElapsedinDays = (currentTime - oldTime).TotalDays;

        /*if (timeElapsedinDays < 1)
        {
            Debug.Log((currentTime - oldTime).TotalDays.ToString());
        }
        else if (timeElapsedinDays > 1 && timeElapsedinDays < 2)
        {
            streak++;
        }
        else streak = 0;*/
        //Debug.Log(timeElapsedinDays.ToString());
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


/*public class LoadSave : MonoBehaviour
{
    [SerializeField] XPManager xpManager;
    [SerializeField] ShopManager shopManager;

    //GameObject []plots;
    public UnityEvent<int, int> xpChanged;
    public UnityEvent<int> moneyChanged;
    public UnityEvent<int> diamondsChanged;
    JsonSerialize saveData;

    //List<PlantSO> plantObjects;

    int streak;

    //JsonSerialize.plotWrapper plotSaveData;

    void Awake()
    {
        //plantObjects = new List<PlantSO>();
        xpChanged.AddListener(xpChangedEvent);
        moneyChanged.AddListener(moneyChangedEvent);
        diamondsChanged.AddListener(DiamondsChangedEvent);

        saveData = JsonUtility.FromJson<JsonSerialize>(File.ReadAllText("JsonData/SaveData.json"));
        xpManager.currentXP = saveData.exp;
        xpManager.level = saveData.level;
        shopManager.money = saveData.money;
        shopManager.diamonds = saveData.diamonds;
        streak = saveData.streak;


        //plotSaveData = new JsonSerialize.plotWrapper();
        //plotSaveData.shopPlantItem = new List<plotData>();

        System.DateTime oldTime = System.DateTime.FromBinary(saveData.lastOnlineDate);
        // System.DateTime currentTime = System.DateTime.Today.AddDays(2.0f);
        System.DateTime currentTime = System.DateTime.Now;
        double timeElapsedinDays = (currentTime - oldTime).TotalDays;

        if (timeElapsedinDays < 1)
        {
            Debug.Log((currentTime - oldTime).TotalDays.ToString());
        }
        else if (timeElapsedinDays > 1 && timeElapsedinDays < 2)
        {
            streak++;
        }
        else streak = 0;
        Debug.Log(timeElapsedinDays.ToString());

        Debug.Log(currentTime.ToString());
        Debug.Log(streak);
    }

    private void OnDestroy()
    {
        JsonSerialize endData = new JsonSerialize();
        //endData.savedPlants = new JsonSerialize.plotWrapper();

        endData.exp = saveData.exp;
        endData.level = saveData.level;
        endData.money = saveData.money;
        endData.diamonds = saveData.diamonds;

        //endData.lastOnlineDate = 
        endData.lastOnlineDate = System.DateTime.Now.ToBinary();
        endData.streak = streak;

        //endData.savedPlants = plotSaveData;
        string myData = JsonUtility.ToJson(endData);
        File.WriteAllText("JsonData/SaveData.json", myData);

        Debug.Log("Data Saved");
    }

    void xpChangedEvent(int xp, int level)
    {
        saveData.exp = xp;
        saveData.level = level;
    }
    void moneyChangedEvent(int _money)
    {
        saveData.money = _money;
    }

    void DiamondsChangedEvent(int _diamonds)
    {
        saveData.diamonds = _diamonds;
    }

    /*void saveGridData()
    {
        int i = 0;
        plots = GameObject.FindGameObjectsWithTag("grid");
        foreach (GameObject Plot in plots)
        {
            //PlotManager current = Plot.GetComponent<PlotManager>();
            /*if (current.isPlanted)
            {
                plotData tempdata = new plotData();
                tempdata.plotnum = i;
                tempdata.plantname = current.selectedPlant.name;
                plotSaveData.shopPlantItem.Add(tempdata);
                Debug.Log(current.selectedPlant.plantTitle);
            }
            //current.plotChangedEvent.AddListener();
            //Debug.Log("Grid: " + i + " at position:" + Plot.transform.position + " currently planted: " + current.isPlanted);
            ++i;
        }
    }
}*/
