using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatchManager : MonoBehaviour
{
    public static PatchManager instance;
    [SerializeField]
    GrowController[] patches;
    [SerializeField]
    PlantArrayWrapper plantedflowers;
    [SerializeField] PlantSO badplant;

    private void Start()
    {
        
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        for (int i = 0; i < patches.Length; i++)
        {
            if (plantedflowers.startPlant[i].plantTitle != badplant.plantTitle && plantedflowers.startPlant[i] != null)
            {
                Debug.Log("Plant at location" + i + "is " + plantedflowers.startPlant[i].plantTitle);
                patches[i].LoadFromData(plantedflowers.startPlant[i], plantedflowers.growthStage[i], plantedflowers.growthSteps[i]);
            }
        }
    }

    public void RefreshPatches()
    {
        for (int i = 0; i < patches.Length; i++)
        {
            if (patches[i].plant != null && patches[i].growthStage >=0)
            {
                if (patches[i].plant.plantTitle != badplant.plantTitle)
                {
                    GrowController patch = patches[i];
                    plantedflowers.startPlant[i] = patch.plant;
                    plantedflowers.growthStage[i] = patch.growthStage;
                    plantedflowers.growthSteps[i] = patch.growthTime;
                }

            }
            else
                {
                    plantedflowers.startPlant[i] = badplant;
                    plantedflowers.growthStage[i] = -1;
                    plantedflowers.growthSteps[i] = 0.0f;
                }
        }
    }
}
