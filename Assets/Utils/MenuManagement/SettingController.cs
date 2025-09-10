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

        private float SliderValueToDb(float volume) 
        // Volume from 0 to 1
        {
            var dB = Mathf.Log10(Mathf.Max(volume, 0.0001f)) * 20f;
            return dB;
        }

        public void UpdateAmbientVolume(float volume)
        {
            Mixer.SetFloat(AmbientParam, SliderValueToDb(volume));
        }
        
        public void UpdateMasterVolume(float volume)
        {
            Mixer.SetFloat("Master", SliderValueToDb(volume));
        }
        
        public void UpdateSoundVolume(float volume)
        {
            Mixer.SetFloat(SoundParam, SliderValueToDb(volume));
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
            Mixer.SetFloat(SoundParam, PlayerPrefs.GetFloat(SoundParam));
            Mixer.SetFloat(AmbientParam, PlayerPrefs.GetFloat(AmbientParam));
        }
    }
}