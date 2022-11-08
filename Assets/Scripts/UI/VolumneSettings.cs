using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace UI
{
    //This script is currently using PlayewrPrefs to save data!
    public class VolumneSettings : MonoBehaviour
    {
        [SerializeField] AudioMixer mixer;

        [SerializeField] Slider masterSlider;
        [SerializeField] Slider sfxSlider;
        [SerializeField] Slider musicSlider;

        public const string MIXER_MASTER = "MasterVolumne";
        public const string MIXER_SFX = "SFXVolumne";
        public const string MIXER_MUSIC = "MusicVolumne";

        private void Awake()
        {
            masterSlider.onValueChanged.AddListener(SetMasterVolumne);
            sfxSlider.onValueChanged.AddListener(SetSFXVolumne);
            musicSlider.onValueChanged.AddListener(SetMusicVolumne);
        }

        private void Start()
        {
            masterSlider.value = PlayerPrefs.GetFloat(Manager.AudioManager.MASTER_KEY, 1f);
            sfxSlider.value = PlayerPrefs.GetFloat(Manager.AudioManager.SFX_KEY, 1f);
            musicSlider.value = PlayerPrefs.GetFloat(Manager.AudioManager.MUSIC_KEY, 1f);
        }

        private void OnDisable()
        {
            PlayerPrefs.SetFloat(Manager.AudioManager.MASTER_KEY, masterSlider.value);
            PlayerPrefs.SetFloat(Manager.AudioManager.SFX_KEY, sfxSlider.value);
            PlayerPrefs.SetFloat(Manager.AudioManager.MUSIC_KEY, musicSlider.value);
        }

        void SetMasterVolumne(float value)
        {
            mixer.SetFloat(MIXER_MASTER, Mathf.Log10(value) * 20);
        }

        void SetSFXVolumne(float value)
        {
            mixer.SetFloat(MIXER_SFX, Mathf.Log10(value) * 20);
        }

        void SetMusicVolumne(float value)
        {
            mixer.SetFloat(MIXER_MUSIC, Mathf.Log10(value) * 20);
        }
    }
}

