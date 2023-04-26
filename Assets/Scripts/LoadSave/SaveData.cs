using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int money;
    public int diamonds;
    public int level;
    public int currentXP;
    public int streak;
    public long lastLoginTimestamp;
}


/*[System.Serializable]
public class JsonSerialize
{
    public int money;
    public int diamonds;
    public int level;
    public int exp;
    public int streak;
    public long lastOnlineDate;

    //[System.Serializable]
    //public class plotWrapper : plotArrayWrapper<plotData> { };
    //public plotWrapper savedPlants;
}

/*[System.Serializable]
public class plotData
{
    public int plotnum;
    public string plantname;
}*/
