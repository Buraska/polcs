using System.Collections;
using UnityEngine;

namespace EventActions
{
    public class FadeTransitionScreenEa : EventAction
    {
        [SerializeField] private bool fadeOut;
        [SerializeField] private int fadeSpeed = 8;

        public override IEnumerator ActionCoroutine()
        {
            if (fadeOut)
            {
                yield return GameManager.Instance.SceneTransitionManager.StartTransition(fadeSpeed);
            }
            else yield return GameManager.Instance.SceneTransitionManager.EndTransition(fadeSpeed);
        }
    }
}