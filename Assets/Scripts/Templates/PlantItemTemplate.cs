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

    public GameObject priceTxtActive;
    public GameObject titleActive;

    public Image icon;
}