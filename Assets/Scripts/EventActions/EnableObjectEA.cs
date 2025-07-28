using System.Collections;
using UnityEngine;

namespace EventActions
{
    public class EnableObjectEA : EventAction
    {
        [SerializeField] private GameObject objectToDestroy;
        [SerializeField] private int fadeSpeed = 8;

        public override IEnumerator ActionCoroutine()
        {
            yield return GameManager.Instance.SceneTransitionManager.EnableObjectCoroutine(objectToDestroy, fadeSpeed);
        }
    }
}