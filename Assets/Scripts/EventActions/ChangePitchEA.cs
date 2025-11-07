using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace EventActions
{
    public class ChangePitchEA : EventAction
    {
        public float Pitch;
        public float Time;

        public AudioSource AudioSource;
        public override IEnumerator ActionCoroutine()
        {
            
            AudioSource.DOPitch(Pitch, Time);
            yield break;
        }
    }
}