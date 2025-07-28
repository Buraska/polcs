using System;
using System.Collections;
using UnityEngine;

namespace Puzzles.Hand
{
    public class HandStarController : MonoBehaviour
    {
        [NonSerialized] public bool isSeparated;
        private SpriteRenderer starSprite;

        private void OnEnable()
        {
            starSprite = GetComponent<SpriteRenderer>();
            StartCoroutine(CustomAnimation.Blinking(starSprite, 0.25f, 0.8f));
        }

        public void SetIsSeparated(bool value)
        {
            if (value == isSeparated) return;
            isSeparated = value;
            StartCoroutine(enableStar(value));
        }

        public IEnumerator enableStar(bool value)
        {
            if (value)
            {
                starSprite.enabled = value;
                yield return CustomAnimation.FadeImage(starSprite, !value, 2);
            }
            else
            {
                yield return CustomAnimation.FadeImage(starSprite, !value, 2);
                starSprite.enabled = value;
            }
        }
    }
}