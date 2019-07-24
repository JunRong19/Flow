﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour {
	[SerializeField, Tooltip("Text used to display time")]
	private TextMeshProUGUI timeText;
	[SerializeField, Tooltip("Text used to display time")]
	private Image timeLeftImage;

	[SerializeField, Tooltip("Current type of corn growing")]
	private CornType growingCornType;

	private TimeSpan timeLeft;

	private float initialSeconds;
	private float maxSeconds;

	[SerializeField, Tooltip("Debug purposes, shows current time left to wait")]
	private float currentSeconds;

	public void StartTimer(float initialMinutes, float maxMinutes, CornType growingType) {

		initialSeconds = initialMinutes * 60;
		maxSeconds = maxMinutes * 60;

		currentSeconds = initialSeconds;

		growingCornType = growingType;

		StartCoroutine(CountdownTime());
	}

	public void CancelTimer() {
		StopTimer(false);
	}

	private IEnumerator CountdownTime() {
		/// Refactor this
		timeLeftImage.fillAmount = currentSeconds / maxSeconds;

		while(currentSeconds > 0) {
			timeLeft = TimeSpan.FromSeconds(currentSeconds);

			timeText.text = string.Format("{0:D2}:{1:D2}", timeLeft.Minutes + timeLeft.Hours * 60, timeLeft.Seconds);

			currentSeconds--;

			if(currentSeconds % 60 == 0) {
                UpdateTimerFill();
            }

			yield return new WaitForSeconds(1f);
		}

		StopTimer(true);
	}

	public void StopTimer(bool success) {

		if(success) {
			CornLoader.LoadCorn(growingCornType);
		} else {
			CornLoader.LoadCorn(CornType.Dead);
		}
		PanelManager panelManager = PanelManager.Instance;

        currentSeconds = 0;
        UpdateTimerFill();

        panelManager.TogglePanelVisibility(PanelType.CornTimer, true);
		panelManager.TogglePanelVisibility(PanelType.Countdown, false);
	}

    private void UpdateTimerFill() {
        timeLeftImage.fillAmount = currentSeconds / maxSeconds;
    }
}
