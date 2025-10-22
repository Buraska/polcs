using System.Collections;
using MessageSystem;
using UnityEngine;

namespace EventActions
{
    public class WaitForTimeInMusicEA : EventAction
    {
        [SerializeField] private float time;
        [SerializeField] private AudioSource _audioSource;

        public override IEnumerator ActionCoroutine()
        {
            if (!_audioSource.isPlaying)
            {
                yield break;
            }

            while (_audioSource.time < time)
            {
                yield return null;
            }
        }
    }
}