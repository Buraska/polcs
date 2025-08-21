using System.Collections;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;

namespace EventActions
{
    public class PlayAmbient : EventAction
    {

        [FormerlySerializedAs("audioName")] public AudioSource audioSource;
        public AudioSource[] additionalAudioSources; 
        public float fadeSeconds = 1.0f;
        [CanBeNull] public Scene sceneToRewriteAmbient;

        public override IEnumerator ActionCoroutine()
        {
            GameManager.Instance.AudioManager.PlayAmbient(audioSource, additionalAudioSources, fadeSeconds);
            if (sceneToRewriteAmbient != null)
            {
                sceneToRewriteAmbient.PlayAmbientEA = this;
            }
            yield break;
        }
    }
}