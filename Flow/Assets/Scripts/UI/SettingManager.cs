using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour {
	[SerializeField] private Image wifiSettingBtn;
	[SerializeField] private Image accelerometerSettingBtn;

	[SerializeField] private Sprite[] wifiSettingSprites;
	[SerializeField] private Sprite[] accelerometerSettingSprites;

	private void Start() {
		LoadSavedSettings();
		PanelManager.Instance.TogglePanelVisibility(PanelType.Settings, false);
	}

	private void LoadSavedSettings() {
		// Load the saved settings from player pref.
		UpdateWifiSetting(GetPlayerPrefBool("ToggleWifi"));
		UpdateAccelerometerSetting(GetPlayerPrefBool("ToggleAccelerometer"));
	}

	private void UpdateWifiSetting(bool state) {
		// Update sprite of wifi setting button.
		wifiSettingBtn.sprite = wifiSettingSprites[state ? 1 : 0];

		// Save wifi setting.
		SetPlayerPrefBool("ToggleWifi", state);

		// Update network checker.
		NetworkChecker.AllowConnection = state;
	}

	private void UpdateAccelerometerSetting(bool state) {
		// Update sprite of accelerometer setting button.
		accelerometerSettingBtn.sprite = accelerometerSettingSprites[state ? 1 : 0];

		// Save accelerometer setting.
		SetPlayerPrefBool("ToggleAccelerometer", state);

		// Update accelerometer.
		Accelerometer.AllowAccelerometer = state;
	}

	#region UI Interaction

	public void ToggleWifiSetting() {
		// Toggles the current state of the wifi setting.
		UpdateWifiSetting(!GetPlayerPrefBool("ToggleWifi"));
	}

	public void TogglAccelerometerSetting() {
		// Toggles the current state of the accelerometer setting.
		UpdateAccelerometerSetting(!GetPlayerPrefBool("ToggleAccelerometer"));
	}

	public void Open() {
		PanelManager.Instance.TogglePanelVisibility(PanelType.Settings, true);
	}

	public void Close() {
		PanelManager.Instance.TogglePanelVisibility(PanelType.Settings, false);
	}

	#endregion

	#region Extension methods

	public static bool GetPlayerPrefBool(string key) {
		// Default value is 1 (True).
		return PlayerPrefs.GetInt(key, 1) == 1;
	}

	public static void SetPlayerPrefBool(string key, bool state) {
		PlayerPrefs.SetInt(key, state ? 1 : 0);
	}

	#endregion
}
