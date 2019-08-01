using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MotivationalText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI motivationText;

    [SerializeField] private string[] motivationTexts;

    [SerializeField] private int secondsToNextText;

    private int currentTextIndex;

    private IEnumerator changingText;

    private void Awake() {
        changingText = TextChanger();
    }

    private void OnEnable() {
        StartCoroutine(changingText);
    }

    private void OnDisable() {
        StopCoroutine(changingText);
    }

    private IEnumerator TextChanger() {
        while(true) {
            yield return new WaitForSeconds(secondsToNextText);

            int index = currentTextIndex;

            while(index == currentTextIndex) {
                index = Random.Range(0, motivationTexts.Length);
            }

            currentTextIndex = index;

            motivationText.text = motivationTexts[index];
        }
    }
}
