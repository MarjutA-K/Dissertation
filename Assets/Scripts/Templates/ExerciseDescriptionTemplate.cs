using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ExerciseDescriptionTemplate : MonoBehaviour
{
    public ExerciseSO exercise;

    public TMP_Text descriptionTxt;


    private void Start()
    {
        InitializeUI();
    }

    void InitializeUI()
    {
        descriptionTxt.text = exercise.exerciseDescription;
    }

}