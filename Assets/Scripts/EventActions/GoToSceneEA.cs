using System.Collections;
using DefaultNamespace;
using EventActions.utils;
using JetBrains.Annotations;
using UnityEngine;

namespace EventActions
{
    public class GoToSceneEA : EventAction
    {
        [SerializeField] private int sceneNum;
        [SerializeField] [CanBeNull] private TimeHolder fadeSpeed;

        public override IEnumerator ActionCoroutine()
        {
            if (fadeSpeed == null)
            {
                fadeSpeed = new TimeHolder();
                fadeSpeed.time = 0.17f;
            }
            yield return GameManager.Instance.SceneTransitionManager.TransitionToSceneCoroutine(sceneNum, fadeSpeed.time);
        }
    }
}