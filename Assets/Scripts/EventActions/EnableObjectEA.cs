using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EventActions
{
    public class EnableObjectEA : BaseEA
    {
        [SerializeField] private GameObject objectToDestroy;
        [SerializeField] private int fadeSpeed = 8;
        public override IEnumerator ActionCoroutine()
        {
            yield return GameManager.Instance.StartCoroutine(SceneManager.Instance.EnableObjectCoroutine(objectToDestroy, fadeSpeed));
        }
    }
}