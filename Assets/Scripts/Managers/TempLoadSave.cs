using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class TempLoadSave : MonoBehaviour
{


    [SerializeField] XPManager xpManager;
    [SerializeField] GardenManager gardenManager;
    int streak;
    void Awake()
    {

        
        JsonSerialize myData =JsonUtility.FromJson<JsonSerialize>(File.ReadAllText("JsonData/SaveData.json"));

        xpManager.level = myData.level;
        xpManager.currentXP = myData.exp;
        gardenManager.money = myData.money;
        streak = myData.streak;


        System.DateTime oldTime = System.DateTime.FromBinary(myData.lastOnlineDate);
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

    void Start()
    {
        //string exercisedata =JsonUtility.ToJson(myExercise);

       //JsonSerialize OtherObject = JsonUtility.FromJson<JsonSerialize>(File.ReadAllText("JsonData/SaveData.json"));
      
        //Debug.Log(OtherObject.money);
        

     

    }
    private void OnDestroy()
    {
    
        
    JsonSerialize endData = new JsonSerialize();
        endData.exp = xpManager.currentXP;
        endData.level = xpManager.level;
        endData.money = gardenManager.money;
        //endData.lastOnlineDate = 
        endData.lastOnlineDate = System.DateTime.Now.ToBinary();
        endData.streak = streak;
       string myData= JsonUtility.ToJson(endData);
        File.WriteAllText("JsonData/SaveData.json", myData);
  
        Debug.Log("Data Saved");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
