using System;
using System.Collections;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

namespace SceneSystem
{
    public class SceneTransitionManager : MonoBehaviour
    {
        [SerializeField] private Scene[] localScenes;
        [SerializeField] private int currentSceneNumber;
        [SerializeField] private Image sceneBlocker;

        public void Awake()
        {
            foreach (var localScene in localScenes) localScene.SetActive(false);
            localScenes[currentSceneNumber].SetActive(true);
            GameManager.Instance.StartCoroutine(EndTransition(GameConstants.GlobalSceneTransitionTime));
        }

        public void SetCurrentSceneNumber(int sceneNum)
        {
            if (sceneNum <= localScenes.Length - 1)
            {
                currentSceneNumber = sceneNum;
            }

            Debug.LogError($"Cannot set scene with number {sceneNum}. The index does not exist.");
        }

        public IEnumerator DisableObjectCoroutine(GameObject obj, float fadeDuration = 1)
        {
            var spriteRenderer = obj.transform.GetComponent<SpriteRenderer>();
            var image = obj.transform.GetComponent<Image>();

            if (spriteRenderer != null)
            {
                yield return CustomAnimation.FadeImage(spriteRenderer, true, fadeDuration);
            }
            else
            {
                yield return CustomAnimation.FadeImage(image, true, fadeDuration);
            }
            obj.SetActive(false);
        }

        public IEnumerator DisableSpriteCoroutine(SpriteRenderer obj, float fadeDuration = 1)
        {
            yield return CustomAnimation.FadeImage(obj.transform.GetComponent<SpriteRenderer>(), true, fadeDuration);
            obj.enabled = false;
        }

        public IEnumerator EnableObjectCoroutine(GameObject obj, float fadeDuration = 1)
        {
            obj.SetActive(true);
            var spriteRenderer = obj.transform.GetComponent<SpriteRenderer>();
            var image = obj.transform.GetComponent<Image>();

            if (spriteRenderer != null)
            {
                yield return CustomAnimation.FadeImage(spriteRenderer, false, fadeDuration);
            }
            else
            {
                yield return CustomAnimation.FadeImage(image, false, fadeDuration);
            }        }


        public IEnumerator TransitionToSceneCoroutine(int sceneNum, float duration = 1)
        {
            yield return StartTransition(duration);

            localScenes[currentSceneNumber].SetActive(false);
            localScenes[sceneNum].SetActive(true);
            currentSceneNumber = sceneNum;

            yield return EndTransition(duration);
        }

        public IEnumerator StartTransition(float duration)
        {
            sceneBlocker.enabled = true;
            yield return CustomAnimation.FadeImage(sceneBlocker, false, duration);
        }

        public IEnumerator EndTransition(float duration)
        {
            yield return CustomAnimation.FadeImage(sceneBlocker, true, duration);
            sceneBlocker.enabled = false;
        }


        public IEnumerator ChangeSceneToCoroutine(Scene oldScene, Scene newScene, float duration = 1)
        {
            var oldSceneInd = Array.IndexOf(localScenes, oldScene);

            if (oldSceneInd == -1) Debug.Log($"Change scene error. Old scene index {oldSceneInd}");
            if (oldSceneInd == currentSceneNumber)
            {
                localScenes[oldSceneInd] = newScene;

                yield return StartTransition(duration);
                oldScene.SetActive(false);
                localScenes[currentSceneNumber].SetActive(true);

                yield return EndTransition(duration);
            }
            else
            {
                localScenes[oldSceneInd] = newScene;
            }
        }
    }
}