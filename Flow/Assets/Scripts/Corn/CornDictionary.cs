using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CornData {
	public CornType cornType;
	public Corn corn;
}

public class CornDictionary : MonoBehaviour {
	[SerializeField] private CornData[] cornData;

	private static Dictionary<CornType, Corn> cornDict = new Dictionary<CornType, Corn>();

	private void Start() {
		InitializeDictionary();
	}

	/// <summary>
	/// Add every corn in the game to the dictionary.
	/// </summary>
	private void InitializeDictionary() {
		for(int i = 0; i < cornData.Length; i++) {
			cornDict.Add(cornData[i].cornType, cornData[i].corn);
		}
	}

	/// <summary>
	/// Get the corn requested from the dictionary and return it.
	/// </summary>
	public static Corn GetCornByType(CornType cornType) {
		cornDict.TryGetValue(cornType, out Corn requestedCorn);
		return requestedCorn;
	}
}

