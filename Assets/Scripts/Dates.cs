using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Dates : MonoBehaviour
{    
    string today;
    DateTime lastPressedDate;
    public Button btn;
    bool buttonPressed;
   
    // Start is called before the first frame update
    void Start()
    {
        today = "lastButtonPressDate";
        //lastPressedDate = DateTime.Parse(today);
        buttonPressed = false;
        Debug.Log(today);
    }

    // Update is called once per frame
    void Update()
    {

        //testing();

        if (lastPressedDate.Date != DateTime.Now.Date && buttonPressed)
        {
            btn.interactable = false;
            Debug.Log("lastButtonPressedDate" + DateTime.Now.ToString());
            //PlayerPrefs.SetString("lastButtonPressedDate", DateTime.Now.ToString());
        }
    }

    public void testing()
    {
           buttonPressed = true;
    }

}
