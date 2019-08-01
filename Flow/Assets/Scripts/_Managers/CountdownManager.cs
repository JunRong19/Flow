using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountdownManager : Singleton<CountdownManager> {
	[SerializeField, Tooltip("The timer used to show the countdown")] private Timer countdownTimer;
	[SerializeField, Tooltip("The time selector used to select the time")] private RadialSlider timeSelector;
	[SerializeField, Tooltip("Toast popup warning")] private ToastPopup toastPopup;

    [SerializeField] private MusicSelector musicSelector;

	private PanelManager panelManager;

	public static bool isCountingDown;

	private void Awake() {
		panelManager = PanelManager.Instance;
	}

	public void StartCountDown() {
		if(!Sensor.ReadyToPlant()) {
            toastPopup.UpdateTextForStart();
            panelManager.TogglePanelVisibility(PanelType.PlantingWarning, true);

            return;
		}

        musicSelector.ToggleMusicPlaying(true);

		panelManager.TogglePanelVisibility(PanelType.CornTimer, false);
		panelManager.TogglePanelVisibility(PanelType.Hamburger, false);

		panelManager.TogglePanelVisibility(PanelType.Countdown, true);
        panelManager.TogglePanelVisibility(PanelType.MusicPopup, true);

        float countdownTime = timeSelector.CurrentValue;
		float maxTime = timeSelector.MaxValue;

		CornType growingType = timeSelector.TimingIntervals[(int)timeSelector.CurrentIndex].Type;

		countdownTimer.StartTimer(countdownTime, maxTime, growingType);

		isCountingDown = true;
	}

	public void StopCountDown(bool success) {
		countdownTimer.StopTimer(success);
		isCountingDown = false;

        musicSelector.ToggleMusicPlaying(false);

        timeSelector.RevertTimerSprite();

        if(!success) {
            toastPopup.UpdateTextForEnd();
            panelManager.TogglePanelVisibility(PanelType.PlantingWarning, true);
        }
    }
}
