using System.Collections;
using DefaultNamespace;
using EventActions.utils;
using JetBrains.Annotations;
using UnityEngine;

namespace EventActions
{
    public class FadeTransitionScreenEa : EventAction
    {
        [SerializeField] private bool fadeOut;
        [SerializeField] [CanBeNull] private TimeHolder fadeSpeed;

        public override IEnumerator ActionCoroutine()
        {
            if (fadeSpeed == null)
            {
                fadeSpeed = new TimeHolder();
                fadeSpeed.time = 0.17f;
            }
            if (fadeOut)
            {
                yield return GameManager.Instance.SceneTransitionManager.StartTransition(fadeSpeed.time);
            }
            else yield return GameManager.Instance.SceneTransitionManager.EndTransition(fadeSpeed.time);
        }
    }
}