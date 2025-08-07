using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace EventActions
{
    public class PlaySound : EventAction
    {

        public AudioSource AudioSource;
        public float volume = 0.15f;

        public override IEnumerator ActionCoroutine()
        {
            GameManager.Instance.AudioManager.PlaySound(AudioSource, volume);
            yield break;
        }
    }
}