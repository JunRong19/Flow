using System;
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

	private TimeSpan timeLeft;

	private float initialSeconds;
	private float maxSeconds;

	[SerializeField, Tooltip("Debug purposes, shows current time left to wait")]
	private float currentSeconds;

	public void StartTimer(float initialMinutes, float maxMinutes) {
		initialSeconds = initialMinutes * 60;
		maxSeconds = maxMinutes * 60;

		currentSeconds = initialSeconds;

		StartCoroutine(CountdownTime());
	}

	private IEnumerator CountdownTime() {
		/// Refactor this
		timeLeftImage.fillAmount = currentSeconds / maxSeconds;

		while(currentSeconds > 0) {
			timeLeft = TimeSpan.FromSeconds(currentSeconds);

			timeText.text = string.Format("{0:D2}:{1:D2}", timeLeft.Minutes + timeLeft.Hours * 60, timeLeft.Seconds);

			currentSeconds--;

			if(currentSeconds % 60 == 0) {
				timeLeftImage.fillAmount = currentSeconds / maxSeconds;
			}

			yield return new WaitForSeconds(1f);
		}

		StopTimer();
	}

	private void StopTimer() {
		PanelManager panelManager = PanelManager.Instance;

		panelManager.TogglePanelVisibility(PanelType.CornTimer, true);
		panelManager.TogglePanelVisibility(PanelType.Countdown, false);
	}
}
