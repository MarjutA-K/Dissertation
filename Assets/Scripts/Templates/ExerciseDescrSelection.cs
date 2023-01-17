using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ExerciseDescrSelection : MonoBehaviour
{
    public ExerciseTemplate selectExercise;
    public GameObject exerciseDescription;

    private bool isTrue = false;
    private bool isSelecting = false;
    private int selectedTool = 0;

    public void SelectTool(int toolNumber)
    {
        if (toolNumber == selectedTool)
        {
            //deselect
            CheckSelection();
        }
        else
        {
            //select tool number and check to see if anything was also selected
            CheckSelection();
            isSelecting = true;
            selectedTool = toolNumber;
        }
    }

    public void SelectExercise(ExerciseTemplate newExercise)
    {
        if (selectExercise == newExercise)
        {
            CheckSelection();
        }
        else
        {
            CheckSelection();
            selectExercise = newExercise;
            isTrue = true;
            exerciseDescription.SetActive(true);
            selectExercise.exercise.description.SetActive(true);
            Instantiate(selectExercise.exercise.description, transform);
        }
    }

    private void CheckSelection()
    {
        if (isTrue)
        {
            isTrue = false;
            if (selectExercise != null)
            {
                selectExercise.exercise.description.SetActive(false);
                selectExercise = null;
            }
        }

        if (isSelecting)
        {
            isSelecting = false;
            selectedTool = 0;
        }


    }
}