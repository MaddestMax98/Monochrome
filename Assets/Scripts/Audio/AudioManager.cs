using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Audio;

namespace Manager
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager instance;

        [SerializeField] AudioMixer mixer;

        public const string MASTER_KEY = "masterKey";
        public const string SFX_KEY = "sfxKey";
        public const string MUSIC_KEY = "musicKey";

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            LoadVolumne();
        }

        void LoadVolumne()
        {
            float masterVolumne = PlayerPrefs.GetFloat(MASTER_KEY, 1f);
            float sfxVolumne = PlayerPrefs.GetFloat(SFX_KEY, 1f);
            float musicVolumne = PlayerPrefs.GetFloat(MUSIC_KEY, 1f);

            mixer.SetFloat(UI.VolumneSettings.MIXER_MASTER, Mathf.Log10(masterVolumne) * 20);
            mixer.SetFloat(UI.VolumneSettings.MIXER_SFX, Mathf.Log10(sfxVolumne) * 20);
            mixer.SetFloat(UI.VolumneSettings.MIXER_MUSIC, Mathf.Log10(musicVolumne) * 20);
        }
    }

}
