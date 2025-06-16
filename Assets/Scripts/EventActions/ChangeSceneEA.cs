using System.Collections;
using SceneSystem;
using UnityEngine;

namespace EventActions
{
    [CreateAssetMenu(fileName = "ChangeSceneEA", menuName = "ChangeSceneEA")]
    public class ChangeSceneEA: EventAction
    {
        [SerializeField] private int changeSceneNum;
        [SerializeField] private Scene newScene;
        
        public override IEnumerator ActionCoroutine()
        {
            GameManager.Instance.SceneTransitionManager.ChangeSceneTo(changeSceneNum, newScene);
            yield break;
        }
    }
}