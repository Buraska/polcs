using System.Collections;
using SceneSystem;
using UnityEngine;

namespace EventActions
{
    public class GoToSceneEA : EventAction
    {

        [SerializeField] private int sceneNum;

        public override IEnumerator ActionCoroutine()
        {
            yield return (GameManager.Instance.SceneTransitionManager.TransitionToSceneCoroutine(sceneNum));

        }
    }
}