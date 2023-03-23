using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExerciseManager : MonoBehaviour
{
    public GameObject exercise;

    List<ExerciseSO> exerciseObjects = new List<ExerciseSO>();

    private void Awake()
    {
        //Assets/Resources/Exercises
        var loadExercises = Resources.LoadAll("ScriptableObjects", typeof(ExerciseSO));
        foreach (var _exercise in loadExercises)
        {
            exerciseObjects.Add((ExerciseSO)_exercise);
        }
        exerciseObjects.Sort(SortByOrder);

        foreach (var _exercise in exerciseObjects)
        {
            ExerciseTemplate newExercise = Instantiate(exercise, transform).GetComponent<ExerciseTemplate>();
            newExercise.exercise = _exercise;
        }
    }

    int SortByOrder(ExerciseSO exercise1, ExerciseSO exercise2)
    {
        return exercise1.exerciseOrder.CompareTo(exercise2.exerciseOrder);
    }
}
