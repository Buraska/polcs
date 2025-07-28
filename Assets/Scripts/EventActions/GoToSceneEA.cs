using System.Collections;
using UnityEngine;

namespace EventActions
{
    public class GoToSceneEA : EventAction
    {
        [SerializeField] private int sceneNum;
        [SerializeField] private int fadeSpeed = 8;

        public override IEnumerator ActionCoroutine()
        {
            yield return GameManager.Instance.SceneTransitionManager.TransitionToSceneCoroutine(sceneNum, fadeSpeed);
        }
    }
}