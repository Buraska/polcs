using System.Collections;
using SceneSystem;
using UnityEngine;

namespace EventActions
{
    public class ChangeSceneEA: EventAction
    {
        [SerializeField] private int changeSceneNum;
        [SerializeField] private Scene newScene;
        
        public override IEnumerator ActionCoroutine()
        {
            yield return (GameManager.Instance.SceneTransitionManager.ChangeSceneToCoroutine(changeSceneNum, newScene));
            yield break;
        }
    }
}