using System.Collections;
using System.Collections.Generic;
using SceneSystem;
using UnityEngine;

namespace EventActions
{
    public class DisableSpriteEA : EventAction
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private int fadeSpeed = 8;

        public override IEnumerator ActionCoroutine()
        {
            yield return (GameManager.Instance.SceneTransitionManager.DisableSpriteCoroutine(spriteRenderer, fadeSpeed));
        }
    }
}