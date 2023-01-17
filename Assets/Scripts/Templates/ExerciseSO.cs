using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New exercise", menuName = "Exercise")]
public class ExerciseSO : ScriptableObject
{
    public string exerciseTitle;
    public Sprite btnImage;
    public int exerciseOrder;
    public GameObject description;
}
