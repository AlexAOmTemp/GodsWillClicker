using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class AudioSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private Slider _masterSlider;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _effectsSlider;
    const string MASTER_VOL = "MasterChannel";
    const string MUSIC_VOL = "MusicChannel";
    const string EFFECTS_VOL = "EffectsChannel";
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        _masterSlider.onValueChanged.AddListener(onMasterVolumeChanged);
        _musicSlider.onValueChanged.AddListener(onMusicVolumeChanged);
        _effectsSlider.onValueChanged.AddListener(onEffectsVolumeChanged);
    }
    void onMasterVolumeChanged(float volume)
    {
        _audioMixer.SetFloat(MASTER_VOL, Mathf.Log10(volume) * 20);
    }
    void onMusicVolumeChanged(float volume)
    {
        _audioMixer.SetFloat(MUSIC_VOL, Mathf.Log10(volume) * 20);
    }
    void onEffectsVolumeChanged(float volume)
    {
        _audioMixer.SetFloat(EFFECTS_VOL, Mathf.Log10(volume) * 20);
    }

}
