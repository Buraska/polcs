using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Utils.MenuManagement
{
    public class SettingController : MonoBehaviour
    {
        
        [SerializeField] private AudioMixer Mixer;
        [SerializeField] private Slider SoundSlider;
        [SerializeField] private Slider AmbientSlider;

        private const string AmbientParam = "Ambient";
        private const string SoundParam = "Sound";


        public void UpdateAmbientVolume(float volume)
        {
            Mixer.SetFloat(AmbientParam, volume);
        }
        
        public void UpdateMasterVolume(float volume)
        {
            Mixer.SetFloat("Master", volume);
        }
        
        public void UpdateSoundVolume(float volume)
        {
            Mixer.SetFloat(SoundParam, volume);
        }

        public void SaveVolume()
        {
            Mixer.GetFloat(SoundParam, out float sound);
            PlayerPrefs.SetFloat(SoundParam, sound);
            
            Mixer.GetFloat(AmbientParam, out float ambient);
            PlayerPrefs.SetFloat(AmbientParam, ambient);
        }

        public void LoadVolume()
        {
            SoundSlider.value = PlayerPrefs.GetFloat(SoundParam);
            AmbientSlider.value = PlayerPrefs.GetFloat(AmbientParam);
        }
    }
}