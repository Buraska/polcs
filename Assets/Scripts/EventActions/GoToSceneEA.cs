using System.Collections;
using UnityEngine;

namespace EventActions
{
    public class GoToSceneEA : BaseEA
    {

        [SerializeField] private int sceneNum;

        public override IEnumerator ActionCoroutine()
        {
            yield return GameManager.Instance.StartCoroutine(SceneManager.Instance.GoToSceneCoroutine(sceneNum));

        }
    }
}