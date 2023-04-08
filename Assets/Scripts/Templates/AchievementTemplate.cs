using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AchievementTemplate : MonoBehaviour
{
    public AchievementSO achievement;
    public TMP_Text descriptionTxt;
    public TMP_Text requiredAmountTxt;
    public TMP_Text currentAmountTxt;
    public TMP_Text rewardAmountTxt;
    public Slider slider;
    public GameObject currentAmount;
    public GameObject requireAmount;
    public GameObject slash;
    public GameObject completed;
}
