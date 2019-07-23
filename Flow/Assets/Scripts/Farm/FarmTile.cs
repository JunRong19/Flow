using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmTile : MonoBehaviour {
	[SerializeField] private SpriteRenderer cornGraphic;

    [SerializeField, Tooltip("Where the tile is in the grid")] private Vector2Int position;

	[SerializeField] private Corn corn;

	#region Properties

	public Corn Corn {
		get => corn;
		set => UpdateCorn(value);
	}

    public Vector2Int Position {
        get => position;
        set => position = value;
    }

    #endregion

    private void Awake() {
        position = new Vector2Int((int)transform.position.x, (int)transform.position.z);
    }

    /// <summary>
    /// Update the corn and the sprite in this tile.
    /// </summary>
    private void UpdateCorn(Corn newCorn) {
		cornGraphic.sprite = newCorn.CornSprite;
		corn = newCorn;
	}
}
