using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ExerciseTemplate : MonoBehaviour
{
    public ExerciseSO exercise;

    public TMP_Text titleTxt;
    public Image btnImage;
    public TMP_Text descriptionTxt;

    ExerciseDescrSelection edt;

    private void Start()
    {
        edt = FindObjectOfType<ExerciseDescrSelection>();
        InitializeUI();
    }

    public void Selected()
    {
        edt.SelectExercise(this);
        Debug.Log(exercise.exerciseTitle);
    }

    void InitializeUI()
    {
        titleTxt.text = exercise.exerciseTitle;
        btnImage.sprite = exercise.btnImage;
    }
}
