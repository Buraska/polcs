using System.Collections;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;

namespace EventActions
{
    public class PlayMusic : EventAction
    {

        [FormerlySerializedAs("audioName")] [CanBeNull]
        public AudioSource audioSource;
        public float fadeSeconds = 1.0f;
        public float startAt = 0f;
        public bool persist = false;

        public override IEnumerator ActionCoroutine()
        {
            GameManager.Instance.AudioManager.PlayMusic(audioSource, fadeSeconds, startAt, persist);

            yield break;
        }
    }
}