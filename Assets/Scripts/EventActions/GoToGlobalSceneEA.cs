using System.Collections;
using DefaultNamespace;
using SceneSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace EventActions
{
    public class GoToGlobalSceneEA : EventAction
    {
        [SerializeField] private string sceneName;

        public override IEnumerator ActionCoroutine()
        {
            GlobalSceneManager.Instance.LoadScene(sceneName);
            yield break;  
        }
    }
}