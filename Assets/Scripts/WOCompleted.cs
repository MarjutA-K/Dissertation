using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WOCompleted : MonoBehaviour
{
    public GameObject checkmark;
    public Button startWOBtn;

    public bool isWOCompleted;

    PlotManager pm;

    // Start is called before the first frame update
    void Start()
    {
        pm = FindObjectOfType<PlotManager>();
        checkmark.SetActive(false);
        isWOCompleted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isWOCompleted)
        {
            checkmark.SetActive(true);
            startWOBtn.interactable = false;
        }
    }
}
