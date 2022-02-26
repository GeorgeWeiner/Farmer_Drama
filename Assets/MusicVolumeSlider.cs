using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MusicVolumeSlider : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;


    public void SetMusicVolume(float sliderValue)
    {
        audioMixer.SetFloat("Music", Mathf.Log10(sliderValue));
    }

}
