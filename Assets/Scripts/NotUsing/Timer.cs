using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public TMP_Text workoutTimerTxt;
    public TMP_Text sliderTimerText;

    public Slider slider;
    public float sliderTime;
    public float workoutTime;
    private float time;

    public bool timerOn = false;

    Player player;
    TabManager congratsMsg;
    WOCompleted workout;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        congratsMsg = FindObjectOfType<TabManager>();
        workout = FindObjectOfType<WOCompleted>();

        slider.onValueChanged.AddListener((v) =>
        {
            float minutes = Mathf.FloorToInt(v / 60);
            float seconds = Mathf.FloorToInt(v % 60);

            sliderTimerText.text = string.Format("{0:00} : {1:00}", minutes, seconds);

            sliderTime = v;
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (timerOn)
        { 
            if (workoutTime > 0)
            {
                workoutTime -= Time.deltaTime;
                time = sliderTime - Time.time;
                updateTimer(workoutTime);
                updateSliderTimer(time);           
            }
            else
            {
                Debug.Log("Time is up");
                workoutTime = 0;
                timerOn = false;
                slider.value = time;
                player.AddMoney();
                player.AddXP();
                //congratsMsg.showCongratsMsg();
                StartCoroutine(showMsgCountdown());
                workout.isWOCompleted = true;
            }
        }
    }

    private IEnumerator showMsgCountdown()
    {
        yield return new WaitForSeconds(2);
        //congratsMsg.CloseCongratsMsg();
        //congratsMsg.CloseExerciseTimerView();
        //congratsMsg.CloseExerciseDescr();
        //congratsMsg.CloseWorkoutView();
    }

    private void updateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        workoutTimerTxt.text = string.Format("{0:00} : {1:00}", minutes, seconds);
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
        workoutTime = slider.value;
        timerOn = true;
    }

    public void StopTimer()
    {
        workoutTime = 0;
        timerOn = false;
    }
}
