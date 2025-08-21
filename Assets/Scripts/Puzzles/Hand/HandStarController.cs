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
            StartCoroutine(EnableStar(value));
        }

        public IEnumerator EnableStar(bool value)
        {
            if (value)
            {
                starSprite.enabled = value;
                yield return CustomAnimation.FadeImage(starSprite, !value, 1);
            }
            else
            {
                yield return CustomAnimation.FadeImage(starSprite, !value, 1);
                starSprite.enabled = value;
            }
        }
    }
}