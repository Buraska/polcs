using System;
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
    

        public IEnumerator TransitionToSceneCoroutine(int sceneNum, int speed = 8)
        {
            yield return (CustomAnimation.FadeImage(sceneBlocker, false, speed));
        
            localScenes[currentSceneNumber].SetActive(false);
            localScenes[sceneNum].SetActive(true);
            currentSceneNumber = sceneNum;

            yield return (CustomAnimation.FadeImage(sceneBlocker, true, speed));
        }
        
        public IEnumerator TransitionToSceneCoroutine(Scene scene)
        {
            yield return (CustomAnimation.FadeImage(sceneBlocker, false));
            var newSceneInd = Array.IndexOf(localScenes, scene);
            localScenes[currentSceneNumber].SetActive(false);
            localScenes[newSceneInd].SetActive(true);
            currentSceneNumber = newSceneInd;

            yield return (CustomAnimation.FadeImage(sceneBlocker, true));
        }
        public IEnumerator ChangeSceneToCoroutine(Scene oldScene, Scene newScene)
        {
            var oldSceneInd = Array.IndexOf(localScenes, oldScene);
            Debug.Log($"Change scene error. Old scene index {oldSceneInd}");

            if (oldSceneInd == -1)
            {
                Debug.Log($"Change scene error. Old scene index {oldSceneInd}");
            }
            if (oldSceneInd == currentSceneNumber)
            {
                localScenes[oldSceneInd] = newScene;
                
                yield return (CustomAnimation.FadeImage(sceneBlocker, false));
                oldScene.SetActive(false);
                localScenes[currentSceneNumber].SetActive(true);

                yield return (CustomAnimation.FadeImage(sceneBlocker, true));
            }
            else localScenes[oldSceneInd] = newScene;
        }
    }
}

