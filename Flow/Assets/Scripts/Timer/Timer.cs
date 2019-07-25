using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CI.QuickSave;

public class Timer : MonoBehaviour {
    [Header("INFORMATION")]
    [SerializeField, Tooltip("Text used to display time")] private TextMeshProUGUI timeText;
	[SerializeField, Tooltip("Text used to display time")] private Image timeLeftImage;
    [SerializeField, Tooltip("Current type of corn growing")] private CornType growingCornType;

    [Header("TIMINGS")]
    [SerializeField, Tooltip("List of timings for the sprite to change into")] private SliderTiming[] timings;
    [SerializeField, Tooltip("Corn sprite to change as timing changes")] private SpriteRenderer cornSprite;

    private Queue<SliderTiming> nextTiming = new Queue<SliderTiming>();

    private float secondsPassed;

	private TimeSpan timeLeft;

	private float initialSeconds;
	private float maxSeconds;

	[SerializeField, Tooltip("Debug purposes, shows current time left to wait")]
	private float currentSeconds;

    private bool timerRunning;

    private void Awake() {
        currentSeconds = 0;
        secondsPassed = 0;
    }

    // Handles screen timeout, timer will continue "running" when screen is off and app is paused
    private void OnApplicationPause(bool pause) {

        if(!timerRunning)
            return;

        if(pause) {
            DateTime previous = DateTime.UtcNow;
            DateTime now = DateTime.UtcNow;

            QuickSaveReader.Create("SleepInfo")
                .Read<DateTime>("PhoneSleptTime", (r) => { previous = r; });

            float seconds = (float)(previous - now).TotalSeconds;

            currentSeconds -= seconds;
            secondsPassed += seconds;

            UpdateTimerFill();
        } else {
            DateTime now = DateTime.UtcNow;

            QuickSaveWriter.Create("SleepInfo")
                .Write("PhoneSleptTime", now)
                .Commit();
        }
    }

    public void StartTimer(float initialMinutes, float maxMinutes, CornType growingType) {
        timerRunning = true;

        growingCornType = growingType;

        SetupSeconds();

        AddCornSprites();

        StartCoroutine(CountdownTime());

        void SetupSeconds() {
            initialSeconds = initialMinutes * 60;
            maxSeconds = maxMinutes * 60;
            secondsPassed = 0;

            currentSeconds = initialSeconds;

            UpdateTimerFill();
        }

        void AddCornSprites() {
            foreach(SliderTiming timing in timings) {
                nextTiming.Enqueue(timing);
            }
        }
	}

	public void CancelTimer() {
		StopTimer(false);
	}

    public void StopTimer(bool success) {

        if(success) {
            CornLoader.LoadCorn(growingCornType);
        } else {
            
        }
        PanelManager panelManager = PanelManager.Instance;

        currentSeconds = 0;
        UpdateTimerFill();

        timerRunning = false;

        panelManager.TogglePanelVisibility(PanelType.CornTimer, true);
        panelManager.TogglePanelVisibility(PanelType.Countdown, false);
    }

    private IEnumerator CountdownTime() {

		while(currentSeconds > 0) {
			timeLeft = TimeSpan.FromSeconds(currentSeconds);

			timeText.text = string.Format("{0:D2}:{1:D2}", timeLeft.Minutes + timeLeft.Hours * 60, timeLeft.Seconds);

			currentSeconds--;

            secondsPassed++;

            UpdateCornSprite();

            if(currentSeconds % 60 == 0) {
                UpdateTimerFill();
            }

			yield return new WaitForSeconds(1f);
		}

		StopTimer(true);
	}

    private void UpdateCornSprite() {
        if(nextTiming.Count == 0) {
            return;
        }

        if(secondsPassed >= nextTiming.Peek().Time * 60) {
            while(secondsPassed >= nextTiming.Peek().Time * 60) {
                cornSprite.sprite = nextTiming.Peek().SpriteToDisplay;
                nextTiming.Dequeue();
            }
        }
    }

    private void UpdateTimerFill() {
        timeLeftImage.fillAmount = currentSeconds / maxSeconds;
    }
}
