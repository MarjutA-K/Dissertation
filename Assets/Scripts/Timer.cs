using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public float timeLeft;
    public bool timerOn = false;

    public TMP_Text timerTxt;
    public TMP_Text sliderTimerText;

    public Slider slider;
    public TMP_Text timeText;
    public float gametime;
    float time;

    private void Start()
    {
        slider.onValueChanged.AddListener((v) =>
        {
            float minutes = Mathf.FloorToInt(v / 60);
            float seconds = Mathf.FloorToInt(v % 60);

            sliderTimerText.text = string.Format("{0:00} : {1:00}", minutes, seconds);

            gametime = v;
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (timerOn)
        { 
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                updateTimer(timeLeft);
                time = gametime - Time.time;
                updateSliderTimer(time);           
            }
            else
            {
                Debug.Log("Time is up");
                timeLeft = 0;
                timerOn = false;
                slider.value = time;
            }
        }
    }

    private void updateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        timerTxt.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }

    private void updateSliderTimer(float currentTime)
    {
        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        sliderTimerText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }

    public void PauseTimer()
    {
        timerOn = !timerOn;
    }

    public void StartTimer()
    {
        timeLeft = slider.value;
        timerOn = true;
    }

    public void StopTimer()
    {
        timeLeft = 0;
        timerOn = false;
    }
}
