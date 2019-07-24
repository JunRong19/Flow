using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : Singleton<PanelManager> {
	[SerializeField, Tooltip("All the panels used in the game")]
	private Panel[] panels;

	private Dictionary<PanelType, Panel> panelsDictionary = new Dictionary<PanelType, Panel>();

	private void Awake() {
		foreach(Panel panel in panels) {
			panelsDictionary.Add(panel.Type, panel);

			if(panel.startHidden) {
				TogglePanelVisibility(panel.Type, false);
			}
		}
	}

	public void TogglePanelVisibility(PanelType panel, bool state) {
		Panel selectedPanel = panelsDictionary[panel];
		selectedPanel.ToggleAllObjectsVisibility(state);
	}
}
