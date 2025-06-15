using System.Collections;
using UnityEngine;

namespace EventActions
{
    public class ChangeSceneEA: BaseEA
    {
        [SerializeField] private int changeSceneNum;
        [SerializeField] private SceneController newScene;
        
        public override IEnumerator ActionCoroutine()
        {
            SceneManager.Instance.ChangeSceneTo(changeSceneNum, newScene);
            yield break;
        }
    }
}