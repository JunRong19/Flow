using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CI.QuickSave;
using System;

public class FarmGrid : MonoBehaviour {
    [SerializeField, Tooltip("Current farm index the player is looking at")] private int farmIndex = -1;

	[SerializeField, Tooltip("All the tiles in the farm")] private List<FarmTile> farmTiles = new List<FarmTile>();

	[SerializeField] private List<FarmTile> emptyTiles = new List<FarmTile>();
	[SerializeField] private List<FarmTile> filledTiles = new List<FarmTile>();
    
	private void Start() {
        InitializeTiles();
        LoadTileDataFromFarm(0);
		UnloadCorn();

        void InitializeTiles() {
            for(int index = 0; index < farmTiles.Count; index++) {
                farmTiles[index].Position = index;
            }
        }
    }

    private void OnDisable() {
        SaveGrid();
    }

	/// <summary>
	/// Loads and sets all farm tiles data based on farm index
	/// </summary>
	public void LoadTileDataFromFarm(int newIndex) {

        SaveGrid();

        emptyTiles.Clear();
        filledTiles.Clear();

        farmIndex = newIndex;

        // If there isn't a save for any farm grids, set up the whole farm grid as empty
        if(!QuickSaveRoot.Exists("FarmGrids")) {
            foreach(FarmTile tile in farmTiles) {
                emptyTiles.Add(tile);
            }
            return;
        }

        string farmInfo = "";

        // Gets the farm information based on its index
        QuickSaveReader.Create("FarmGrids")
            .Read<string>("Farm" + newIndex, (r) => { farmInfo = r; });

        // Each tile is seperated by a ';'
        string[] tiles = farmInfo.Split(';');

        // Sets up every farm tile
        for(int index = 0; index < tiles.Length; index++) {

            // Last tile has a ';' after it so last element in array is empty and should skip
            if(index == tiles.Length - 1) {
                continue;
            }

            // Each information in the tile is seperated by a ','
            string[] tileInfo = tiles[index].Split(',');

            // Where the tile is in the game world
            int pos = int.Parse(tileInfo[0]);

            // The type of corn currently on the farm
            Enum.TryParse(tileInfo[1], out CornType type);

            if(type != CornType.None) {
                PlantCornAtGridPosition(pos, type);
            } else {
                FarmTile emptyTile = farmTiles[pos];
                emptyTiles.Add(emptyTile);
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

                /// Go to next farm...

				break;
			}
			PlantCornAtRandomPosition(unloadedCorns[i]);
		}

        SaveGrid();
    }

    private void SaveGrid() {
        string farmInfo = "";

        foreach(FarmTile tile in farmTiles) {
            string tileInfo = "";
            tileInfo += tile.Position;

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

    private void PlantCornAtGridPosition(int pos, CornType cornType) {
        // Plant a corn at a specific position on the grid.
        FarmTile farmTile = farmTiles[pos];

        farmTile.Corn = CornDictionary.GetCornByType(cornType);

		// Update the filled tiles list.
		filledTiles.Add(farmTile);
	}

	private void PlantCornAtRandomPosition(CornType cornType) {
		// Choose a random tile to plant the corn.
		FarmTile randomTile = emptyTiles[UnityEngine.Random.Range(0, emptyTiles.Count)];
		randomTile.Corn = CornDictionary.GetCornByType(cornType);
        
        // Update the empty and filled tiles list.
        emptyTiles.Remove(randomTile);
		filledTiles.Add(randomTile);
	}
}
