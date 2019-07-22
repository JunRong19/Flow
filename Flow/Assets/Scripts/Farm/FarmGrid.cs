using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CI.QuickSave;
using System;

public class FarmGrid : MonoBehaviour {
	[SerializeField, Tooltip("Prefab of one tile.")] private FarmTile farmTile;

    [SerializeField, Tooltip("Current farm index the player is looking at")] private int farmIndex;

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

    private void OnDisable() {
        SaveGrid();
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

                tile.Position = new Vector2Int(w, h);

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

        if(QuickSaveRoot.Exists("FarmGrids")) {
            string farmInfo = "";

            QuickSaveReader.Create("FarmGrids")
                .Read<string>("Farm" + farmIndex, (r) => { farmInfo = r; });

            string[] tiles = farmInfo.Split(';');

            for(int index = 0; index < tiles.Length; index++) {

                if(index == tiles.Length - 1) {
                    continue;
                }

                string[] tileInfo = tiles[index].Split(',');

                Enum.TryParse(tileInfo[2], out CornType type);

                if(type != CornType.None) {
                    PlantCornAtGridPosition(new Vector2Int(int.Parse(tileInfo[0]), int.Parse(tileInfo[1])), type);
                }
            }
        }
    }

	/// <summary>
	/// Unload any corns that the player planted.
	/// </summary>
	private void UnloadCorn() {
		List<CornType> unloadedCorns = CornLoader.UnloadCorn();

        // If there is no corn to unload return.
        if(unloadedCorns.Count <= 0 || emptyTiles.Count <= 0) {

            /// TO DO adding corns to the next farm

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

        SaveGrid();
    }

    private void SaveGrid() {
        string farmInfo = "";

        foreach(FarmTile tile in farmGrid) {
            string tileInfo = "";
            tileInfo += tile.Position.x + "," + tile.Position.y;

            if(tile.Corn == null || tile.Corn.Type == CornType.None) {
                tileInfo += "," + CornType.None;
            } else {
                tileInfo += "," + tile.Corn.Type;
            }

            tileInfo += ";";

            farmInfo += tileInfo;
        }

        QuickSaveWriter.Create("FarmGrids")
            .Write("Farm" + farmIndex, farmInfo)
            .Commit();
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
		FarmTile randomTile = emptyTiles[UnityEngine.Random.Range(0, emptyTiles.Count)];
		randomTile.Corn = CornDictionary.GetCornByType(cornType);

        Debug.Log("Planting corn at random pos");

        // Update the empty and filled tiles list.
        emptyTiles.Remove(randomTile);
		filledTiles.Add(randomTile);
	}
}
