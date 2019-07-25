using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ToastPopup : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI toastText;

    [SerializeField] private int waitSeconds;

    [Header("MESSAGES")]
    [TextArea(2, 3)] public string BeforeStartNetwork;
    [TextArea(2, 3)] public string BeforeStartAccelerometer;
    [TextArea(2, 3)] public string BeforeStartBoth;

    private void OnEnable() {
        UpdateText();
        StartCoroutine(KeepPopupOpenFor(waitSeconds));
    }

    private void UpdateText() {
        bool networkReady = Sensor.NetworkReady();
        bool accelerometerReady = Sensor.AccelerometerReady();

        if(!networkReady && !accelerometerReady) {
            toastText.text = BeforeStartBoth;
            waitSeconds = 7;
        } else if(!networkReady) {
            toastText.text = BeforeStartNetwork;
            waitSeconds = 4;
        } else {
            toastText.text = BeforeStartAccelerometer;
            waitSeconds = 4;
        }
    }

    private IEnumerator KeepPopupOpenFor(int seconds) {
        yield return new WaitForSeconds(seconds);

        gameObject.SetActive(false);
    }
}
