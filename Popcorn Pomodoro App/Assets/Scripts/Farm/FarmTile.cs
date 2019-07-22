using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmTile : MonoBehaviour {
	[SerializeField] private SpriteRenderer cornGraphic;

	private Corn corn;

	#region Properties

	public Corn Corn {
		get => corn;
		set => UpdateCorn(value);
	}

	#endregion

	/// <summary>
	/// Update the corn and the sprite in this tile.
	/// </summary>
	private void UpdateCorn(Corn newCorn) {
		cornGraphic.sprite = newCorn.cornSprite;
		corn = newCorn;
	}
}
