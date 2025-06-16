using System.Collections;
using System.Collections.Generic;
using SceneSystem;
using UnityEngine;

namespace EventActions
{
    [CreateAssetMenu(fileName = "EnableObjectEA", menuName = "EnableObjectEA")]
    public class EnableObjectEA : EventAction
    {
        [SerializeField] private GameObject objectToDestroy;
        [SerializeField] private int fadeSpeed = 8;
        public override IEnumerator ActionCoroutine()
        {
            yield return (GameManager.Instance.SceneTransitionManager.EnableObjectCoroutine(objectToDestroy, fadeSpeed));
        }
    }
}