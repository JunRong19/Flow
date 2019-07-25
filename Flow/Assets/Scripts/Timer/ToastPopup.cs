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

    [TextArea(2, 3)] public string AfterStartNetwork;
    [TextArea(2, 3)] public string AfterStartAccelerometer;
    [TextArea(2, 3)] public string AfterStartGiveUp;

    private void OnEnable() {
        StartCoroutine(KeepPopupOpenFor(waitSeconds));
    }

    public void UpdateTextForStart() {
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

    public void UpdateTextForEnd() {
        bool networkReady = Sensor.NetworkReady();
        bool accelerometerReady = Sensor.AccelerometerReady();

        if(!networkReady) {
            toastText.text = AfterStartNetwork;
            waitSeconds = 4;
        } else if(!accelerometerReady) {
            toastText.text = AfterStartAccelerometer;
            waitSeconds = 4;
        } else {
            waitSeconds = 4;
            toastText.text = AfterStartGiveUp;
            waitSeconds = 4;
        }
    }

    private IEnumerator KeepPopupOpenFor(int seconds) {
        yield return new WaitForSeconds(seconds);

        gameObject.SetActive(false);
    }
}
