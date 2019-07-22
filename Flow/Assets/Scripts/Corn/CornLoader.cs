using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornLoader : MonoBehaviour {
	private static List<CornType> loader = new List<CornType>();

	/// <summary>
	///	When player finish growing a corn / corn died. Add the corn that grew / died to the corn loader.
	/// To be loaded into the farm when the player go to the farm scene.
	/// </summary>
	public static void LoadCorn(CornType cornType) {
		loader.Add(cornType);
	}

	public static List<CornType> UnloadCorn() {
		// Create a new list to unload since list is reference type.
		List<CornType> unloadedCorns = new List<CornType>(loader);

		// Clear the loader.
		ClearLoader();

		// Return the unloaded corns.
		return unloadedCorns;
	}

	public static void ClearLoader() {
		loader.Clear();
	}
}
