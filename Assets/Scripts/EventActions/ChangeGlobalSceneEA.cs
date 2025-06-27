using System.Collections;
using SceneSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace EventActions
{
    public class ChangeGlobalSceneEA: EventAction
    {
        [SerializeField] private int changeSceneNum;
        
        public override IEnumerator ActionCoroutine()
        {
            SceneManager.LoadScene(changeSceneNum);
            yield break;
        }
    }
}