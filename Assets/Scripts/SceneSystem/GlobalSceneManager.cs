using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utils;

namespace SceneSystem
{
    public class GlobalSceneManager : MonoBehaviour
    {
        public static GlobalSceneManager Instance;

        [SerializeField] private Image _loaderScreen;
        [SerializeField] private Image _loaderImage;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public async void LoadScene(string sceneName)
        {
            Instance.StartCoroutine(LoadSceneCoroutine(sceneName));
        }
        
        public IEnumerator LoadSceneCoroutine(string sceneName)
        {
            yield return (SetActiveLoadScreenCoroutine(true));
            var scene = SceneManager.LoadSceneAsync(sceneName);
            scene.allowSceneActivation = false;

            _loaderImage.enabled = true;
            _loaderImage.rectTransform.DORotate(new Vector3(0, 0, 360), 1f, RotateMode.FastBeyond360)
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Restart);
            while (scene.progress < 0.90f)
            {
                // You can update a progress bar here: scene.progress is [0..0.9]
                yield return null;
            } 
            _loaderImage.enabled = false;
            scene.allowSceneActivation = true;
            
            while (!scene.isDone)
            {
                yield return null;
            }
            GameManager.Instance.SceneTransitionManager.GetScene();
            
            yield return (SetActiveLoadScreenCoroutine(false));
        }
        
        private IEnumerator SetActiveLoadScreenCoroutine(bool value)
        {
            if (value)
            {
                _loaderScreen.enabled = value;
            }
            yield return (CustomAnimation.FadeImage(_loaderScreen, !value, 1f));

            if (!value)
            {
                _loaderScreen.enabled = value;
            }

            yield return null;
        }

    }
}