﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Reference: https://riptutorial.com/unity3d/example/12177/read-accelerometer-sensor--advance-

public class Accelerometer : MonoBehaviour {
	[SerializeField, Tooltip("Minimum and maximum range (-+) from flat vector.")] private float sensitivityRange;
	[SerializeField, Tooltip("The lower this value, the less smooth the value is and faster Accel is updated.")] private float updateSpeed = 30.0f;

	private Quaternion mobileAxis;

	private Vector3 lowPassValue = Vector3.zero;
	private Vector3 flatVector = new Vector3(0, 1, 0);
	private Vector3 filteredAccelValue;

	private float accelerometerUpdateInterval = 1.0f;
	private float lowPassKernelWidthInSeconds = 1.0f;
	private float lowPassFilterFactor = 0;

	#region Properties

	public static bool AllowAccelerometer { get; set; }
	public static bool IsStationary { get; private set; }

	#endregion

	private void Start() {
		//Filter Accelerometer
		accelerometerUpdateInterval /= updateSpeed;
		lowPassFilterFactor = accelerometerUpdateInterval / lowPassKernelWidthInSeconds;
		lowPassValue = Input.acceleration;

		mobileAxis = Quaternion.Euler(90, 0, 0);
	}

	private void Update() {
		if(AllowAccelerometer && CountdownManager.isCountingDown) {
			HandleAccelerometer();
		}
	}

	private void HandleAccelerometer() {
		//Get smoothed Accelerometer values.
		filteredAccelValue = mobileAxis * FilterAccelValue();

		//Check if user's phone had moved greater than limit.
		if(Vector3.Distance(flatVector, filteredAccelValue) > sensitivityRange) {
			IsStationary = false;

			CountdownManager.Instance.StopCountDown(false);
			CountdownManager.isCountingDown = false;
		} else {
			IsStationary = true;
		}
	}

	private Vector3 FilterAccelValue() {
		lowPassValue = Vector3.Lerp(lowPassValue, Input.acceleration, lowPassFilterFactor);
		return lowPassValue;
	}
}