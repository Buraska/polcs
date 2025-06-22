using System.Collections;
using System.Collections.Generic;
using SceneSystem;
using UnityEngine;

namespace EventActions
{
    public class DisableObjectEA : EventAction
    {
        [SerializeField] private GameObject objectToDisable;
        [SerializeField] private int fadeSpeed = 8;

        public override IEnumerator ActionCoroutine()
        {
            yield return (GameManager.Instance.SceneTransitionManager.DisableObjectCoroutine(objectToDisable, fadeSpeed));
        }
    }
}