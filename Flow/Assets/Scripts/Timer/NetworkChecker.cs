using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NetworkChecker : MonoBehaviour {

	[SerializeField, Tooltip("Time interval in seconds during pinging for any network connection.")] private float pingInterval;

	private Ping ping;
	private WaitForSeconds waitingTime;

	private float pingStartTime;
	private bool isPinging;

	private const string pingAddress = "8.8.8.8"; // Google Public DNS server

	#region Properties

	public static bool AllowConnection { get; set; }
	public static bool HasNetwork { get; private set; }

	#endregion

	private void Start() {
		waitingTime = new WaitForSeconds(pingInterval);
		UpdateNetworkConnectivity();
	}

	private void Update() {
		if(AllowConnection) {
			UpdateNetworkConnectivity();
		}
	}

	private void UpdateNetworkConnectivity() {
		// If user is currently pinging, don't try to make any connections.
		if(isPinging) {
			return;
		}

		// Try to see if any connection can be made before pinging to see if there is network.
		if(ConnectionPossiblyAvaiable()) {
			StartCoroutine(PingForNetwork());
		}
	}

	private bool ConnectionPossiblyAvaiable() {
		bool possibleConnection;

		// Use the user's NetworkReachability to determine if they can conenct to the internet.
		switch(Application.internetReachability) {
			case NetworkReachability.ReachableViaLocalAreaNetwork:
				possibleConnection = true;
				break;
			case NetworkReachability.ReachableViaCarrierDataNetwork:
				possibleConnection = true;
				break;
			default:
				possibleConnection = false;
				break;
		}

		if(!possibleConnection) {
			// Unable to connect to the internet.
			HasNetwork = false;
			return false;
		}

		// If timer is counting down, stop it because the player has wifi.
		if(CountdownManager.isCountingDown) {
			CountdownManager.Instance.StopCountDown(false);
			CountdownManager.isCountingDown = false;
			return false;
		}
		return true;
	}

	private IEnumerator PingForNetwork() {
		// Lock the coroutine.
		isPinging = true;

		// Start a new ping request to Google.
		ping = new Ping(pingAddress);
		pingStartTime = Time.time;

		// Wait a few seconds.
		yield return waitingTime;

		// Check if there is any reply from the ping request.
		if(ping != null && ping.isDone) {
			// Able to establish connection.
			HasNetwork = true;

		} else {
			// Failed to establish connection.
			HasNetwork = false;
		}

		// Stop pinging.
		ping = null;
		isPinging = false;
	}

	public static bool IsNetworkReady() {
		if(!AllowConnection || !HasNetwork) {
			return true;
		} else {
			return false;
		}
	}
}
