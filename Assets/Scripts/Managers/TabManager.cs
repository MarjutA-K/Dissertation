using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TabManager : MonoBehaviour
{
    TabManager instance;

    public GameObject shop;
    public GameObject activities;
    public GameObject workout;
    public GameObject exerciseDescription;
    public GameObject exerciseTimer;
    public GameObject congratsMsg;
    public GameObject inventory;
    public GameObject steps;
    public GameObject orders;
    public GameObject achievements;

    //public bool interactable;

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
        //interactable = true;
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

    /*public void OpenActivities()
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
    }*/

    public void OpenInventory()
    {
        if (inventory != null)
        {
            inventory.SetActive(true);
        }
    }

    public void CloseInventory()
    {
        inventory.SetActive(false);
    }

    public void OpenSteps()
    {
        if(steps != null)
        {
            steps.SetActive(true);
        }
    }

    public void CloseSteps()
    {
        steps.SetActive(false);
    }

    public void OpenOrders()
    {
        if (orders != null)
        {
            orders.SetActive(true);
        }
    }

    public void CloseOrders()
    {
        orders.SetActive(false);
    }

    public void OpenAchievements()
    {
        if (achievements != null)
        {
            achievements.SetActive(true);
        }
    }

    public void CloseAchievements()
    {
        achievements.SetActive(false);
    }
}
