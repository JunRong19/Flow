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
		Debug.Log(NetworkChecker.HasNetwork);
		Debug.Log(Accelerometer.IsStationary);
	}
}
