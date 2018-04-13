using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AudioSystem : MonoBehaviour
{
    public Slider MusicSlider;
    private AudioSource MainAudioSource;
    // Use this for initialization
    void Start()
    {
        MainAudioSource = GetComponent<AudioSource>();
    }
    public void UpdateMainMusicAudio()
    {
        MainAudioSource.volume = MusicSlider.value * 1 / 100;
    }
}
