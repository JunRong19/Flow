using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmTile : MonoBehaviour {
	[SerializeField] private SpriteRenderer cornGraphic;

    [SerializeField, Tooltip("Where the tile is in the grid list")] private int position;

	[SerializeField] private Corn corn;

	#region Properties

	public Corn Corn {
		get => corn;
		set => UpdateCorn(value);
	}

    public int Position {
        get => position;
        set => position = value;
    }

    #endregion

    /// <summary>
    /// Update the corn and the sprite in this tile.
    /// </summary>
    private void UpdateCorn(Corn newCorn) {
		cornGraphic.sprite = newCorn.CornSprite;
		corn = newCorn;
	}
}
