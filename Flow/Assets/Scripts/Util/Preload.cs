using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This script helps to load into the starting screen while preloading other DDOL scripts in the scene.
/// </summary>
public class Preload : MonoBehaviour {

	[SerializeField, Tooltip("Starting scene name.")] private string startingScene;

	private void Start() {
		// Load the starting scene.
		SceneManager.LoadScene(startingScene);
	}
}
