using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] private Sound[] sounds;

    private Dictionary<SoundType, AudioSource> soundTypesDict = new Dictionary<SoundType, AudioSource>();

    private void Awake() {
        InitializeSounds();

        void InitializeSounds() {
            foreach(Sound sound in sounds) {
                sound.Source = gameObject.AddComponent<AudioSource>();
                sound.Source.clip = sound.Clip;

                sound.Source.volume = sound.Volume;
                sound.Source.pitch = sound.Pitch;
                sound.Source.loop = sound.Loop;

                soundTypesDict.Add(sound.Type, sound.Source);
            }
        }
    }

    public void PlaySound(SoundType type) {
        soundTypesDict.TryGetValue(type, out AudioSource source);

        if(source != null) {
            source.Play();
        } else {
            Debug.LogWarning("Tried to play clip: " + type + " but not found!");
        }
    }

    public void StopSound(SoundType type) {
        soundTypesDict.TryGetValue(type, out AudioSource source);

        if(source != null) {
            source.Stop();
        } else {
            Debug.LogWarning("Tried to stop clip: " + type + " but not found!");
        }
    }
}
