using System.Collections;
using DefaultNamespace;
using EventActions.utils;
using JetBrains.Annotations;
using UnityEngine;

namespace EventActions
{
    public class DisableSpriteEA : EventAction
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] [CanBeNull] private TimeHolder fadeSpeed;

        public override IEnumerator ActionCoroutine()
        {
            if (fadeSpeed == null)
            {
                fadeSpeed = new TimeHolder();
                fadeSpeed.time = 0.2f;
            }
            yield return GameManager.Instance.SceneTransitionManager.DisableSpriteCoroutine(spriteRenderer, fadeSpeed.time);
        }
    }
}