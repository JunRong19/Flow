using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archive
{
    // Timer

    //private int previousTime;

    //private int currentTime;

    //private int countdownTime;

    //private int seconds;

    //private int minutes;

    //private bool timerStart;

    //public void StartCountdown()
    //{
    //    //The countdown has been started.
    //    currentTime = currentTime / 5;

    //    countdownTime = Mathf.RoundToInt(currentTime) * 5;

    //    Debug.Log(countdownTime);

    //    if (countdownTime >= 10)
    //    {
    //        if (!timerStart)
    //        {
    //            Debug.Log("Timer started");

    //            seconds = 60;

    //            minutes = countdownTime - 1;

    //            StartCoroutine(CountdownTime());

    //            timerStart = true;



    //            UIManager.instance.StartTimerCountdown();
    //        }
    //    }
    //}

    //private IEnumerator CountdownTime()
    //{
    //    while (minutes > 0)
    //    {
    //        //Countdown the time and update the UI for counting down.
    //        if (seconds > 0)
    //        {
    //            Debug.Log(seconds);

    //            Debug.Log(minutes);

    //            seconds -= 1;

    //            //Upate UI
    //            UIManager.instance.UpdateCountdownTimeText(minutes, seconds);

    //            yield return new WaitForSeconds(1.0f);
    //        }
    //        else
    //        {
    //            seconds = 60;

    //            minutes -= 1;

    //            UIManager.instance.UpdateCountdownTimeText(minutes, seconds);

    //            //Update UI
    //            yield return new WaitForEndOfFrame();
    //        }
    //    }
    //}

    //public void SetTimer()
    //{
    //    int newTime = Mathf.RoundToInt(GetComponent<RadialSlider>().SliderValue);

    //    //When the slider is dragged around, do something. 
    //    previousTime = currentTime;

    //    currentTime = newTime;

    //    ChangePicture(currentTime);
    //    //Change the text on the screen to show how many minutes they have selected.
    //}

    //private void ChangePicture(float newTime)
    //{
    //    //When the value hits a certain amount, either positively or negatively, change the picture.
    //    if (newTime > previousTime)
    //    {
    //        //When the new time is more than the previous time, change the picture to the next one (the one after the current picture).
    //    }
    //    else
    //    {
    //        //When the new time is lesser than the previous time, change the picture to the previous one (the one before the current picture).
    //    }
    //}

    //private void OnTimerEnd()
    //{
    //    //On timer end, grow a tree.

    //    //Update any other UI needed as well.
    //}


    //UIManager
    //[SerializeField, Tooltip("The text on the countdown panel when the player starts the timer should be here.")]
    //private TextMeshProUGUI countdownTimeText;

    //[SerializeField, Tooltip("The panel where the user can watch the timer go down.")]
    //private GameObject countdownPanel;

    //public delegate void UpdateUI();

    //public event UpdateUI OnUIUpdate, OnTimerStart;

    //private void OnEnable()
    //{
    //    OnTimerStart += TriggerTimerStart;
    //}

    //public void UpdateCountdownTimeText(int minutes, int seconds)
    //{
    //    countdownTimeText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
    //}

    //public void TriggerTimerStart()
    //{
    //    if (OnTimerStart != null)
    //    {
    //        OnTimerStart();
    //    }
    //}

    //public void StartTimerCountdown()
    //{
    //    countdownPanel.SetActive(true);
    //}
}
