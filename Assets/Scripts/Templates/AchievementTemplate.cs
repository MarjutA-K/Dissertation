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
    public GameObject claimBtn;
}
