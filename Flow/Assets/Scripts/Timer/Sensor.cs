using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : Singleton<Sensor> {

	public static bool ReadyToPlant() {
		// If one of the sensor is not correct, return false.
		if(NetworkChecker.IsNetworkReady() && Accelerometer.IsAccelerometerReady()) {
			return true;
		} else {
			return false;
		}
	}

    public static bool NetworkReady() {
        return NetworkChecker.IsNetworkReady();
    }

    public static bool AccelerometerReady() {
        return Accelerometer.IsAccelerometerReady();
    }
}
