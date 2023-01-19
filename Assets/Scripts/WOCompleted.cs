using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WOCompleted : MonoBehaviour
{
    public GameObject checkmark;
    public GameObject complitedTxt;
    public GameObject descriptionTxt;
    public Button startWOBtn;

    public bool isWOCompleted;

    // Start is called before the first frame update
    void Start()
    {
        checkmark.SetActive(false);
        isWOCompleted = false;
        complitedTxt.SetActive(false);
        descriptionTxt.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(isWOCompleted)
        {
            checkmark.SetActive(true);
            startWOBtn.interactable = false;
            complitedTxt.SetActive(true);
            descriptionTxt.SetActive(false);
        }
    }
}
