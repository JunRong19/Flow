using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountdownManager : Singleton<CountdownManager> {
	[SerializeField, Tooltip("The timer used to show the countdown")]
	private Timer countdownTimer;
	[SerializeField, Tooltip("The time selector used to select the time")]
	private RadialSlider timeSelector;

	public static bool isCountingDown;

	private PanelManager panelManager;


	private void Awake() {
		panelManager = PanelManager.Instance;
	}

	public void StartCountDown() {
		if(!Sensor.ReadyToPlant()) {
			Debug.Log("Does not fullfil the requirement to start planting.");
			return;
		}

		panelManager.TogglePanelVisibility(PanelType.CornTimer, false);
		panelManager.TogglePanelVisibility(PanelType.Countdown, true);

		float countdownTime = timeSelector.CurrentValue;
		float maxTime = timeSelector.MaxValue;

		CornType growingType = timeSelector.TimingIntervals[(int)timeSelector.CurrentIndex].Type;

		countdownTimer.StartTimer(countdownTime, maxTime, growingType);

		isCountingDown = true;
	}

	public void StopCountDown(bool success) {
		countdownTimer.StopTimer(success);
		isCountingDown = false;
	}
}
