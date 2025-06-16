using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace SceneSystem
{
    public class SceneTransitionManager : MonoBehaviour
    {
    
        [SerializeField] private Scene[] localScenes;
        [SerializeField] private int currentSceneNumber = 0;
        [SerializeField] private Image sceneBlocker;
        private UIBlocker _uiBlocker;

        public void Init(UIBlocker blocker)
        {
            _uiBlocker = blocker;
            foreach (var localScene in localScenes) { localScene.SetActive(false); }
            localScenes[currentSceneNumber].SetActive(true);
        }
        public void ChangeSceneTo(int sceneNum, Scene newScene)
        {
            if (sceneNum == currentSceneNumber)
            {
                var oldScene = localScenes[sceneNum];
                localScenes[sceneNum] = newScene;
                StartCoroutine(SceneSwitchingCoroutine(oldScene));
            }

            localScenes[sceneNum] = newScene;
        }

        public IEnumerator DisableObjectCoroutine(GameObject obj, int fadeSpeed = 8)
        {
            _uiBlocker.Block();
            yield return StartCoroutine(CustomAnimation.FadeImage(obj.transform.GetComponent<SpriteRenderer>(), true, fadeSpeed));
            obj.SetActive(false);
            _uiBlocker.Unblock();
        }
    
        public IEnumerator EnableObjectCoroutine(GameObject obj, int fadeSpeed = 8)
        {
            _uiBlocker.Block();
            obj.SetActive(true);
            yield return StartCoroutine(CustomAnimation.FadeImage(obj.transform.GetComponent<SpriteRenderer>(), false, fadeSpeed));
            _uiBlocker.Unblock();
        }
    

        public IEnumerator TransitionToSceneCoroutine(int sceneNum)
        {
            _uiBlocker.Block();

            yield return StartCoroutine(CustomAnimation.FadeImage(sceneBlocker, false));
        
            localScenes[currentSceneNumber].SetActive(false);
            localScenes[sceneNum].SetActive(true);
            currentSceneNumber = sceneNum;

            yield return StartCoroutine(CustomAnimation.FadeImage(sceneBlocker, true));
            _uiBlocker.Unblock();
        }
        
        private IEnumerator SceneSwitchingCoroutine(Scene oldScene)
        {
            yield return StartCoroutine(CustomAnimation.FadeImage(sceneBlocker, false));
            oldScene.SetActive(false);
            localScenes[currentSceneNumber].SetActive(true);

            yield return StartCoroutine(CustomAnimation.FadeImage(sceneBlocker, true));
        }



    }
}

