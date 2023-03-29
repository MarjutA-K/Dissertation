using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class TestPedometer : MonoBehaviour
{
    public static TestPedometer instance;

    public TMP_Text stepsTxt;
    public float stepThreshold = 0.5f;
    private float lowPassFilterFactor = 0.2f;
    private float[] lowPassResults = new float[3];
    private Vector3 prevAcceleration;
    private Vector3 prevRotation;
    public int stepCount = 0;

    private bool reachedTarget1;
    private bool reachedTarget2;
    private bool reachedTarget3;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        stepsTxt.text = stepCount.ToString();

        reachedTarget1 = true;
        reachedTarget2 = true;
        reachedTarget3 = true;
    }

    void Update()
    {
        StepsTaken();

        /*switch (stepCount)
        {
            case 500:
                if (reachedTarget1)
                {
                    reachedTarget1 = false;
                    ShopManager.instance.AddMoney(50);
                }
                Debug.Log("500 steps");
                break;
            case 1000:
                if (reachedTarget2)
                {
                    reachedTarget2 = false;
                    ShopManager.instance.AddMoney(100);
                }
                Debug.Log("1000 steps");
                break;
            case 5000:
                if (reachedTarget3)
                {
                    reachedTarget3 = false;
                    ShopManager.instance.AddMoney(200);
                }
                Debug.Log("5000 steps");
                break;
            default:
                //Debug.Log("Unknown level");
                break;
        }*/
    }

    public void AddSteps()
    {
        stepCount += 500;
        stepsTxt.text = stepCount.ToString();

        if (stepCount >= 1000)
        {
            int amountInK = stepCount / 1000;
            stepsTxt.text = amountInK.ToString("0.#") + "K";
        }
        else
        {
            stepsTxt.text = stepCount.ToString();
        }
    }

    private void StepsTaken()
    {
        Vector3 rawAcceleration = Input.acceleration;
        Vector3 rawRotation = Input.gyro.rotationRateUnbiased;

        // Apply low-pass filter to accelerometer data
        lowPassResults[0] = rawAcceleration.x * lowPassFilterFactor + lowPassResults[0] * (1 - lowPassFilterFactor);
        lowPassResults[1] = rawAcceleration.y * lowPassFilterFactor + lowPassResults[1] * (1 - lowPassFilterFactor);
        lowPassResults[2] = rawAcceleration.z * lowPassFilterFactor + lowPassResults[2] * (1 - lowPassFilterFactor);
        Vector3 acceleration = new Vector3(lowPassResults[0], lowPassResults[1], lowPassResults[2]);

        // Calculate delta acceleration and rotation
        Vector3 deltaAcceleration = acceleration - prevAcceleration;
        Vector3 deltaRotation = rawRotation - prevRotation;

        // Check if step was taken
        if (deltaAcceleration.sqrMagnitude > stepThreshold && deltaRotation.sqrMagnitude < 0.05f)
        {
            stepCount++;
            stepsTxt.text = stepCount.ToString();

            if(stepCount >= 1000)
            {
                Debug.Log("Hello");
                int amountInK = stepCount / 1000;
                stepsTxt.text = amountInK.ToString("0.#") + "K";
            }
            else
            {
                stepsTxt.text = stepCount.ToString();
            }
        }

        // Update previous values
        prevAcceleration = acceleration;
        prevRotation = rawRotation;
    }
}
