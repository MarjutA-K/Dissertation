using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OpenTabs : MonoBehaviour
{
    public GameObject shop;
    public GameObject activities;
    public GameObject workout;
    public GameObject exerciseDescription;
    public GameObject exerciseTimer;


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
        }
    }

    public void CloseActivities()
    {
        activities.SetActive(false);
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
}
