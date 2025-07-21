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
            GameManager.Instance.StartCoroutine(EndTransition(2));
        }

        public IEnumerator DisableObjectCoroutine(GameObject obj, int fadeSpeed = 8)
        {
            yield return (CustomAnimation.FadeImage(obj.transform.GetComponent<SpriteRenderer>(), true, fadeSpeed));
            obj.SetActive(false);
        }
        
        public IEnumerator DisableSpriteCoroutine(SpriteRenderer obj, int fadeSpeed = 8)
        {
            yield return (CustomAnimation.FadeImage(obj.transform.GetComponent<SpriteRenderer>(), true, fadeSpeed));
            obj.enabled = false;
        }
    
        public IEnumerator EnableObjectCoroutine(GameObject obj, int fadeSpeed = 8)
        {
            obj.SetActive(true);
            yield return (CustomAnimation.FadeImage(obj.transform.GetComponent<SpriteRenderer>(), false, fadeSpeed));
        }


        public IEnumerator TransitionToSceneCoroutine(int sceneNum, int speed = 8)
        {
            yield return StartTransition(speed);
        
            localScenes[currentSceneNumber].SetActive(false);
            localScenes[sceneNum].SetActive(true);
            currentSceneNumber = sceneNum;

            yield return EndTransition(speed);
        }
        
        public IEnumerator StartTransition(int speed)
        {
            sceneBlocker.enabled = true;
            yield return (CustomAnimation.FadeImage(sceneBlocker, false, speed));
        }
    
        public IEnumerator EndTransition(int speed)
        {
            yield return (CustomAnimation.FadeImage(sceneBlocker, true, speed));
            sceneBlocker.enabled = false;
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
                
                yield return StartTransition(0);
                oldScene.SetActive(false);
                localScenes[currentSceneNumber].SetActive(true);

                yield return EndTransition(0);
            }
            else localScenes[oldSceneInd] = newScene;
        }
    }
}

