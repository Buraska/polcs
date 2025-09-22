using System.Collections;
using UnityEngine;

namespace EventActions
{
    public class ChangePitchEA : EventAction
    {
        public float Amount;

        public AudioSource AudioSource;
        public override IEnumerator ActionCoroutine()
        {
            AudioSource.pitch += Amount;
            yield break;
        }
    }
}