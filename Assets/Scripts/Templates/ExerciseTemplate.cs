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

    private void Start()
    {
        InitializeUI();
    }

    void InitializeUI()
    {
        titleTxt.text = exercise.exerciseTitle;
        btnImage.sprite = exercise.btnImage;
    }
}
