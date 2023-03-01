using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

public class TempLoadSave : MonoBehaviour
{
    [SerializeField] XPManager xpManager;
    [SerializeField] ShopManager shopManager;
    int streak;
    GameObject []plots;
    public UnityEvent<int, int> xpChanged;
    public UnityEvent<int> moneyChanged;
    JsonSerialize saveData;
    List<ShopPlantItemSO> plantObjects;

    JsonSerialize.plotWrapper plotSaveData;
    void Awake()
    {
        plantObjects = new List<ShopPlantItemSO>();
        xpChanged.AddListener(xpChangedEvent);
        moneyChanged.AddListener(moneyChangedEvent);
        
        saveData =JsonUtility.FromJson<JsonSerialize>(File.ReadAllText("JsonData/SaveData.json"));
        xpManager.currentXP = saveData.exp;
        xpManager.level = saveData.level;
        shopManager.money = saveData.money;
        streak = saveData.streak;
       

        plotSaveData = new JsonSerialize.plotWrapper();
        plotSaveData.shopPlantItem = new List<plotData>();
       
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

        var loadPlants = Resources.LoadAll("ScriptableObjects", typeof(ShopPlantItemSO));
        foreach (var plant in loadPlants)
        {
            plantObjects.Add((ShopPlantItemSO)plant);
        }
        int i = 0;
        plots = GameObject.FindGameObjectsWithTag("grid");
        foreach (GameObject Plot in plots)
        {
            PlotManager current = Plot.GetComponent<PlotManager>();
            current.plotChangedEvent.AddListener(saveGridData);
            //current.plotChangedEvent.AddListener();
            //Debug.Log("Grid: " + i + " at position:" + Plot.transform.position + " currently planted: " + current.isPlanted);
            ++i;
        }
        if(saveData.savedPlants.shopPlantItem != null)
        {
            foreach (plotData plot in saveData.savedPlants.shopPlantItem)
            {
                Debug.Log("plot: " + plot.plotnum + "plant: " + plot.plantname);
            }

        }




        plots = GameObject.FindGameObjectsWithTag("grid");
  
    }

    void Start()
    {
        //string exercisedata =JsonUtility.ToJson(myExercise);

       //JsonSerialize OtherObject = JsonUtility.FromJson<JsonSerialize>(File.ReadAllText("JsonData/SaveData.json"));
      
        //Debug.Log(OtherObject.money);
    }
    private void OnDisable()
    {
          
      
    }

    private void OnDestroy()
    {
    JsonSerialize endData = new JsonSerialize();
        endData.savedPlants = new JsonSerialize.plotWrapper();
        endData.exp = saveData.exp;
        endData.level = saveData.level;
        endData.money = saveData.money;
        //endData.lastOnlineDate = 
        endData.lastOnlineDate = System.DateTime.Now.ToBinary();
        endData.streak = streak;
        endData.savedPlants = plotSaveData;
        string myData= JsonUtility.ToJson(endData);
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
    void saveGridData()
    {
        int i = 0;
        plots = GameObject.FindGameObjectsWithTag("grid");
        foreach (GameObject Plot in plots)
        {
            PlotManager current = Plot.GetComponent<PlotManager>();
            if (current.isPlanted)
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
