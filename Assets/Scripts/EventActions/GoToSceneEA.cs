using System.Collections;
using SceneSystem;
using UnityEngine;

namespace EventActions
{
    [CreateAssetMenu(fileName = "GoToSceneEA", menuName = "GoToSceneEA")]
    public class GoToSceneEA : EventAction
    {

        [SerializeField] private int sceneNum;

        public override IEnumerator ActionCoroutine()
        {
            yield return (GameManager.Instance.SceneTransitionManager.TransitionToSceneCoroutine(sceneNum));

        }
    }
}