using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class TestPedometer : MonoBehaviour
{
    /*private int stepCount = 0;
    public TMP_Text stepsTxt;

    StepCounter stepCounter;


    public void OnStep(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            stepCount++;
            stepsTxt.text = "Step taken! Total steps: " + stepCount.ToString();
        }
    }

    void OnEnable()
    {
        InputSystem.EnableDevice(StepCounter.current);
        InputAction stepAction = new InputAction("step", binding: "<StepCounter>/step");
        stepAction.performed += OnStep;
        stepAction.Enable();
    }

    void OnDisable()
    {
        InputAction stepAction = new InputAction("step", binding: "<StepCounter>/step");
        stepAction.performed -= OnStep;
        stepAction.Disable();
        InputSystem.DisableDevice(StepCounter.current);
    }*/


    public TMP_Text stepsTxt;
    public float stepThreshold = 0.5f;
    private float lowPassFilterFactor = 0.2f;
    private float[] lowPassResults = new float[3];
    private Vector3 prevAcceleration;
    private Vector3 prevRotation;
    private int stepCount = 0;

    void Update()
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
            stepsTxt.text = "Step taken! Total steps: " + stepCount.ToString();
        }

        // Update previous values
        prevAcceleration = acceleration;
        prevRotation = rawRotation;
    }
}
