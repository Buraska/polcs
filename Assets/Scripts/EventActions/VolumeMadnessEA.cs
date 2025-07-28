using System.Collections;
using Animation;

namespace EventActions
{
    public class VolumeMadnessEA : EventAction
    {
        public MadnessAnimation MadnessAnimation;
        public float increasingValue;
        public float speed = 0.5f;
        public bool waitTheEnd = false;

        public override IEnumerator ActionCoroutine()
        {
            if (increasingValue > 1) increasingValue /= 100;
            if (waitTheEnd)
            {
                yield return MadnessAnimation.IncreaseVolumeTween(increasingValue, speed);
            }
            else
            {
                GameManager.Instance.StartCoroutine(MadnessAnimation.IncreaseVolumeTween(increasingValue, speed));
            }
            yield break;
        }
    }
}