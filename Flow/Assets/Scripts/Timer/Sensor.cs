using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : Singleton<Sensor> {

	public static bool ReadyToPlant() {
		// If one of the sensor is not correct, return false.
		Debug.Log(IsNetworkReady());
		if(IsNetworkReady() && IsAccelerometerReady()) {
			return true;
		} else {
			return false;
		}
	}

	private static bool IsNetworkReady() {
		if(!NetworkChecker.AllowConnection || !NetworkChecker.HasNetwork) {
			return true;
		} else {
			return false;
		}
	}

	private static bool IsAccelerometerReady() {
		if(!Accelerometer.AllowAccelerometer || Accelerometer.IsStationary) {
			return true;
		} else {
			return false;
		}
	}

	private void Update() {
		Debug.Log("Network checker toggle: " + NetworkChecker.AllowConnection);
		Debug.Log("Accelerometer toggle: " + Accelerometer.AllowAccelerometer);

		Debug.Log("Network has network: " + NetworkChecker.HasNetwork);
		Debug.Log("Accelerometer is stationary: " + Accelerometer.IsStationary);
	}
}
