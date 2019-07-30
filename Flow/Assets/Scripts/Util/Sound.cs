using UnityEngine;

public class Sound : MonoBehaviour
{
    public SoundType Type;

    public AudioClip Clip;

    [Range(0, 1)] public float Volume = 1f;
    [Range(0.3f, 3f)] public float Pitch = 0.8f;

    public bool Loop;

    public AudioSource Source;
}
