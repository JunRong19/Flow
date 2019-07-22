using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CornDebug : MonoBehaviour {
	private void Update() {
		if(Input.GetKeyDown(KeyCode.Space)) {
			Debug.Log("You grew an adult corn!");
			CornLoader.LoadCorn(CornType.Adult);
		}

		if(Input.GetMouseButtonDown(1)) {
			SceneManager.LoadScene("FarmLand");
		}

		if(Input.GetMouseButtonDown(2)) {
			SceneManager.LoadScene("Timer");
		}
	}
}
