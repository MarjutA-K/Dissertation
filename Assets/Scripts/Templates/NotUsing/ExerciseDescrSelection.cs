using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ExerciseDescrSelection : MonoBehaviour
{
    private ExerciseTemplate selectExercise;
    private GameObject clone;

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
            selectExercise.exercise.description.SetActive(true);
            clone = Instantiate(selectExercise.exercise.description, transform);
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
                Destroy(clone);
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