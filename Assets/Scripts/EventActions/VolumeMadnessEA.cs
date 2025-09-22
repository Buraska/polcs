using System.Collections;
using Anime;
using UnityEngine.Serialization;

namespace EventActions
{
    public class VolumeMadnessEA : EventAction
    {
        public MadnessAnimation MadnessAnimation;
        public float increasingValue;
        [FormerlySerializedAs("speed")] public float duration = 2f;
        public bool waitTheEnd = false;
        public bool mute = false;
        public bool completePreviousTween = true;

        public override IEnumerator ActionCoroutine()
        {
            if (increasingValue > 1) increasingValue /= 100;
            if (waitTheEnd)
            {
                yield return MadnessAnimation.IncreaseVolumeTween(increasingValue, duration, mute, completePreviousTween);
            }
            else
            {
                GameManager.Instance.StartCoroutine(MadnessAnimation.IncreaseVolumeTween(increasingValue, duration, mute, completePreviousTween));
            }
            yield break;
        }
    }
}