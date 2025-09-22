using System;
using System.Linq;
using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;
using Utils;

namespace DigitalRuby.SoundManagerNamespace.MySoundManager
{
    public class AudioManager : MonoBehaviour
    {

        [FormerlySerializedAs("SoundDTOs")] public AudioSource[] Audios;

        private AudioSource currentAmbient;
        [CanBeNull] private AudioSource currentMusic;

        private const float VolumeScale = 0.15f;

        // private static AudioManager instance;
        //
        // void Awake()
        // {
        //     if (instance == null)
        //     {
        //         instance = this;
        //         DontDestroyOnLoad(gameObject);
        //     }
        //     else
        //     {
        //         Destroy(gameObject); // выкидываем лишнего
        //     }
        // }    

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

        public void PlayMusic([CanBeNull] AudioSource audioSource, float fadeSeconds = 1.0f, float startAt = 0f,
            bool persist = false)
        {
            if (!ReferenceEquals(audioSource, currentMusic))
            {
                if (!ReferenceEquals(audioSource, currentMusic))
                {
                    currentMusic.StopLoopingMusicManaged();
                }
                currentMusic = audioSource;
            }

            if (audioSource == null)
            {
                return;
            }
            MyUtils.Log($"AudioManager. Start event action of playing music {audioSource.gameObject.name}");
            audioSource.PlayLoopingMusicManaged(VolumeScale, fadeSeconds, persist, false);
            audioSource.time = startAt;
        } 
                
        public void PlayAmbient([CanBeNull] AudioSource audioSource, [CanBeNull] AudioSource[] additionalAudioSources = null, float fadeSeconds = 1.0f, bool additive = false)
        {
            // var sound = Audios.FirstOrDefault(x => x == audioSource);
            // if (sound == null)
            // {
            //     Debug.LogWarning($"Sound with name '{audioSource}' not found. Adding Ambient into library");
            //     Audios.Append(audioSource);
            // }
            if (audioSource == null)
            {
                currentAmbient.StopLoopingSoundManaged();
                currentAmbient = null;
            }

            MyUtils.Log($"AudioManager. Start event action of playing ambient {audioSource.gameObject.name}");
            if (currentAmbient != audioSource || !audioSource.isPlaying)
            {
                MyUtils.Log($"AudioManager. Playing ambient {audioSource}");
                audioSource.PlayLoopingMusicManaged(VolumeScale, fadeSeconds, false, !additive);
                currentAmbient = audioSource;
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