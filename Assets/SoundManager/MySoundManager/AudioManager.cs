﻿using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace DigitalRuby.SoundManagerNamespace.MySoundManager
{
    public class AudioManager : MonoBehaviour
    {

        [FormerlySerializedAs("SoundDTOs")] public AudioSource[] Audios;

        private AudioSource currentSource;
        
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
        
                
        public void PlayAmbient(AudioSource audioSource, AudioSource[] additionalAudioSources, float fadeSeconds = 1.0f)
        {
            var sound = Audios.FirstOrDefault(x => x == audioSource);
            if (sound == null)
            {
                Debug.LogWarning($"Sound with name '{audioSource}' not found. Adding Ambient into library");
                Audios.Append(audioSource);
            }

            if (currentSource != audioSource)
            {
                audioSource.PlayLoopingMusicManaged(0.15f, fadeSeconds, false);
                currentSource = audioSource;
            }

            if (additionalAudioSources == null) return;
            foreach (var source in additionalAudioSources)
            {
                source.PlayLoopingMusicManaged(0.15f, fadeSeconds, false, false);
            }
        }
    }

}