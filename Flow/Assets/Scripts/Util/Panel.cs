using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Panel {
	public PanelType Type;
	public GameObject[] Objects;
	public bool startHidden;

	public void ToggleAllObjectsVisibility(bool state) {
		foreach(GameObject item in Objects) {
			item.SetActive(state);
		}
	}
}
