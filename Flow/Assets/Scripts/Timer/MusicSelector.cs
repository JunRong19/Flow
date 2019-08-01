using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BayatGames.SaveGameFree;

public class MusicSelector : MonoBehaviour
{
    [SerializeField] private SoundType songSelected;

    private SoundManager soundManager;

    private void Awake() {
        soundManager = SoundManager.Instance;

        if(!SaveGame.Exists("SelectedMusic")) {
            songSelected = SoundType.NATURE_01;
        } else {
            songSelected = SaveGame.Load<SoundType>("SelectedMusic");
        }
    }

    private void OnDisable() {
        SaveGame.Save<SoundType>("SelectedMusic", songSelected);
    }

    public void ToggleMusicPlaying(bool state) {
        if(state) {
            PlayMusic();
        } else {
            StopMusic();
        }
    }

    public void ChangeMusic(SoundType type) {
        StopMusic();
        songSelected = type;
        PlayMusic();
    }

    private void PlayMusic() {
        soundManager.PlaySound(songSelected);
    }

    private void StopMusic() {
        soundManager.StopSound(songSelected);
    }
}
