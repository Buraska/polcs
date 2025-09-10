using System;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;
using Utils;

namespace DigitalRuby.SoundManagerNamespace.MySoundManager
{
    public class AudioManager : MonoBehaviour
    {

        [FormerlySerializedAs("SoundDTOs")] public AudioSource[] Audios;

        private AudioSource currentSource;

        private const float VolumeScale = 0.15f;
        

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
        
                
        public void PlayAmbient(AudioSource audioSource, [CanBeNull] AudioSource[] additionalAudioSources = null, float fadeSeconds = 1.0f)
        {
            // var sound = Audios.FirstOrDefault(x => x == audioSource);
            // if (sound == null)
            // {
            //     Debug.LogWarning($"Sound with name '{audioSource}' not found. Adding Ambient into library");
            //     Audios.Append(audioSource);
            // }

            MyUtils.Log($"AudioManager. Start event action of playing ambient {audioSource.gameObject.name}");
            if (currentSource != audioSource || !audioSource.isPlaying)
            {
                MyUtils.Log($"AudioManager. Playing ambient {audioSource}");
                audioSource.PlayLoopingMusicManaged(VolumeScale, fadeSeconds, false, true);
                currentSource = audioSource;
            }
            else
            {
                MyUtils.Log($"AudioManager. Cant play. Volume - {audioSource.volume}, isPlaying - {audioSource.isPlaying}");
            }

            if (additionalAudioSources == null) return;
            foreach (var source in additionalAudioSources)
            {
                if (source.isPlaying && audioSource.volume != 0) { continue; }
                source.PlayLoopingMusicManaged(VolumeScale, fadeSeconds, false, false);
            }
        }
    }

}