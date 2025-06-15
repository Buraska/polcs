using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EventActions
{
    public class DisableObjectEA : BaseEA
    {
        [SerializeField] private GameObject objectToDisable;
        [SerializeField] private int fadeSpeed = 8;

        public override IEnumerator ActionCoroutine()
        {
            yield return GameManager.Instance.StartCoroutine(SceneManager.Instance.DisableObjectCoroutine(objectToDisable, fadeSpeed));
        }
    }
}