using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlantItemTemplate : MonoBehaviour
{
    [HideInInspector]public PlantSO plant;

    public TMP_Text titleTxt;
    public TMP_Text priceTxt;
    public TMP_Text btnTxt;
    public TMP_Text stepsToGrowTxt;

    public GameObject stepsToGrow;

    public Image icon;
    public Image currencyIcon;
    public Image diamondsIcon;
}