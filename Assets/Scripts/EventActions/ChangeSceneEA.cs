using System.Collections;
using EventActions.utils;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;

namespace EventActions
{
    public class ChangeSceneEA : EventAction
    {
        [SerializeField] private Scene oldScene;
        [SerializeField] private Scene newScene;
        [SerializeField] [CanBeNull] private TimeHolder time;

        private void Awake()
        {
            if (oldScene == null || newScene == null)
                Debug.LogError("Scene reference is invalid. Make sure both scenes are loaded or assigned properly.");
        }

        public override IEnumerator ActionCoroutine()
        {
            if (time == null)
            {
                time = new TimeHolder();
                time.time = 0.17f;
            }
            yield return GameManager.Instance.SceneTransitionManager.ChangeSceneToCoroutine(oldScene, newScene, time.time);
        }
    }
}