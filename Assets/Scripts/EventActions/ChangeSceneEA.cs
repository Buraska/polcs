using System;
using System.Collections;
using SceneSystem;
using UnityEngine;

namespace EventActions
{
    public class ChangeSceneEA: EventAction
    {
        [SerializeField] private Scene oldScene;
        [SerializeField] private Scene newScene;

        private void Awake()
        {
            if (oldScene == null ||  newScene == null)
            {
                Debug.LogError("Scene reference is invalid. Make sure both scenes are loaded or assigned properly.");
            }
            
        }

        public override IEnumerator ActionCoroutine()
        {
            yield return (GameManager.Instance.SceneTransitionManager.ChangeSceneToCoroutine(oldScene, newScene));
        }
    }
}