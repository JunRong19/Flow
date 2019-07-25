using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using CI.QuickSave;
using BayatGames.SaveGameFree;

public class FarmSwapper : MonoBehaviour
{
    public FarmGrid CurrentFarmGrid;
    
    [SerializeField] private TextMeshProUGUI selectedFarmText;

    [SerializeField] private int selectedFarmIndex;
    [SerializeField] private int maxFarmIndex;

    private void Awake() {
        if(SaveGame.Exists("FarmGrids")) {
            List<string> farmsInfo = SaveGame.Load<List<string>> ("FarmGrids");

            maxFarmIndex = farmsInfo.Count - 1;
        } else {
            maxFarmIndex = 0;
        }

        selectedFarmIndex = maxFarmIndex;

        CurrentFarmGrid.LoadTileDataFromFarm(selectedFarmIndex);
        UpdateSelectedFarmText();
    }

    public void GoToPreviousFarm() {
        selectedFarmIndex--;

        if(selectedFarmIndex < 0) {
            selectedFarmIndex = 0;
            return;
        }

        CurrentFarmGrid.LoadTileDataFromFarm(selectedFarmIndex);
        UpdateSelectedFarmText();
    }

    public void GoToNextFarm() {
        selectedFarmIndex++;

        if(selectedFarmIndex > maxFarmIndex) {
            selectedFarmIndex = maxFarmIndex;
            return;
        }

        CurrentFarmGrid.LoadTileDataFromFarm(selectedFarmIndex);
        UpdateSelectedFarmText();
    }

    public void ExpandFarmList() {
        maxFarmIndex++;
        selectedFarmIndex = maxFarmIndex;

        CurrentFarmGrid.CreateTileDataFromFarm(selectedFarmIndex);
        UpdateSelectedFarmText();
    }

    private void UpdateSelectedFarmText() {
        selectedFarmText.text = "Viewing Farm #" + (selectedFarmIndex + 1);
    }
}
