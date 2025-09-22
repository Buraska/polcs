using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace EventActions
{
    public class KillDotTweenAnimation: EventAction
    {
        public GameObject AnimationObject;
        public override IEnumerator ActionCoroutine()
        {
            DOTween.Kill(AnimationObject, true);
            yield break;
        }
    }
}