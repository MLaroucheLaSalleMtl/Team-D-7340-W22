using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Audio : MonoBehaviour
{
    public Slider SoundSlider;
    public Slider SfxSlider;
    public AudioMixer audiomixer;
    
    void Update()
    {
        AudioSlider();
    }
    private void AudioSlider()
    {
        audiomixer.SetFloat("BGM", SoundSlider.value);
    }
}
