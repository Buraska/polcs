using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace DigitalRuby.SoundManagerNamespace.MySoundManager
{
    public class AudioManager : MonoBehaviour
    {

        [FormerlySerializedAs("SoundDTOs")] public AudioSource[] Audios;
        
        private void PlayMusic(int index)
        {
        }

        public void PlaySound(string name, float volume)
        {
            var sound = GetSound(name);
            sound.volume = volume;
            if (sound != null)
            {
                sound.Play();
            }
        }
        
        public void PlaySound(AudioSource audioSource, float volume)
        {
            audioSource.volume = volume;
            audioSource.Play();
        }

        private AudioSource GetSound(string name)
        {
            var sound = Audios.FirstOrDefault(x => x.name == name);
            if (sound != null)
            {
                Debug.LogWarning($"Fetched sound with name '{name}'.");
                return sound;
            }

            Debug.LogWarning($"Sound with name '{name}' not found.");
            return null;
        }
        
                
        public void PlayAmbient(string name)
        {
            var sound = Audios.FirstOrDefault(x => x.name == name);
            if (sound != null)
            {
                Debug.LogWarning($"Playing sound with name '{name}'.");
                sound.PlayLoopingMusicManaged(0.15f, 1.0f, false);
            }
            else
            {
                Debug.LogWarning($"Sound with name '{name}' not found.");
            }        
        }
    }

}