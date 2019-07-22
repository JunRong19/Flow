using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmGrid : MonoBehaviour {
	[SerializeField, Tooltip("Prefab of one tile.")] private FarmTile farmTile;

	[SerializeField, Tooltip("Width of the grid.")] private int width = 6;
	[SerializeField, Tooltip("Height of the grid.")] private int height = 6;

	private FarmTile[,] farmGrid;

	private List<FarmTile> emptyTiles = new List<FarmTile>();
	private List<FarmTile> filledTiles = new List<FarmTile>();

	private void Start() {
		InitializeGrid();
		LoadCornFromSave();
		UnloadCorn();
	}

	private void InitializeGrid() {
		// Initialize the 2D array for the farm grid.
		farmGrid = new FarmTile[width, height];

		// Cache the position of the farm tiles.
		Vector3 pos = new Vector3();

		// Loop through all the coords of the grid.
		for(int w = 0; w < width; w++) {
			for(int h = 0; h < height; h++) {
				pos.x = w;
				pos.z = h;

				// Spawn farm tiles and parent them to this gameobject.
				FarmTile tile = Instantiate(farmTile, pos, Quaternion.identity);
				tile.transform.parent = transform;

				// Assign the newly spawned farm tile to the current coords of the grid.
				farmGrid[w, h] = tile;

				// The newly spawn tiles are empty.
				emptyTiles.Add(tile);
			}
		}
	}

	/// <summary>
	/// Load any corns in the save system for the current day.
	/// </summary>
	private void LoadCornFromSave() {
		// Load system using PlantCornAtGridPosition();
	}

	/// <summary>
	/// Unload any corns that the player planted.
	/// </summary>
	private void UnloadCorn() {
		List<CornType> unloadedCorns = CornLoader.UnloadCorn();

		// If there is no corn to unload or no space on the farm, return.
		if(unloadedCorns.Count <= 0 || emptyTiles.Count <= 0) {
			return;
		}

		// Start planting the unloaded corns at random position. 
		for(int i = 0; i < unloadedCorns.Count; i++) {
			// If all the tiles are full, stop planting any more corns.
			if(emptyTiles.Count <= 0) {
				Debug.Log("Grid is full, you planted enough corns today, come back tomorrow!");
				break;
			}
			PlantCornAtRandomPosition(unloadedCorns[i]);
		}

		// Save the game here + date.
	}

	private void PlantCornAtGridPosition(Vector2Int pos, CornType cornType) {
		// Plant a corn at a specific position on the grid.
		farmGrid[pos.x, pos.y].Corn = CornDictionary.GetCornByType(cornType);

		// Update the empty and filled tiles list.
		emptyTiles.Remove(farmGrid[pos.x, pos.y]);
		filledTiles.Add(farmGrid[pos.x, pos.y]);
	}

	private void PlantCornAtRandomPosition(CornType cornType) {
		// Choose a random tile to plant the corn.
		FarmTile randomTile = emptyTiles[Random.Range(0, emptyTiles.Count)];
		randomTile.Corn = CornDictionary.GetCornByType(cornType);

		// Update the empty and filled tiles list.
		emptyTiles.Remove(randomTile);
		filledTiles.Add(randomTile);
	}
}
