using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

public class AudioSystem : MonoBehaviour
{
    public static AudioSystem instance = null;
    public FloatVariable SavedVolume;
    public AudioSource MainAudioSource;
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if(instance !=this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start()
    {
        MainAudioSource = GetComponent<AudioSource>();
    }
    public void UpdateMainMusicAudio()
    {
        var vol = SavedVolume.Value;
        MainAudioSource.volume = vol;
    }
    public void SaveVolume()
    {
        SavedVolume.Value = MainAudioSource.volume;
    }
}
