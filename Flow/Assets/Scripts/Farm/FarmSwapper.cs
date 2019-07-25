using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using CI.QuickSave;
using BayatGames.SaveGameFree;

public class FarmSwapper : MonoBehaviour
{
    public FarmGrid CurrentFarmGrid;

    [SerializeField] private TextMeshProUGUI uiDebug;

    [SerializeField] private TextMeshProUGUI selectedFarmText;

    [SerializeField] private int selectedFarmIndex;
    [SerializeField] private int maxFarmIndex;

    private void Awake() {

        uiDebug.text = "Start; ";

        if(SaveGame.Exists("FarmGrids")) {
            uiDebug.text += "Exists; ";

            List<string> farmsInfo = SaveGame.Load<List<string>> ("FarmGrids");

            uiDebug.text += "Length; " + farmsInfo.Count;

            maxFarmIndex = farmsInfo.Count - 1;
        } else {
            maxFarmIndex = 0;
        }


        //if(QuickSaveRoot.Exists("FarmGrids")) {

        //    int numberOfFarms = 0;

        //    uiDebug.text += "Exists; ";

        //    IEnumerable<string> keys = QuickSaveReader.Create("FarmGrids").GetAllKeys();
        //    foreach(var key in keys) {
        //        numberOfFarms++;
        //    }

        //    uiDebug.text += "Length; " + numberOfFarms;

        //    maxFarmIndex = numberOfFarms - 1;
        //} else {
        //    maxFarmIndex = 0;
        //}


        uiDebug.text += "End ";

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
            selectedFarmIndex = maxFarmIndex - 1;
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
