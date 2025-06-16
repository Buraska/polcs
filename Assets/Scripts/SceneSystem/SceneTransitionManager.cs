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

        public void Awake()
        {
            foreach (var localScene in localScenes) { localScene.SetActive(false); }
            localScenes[currentSceneNumber].SetActive(true);
        }

        public IEnumerator DisableObjectCoroutine(GameObject obj, int fadeSpeed = 8)
        {
            yield return (CustomAnimation.FadeImage(obj.transform.GetComponent<SpriteRenderer>(), true, fadeSpeed));
            obj.SetActive(false);
        }
    
        public IEnumerator EnableObjectCoroutine(GameObject obj, int fadeSpeed = 8)
        {
            obj.SetActive(true);
            yield return (CustomAnimation.FadeImage(obj.transform.GetComponent<SpriteRenderer>(), false, fadeSpeed));
        }
    

        public IEnumerator TransitionToSceneCoroutine(int sceneNum)
        {
            yield return (CustomAnimation.FadeImage(sceneBlocker, false));
        
            localScenes[currentSceneNumber].SetActive(false);
            localScenes[sceneNum].SetActive(true);
            currentSceneNumber = sceneNum;

            yield return (CustomAnimation.FadeImage(sceneBlocker, true));
        }
        
        public IEnumerator ChangeSceneToCoroutine(int sceneNum, Scene newScene)
        {
            if (sceneNum == currentSceneNumber)
            {
                var oldScene = localScenes[sceneNum];
                localScenes[sceneNum] = newScene;
                
                yield return (CustomAnimation.FadeImage(sceneBlocker, false));
                oldScene.SetActive(false);
                localScenes[currentSceneNumber].SetActive(true);

                yield return (CustomAnimation.FadeImage(sceneBlocker, true));
            }
            else localScenes[sceneNum] = newScene;
        }
    }
}

