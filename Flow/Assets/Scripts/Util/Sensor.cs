using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour {
	[SerializeField] private NetworkChecker networkChecker;
	[SerializeField] private Accelerometer accelerometer;

	public void ToggleNetworkChecker(bool state) {
		networkChecker.enabled = state;
	}

	public void ToggleAccelerometer(bool state) {
		accelerometer.enabled = state;
	}

	private void Update() {
		Debug.Log("Network checker toggle: " + NetworkChecker.AllowConnection);
		Debug.Log("Accelerometer toggle: " + Accelerometer.AllowAccelerometer);
	}
}
