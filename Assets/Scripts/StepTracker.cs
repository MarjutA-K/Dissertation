using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StepTracker : MonoBehaviour
{
    public static StepTracker instance;
    private ShopManager shopManager;
    [SerializeField] public LoadSave _saveManager;

    public TMP_Text stepsTxt;
    public TMP_Text stepAmountTxt;
    public TMP_Text currentStorageAmountTxt;
    public TMP_Text storageAmountTxt;
    public TMP_Text priceTxt;

    public Button upgradeBtn;
    public Slider slider;
    public Slider storageSlider;

    public float stepThreshold = 0.5f;
    private float lowPassFilterFactor = 0.2f;
    private float[] lowPassResults = new float[3];
    private Vector3 prevAcceleration;
    private Vector3 prevRotation;
    public int stepCount;
    public int steps;

    public int purchasePrice;

    public int maxValue = 10000;

    public GameObject maxTxt;
    public GameObject coinIcon;
    public GameObject plusSign;
    public GameObject upgradeAmount;

    public int addSteps;

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
        storageSlider.maxValue = 50000;
        maxValue = 10000;
        purchasePrice = 1000;

        stepsTxt.text = stepCount.ToString();
        stepAmountTxt.text = steps.ToString();
        currentStorageAmountTxt.text = maxValue.ToString();
        priceTxt.text = purchasePrice.ToString();
        maxTxt.SetActive(false);

        shopManager = FindObjectOfType<ShopManager>();       
    }

    void Update()
    {
        StepsTaken();
        StepsTaken1();
        CheckPurchable();
        UpdateUI();

        slider.maxValue = maxValue;
        slider.value = stepCount;
        storageSlider.value = maxValue;

        AddSteps();
    }

    private void UpdateUI()
    {
        if (maxValue >= 10000)
        {
            int amountInK = maxValue / 1000;
            storageAmountTxt.text = amountInK.ToString("0.#") + "K";
        }

        if (maxValue == 50000)
        {
            upgradeBtn.interactable = false;
            maxTxt.SetActive(true);
            priceTxt.gameObject.SetActive(false);
            coinIcon.SetActive(false);
            plusSign.SetActive(false);
            upgradeAmount.SetActive(false);
        }
    }

    public void UpgradeStorage()
    {
        if (shopManager.money >= purchasePrice)
        {            
            shopManager.money = shopManager.money - purchasePrice;
            shopManager.moneyTxt.text = shopManager.money.ToString();
            maxValue += 10000;
            currentStorageAmountTxt.text = maxValue.ToString();
            purchasePrice *= 2;
            priceTxt.text = purchasePrice.ToString();
            _saveManager.moneyChanged.Invoke(shopManager.money);
        }
    }

    public void CheckPurchable()
    {
        if (shopManager.money >= purchasePrice)
        {
            upgradeBtn.interactable = true;
        }
        else
        {
            upgradeBtn.interactable = false;
        }
    }

    private void AddXP()
    {
        if((stepCount % 1000) == 0 && stepCount != 0)
        {
            XPManager.instance.AddXP(250);
        }
    }

    public void AddSteps()
    { 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            steps += addSteps;
            stepAmountTxt.text = steps.ToString();
            AddXP();

            if (stepCount < maxValue)
            {
                stepCount += addSteps;
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
            steps++;
            stepsTxt.text = steps.ToString();

            if(steps >= 1000)
            {
                int amountInK = steps / 1000;
                stepAmountTxt.text = amountInK.ToString("0.#") + "K";
            }
            else
            {
                stepAmountTxt.text = steps.ToString();
            }
        }

        // Update previous values
        prevAcceleration = acceleration;
        prevRotation = rawRotation;
    }

    private void StepsTaken1()
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

        // Update previous values
        prevAcceleration = acceleration;
        prevRotation = rawRotation;
    }
}
