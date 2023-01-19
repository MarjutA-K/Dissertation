using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OpenTabs : MonoBehaviour
{
    OpenTabs instance;

    public GameObject shop;
    public GameObject activities;
    public GameObject workout;
    public GameObject exerciseDescription;
    public GameObject exerciseTimer;
    public GameObject congratsMsg;

    public bool interactable;

    private void Awake()
    {
        if (instance == null)
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
        interactable = true;
    }

    public void OpenShop()
    {
        if (shop != null)
        {
            shop.SetActive(true);
        }
    }

    public void CloseShop()
    {
        shop.SetActive(false);
    }

    public void OpenActivities()
    {
        if (activities != null)
        {
            activities.SetActive(true);
            interactable = false;
        }
    }

    public void CloseActivities()
    {
        activities.SetActive(false);
        interactable = true;
    }

    public void OpenWorkoutView()
    {
        if(workout != null)
        {
            workout.SetActive(true);
        }
        
    }

    public void CloseWorkoutView()
    {
        workout.SetActive(false);
    }

    public void OpenExerciseDescr()
    {
        exerciseDescription.SetActive(true);
    }

    public void CloseExerciseDescr()
    {
        exerciseDescription.SetActive(false);
    }

    public void OpenExerciseTimerView()
    {
        exerciseTimer.SetActive(true);
    }

    public void CloseExerciseTimerView()
    {
        exerciseTimer.SetActive(false);
    }

    public void showCongratsMsg()
    {
        congratsMsg.SetActive(true);
    }

    public void CloseCongratsMsg()
    {
        congratsMsg.SetActive(false);
    }
}
