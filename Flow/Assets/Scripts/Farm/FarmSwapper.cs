using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using CI.QuickSave;

public class FarmSwapper : MonoBehaviour
{
    public FarmGrid CurrentFarmGrid;

    [SerializeField] private TextMeshProUGUI selectedFarmText;

    [SerializeField] private int selectedFarmIndex;
    [SerializeField] private int maxFarmIndex;

    private void Awake() {
        List<string> numberOfFarms = QuickSaveReader.Create("FarmGrids").GetAllKeys().ToList();

        maxFarmIndex = numberOfFarms.Count - 1;
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

    private void UpdateSelectedFarmText() {
        selectedFarmText.text = "Viewing Farm #" + selectedFarmIndex + 1;
    }
}
