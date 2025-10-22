using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Puzzles.CardDesk
{
    public class Card : MonoBehaviour, IPointerDownHandler
    {
        [NonSerialized] public bool IsClicked;
        [NonSerialized] public bool isTurned;
        public int cardOrder { get; private set;}= -1;

        [NonSerialized] public SpriteRenderer SpriteRenderer;

        private void Awake()
        {
            SpriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log("Card Clicked");
            IsClicked = true;
        }

        public IEnumerator openCard(Sprite faceSprite, int order)
        {
            cardOrder = order;
            isTurned = true;
            yield return CustomAnimation.RotateOverTime(transform, 90, 0.25f);
            SpriteRenderer.sprite = faceSprite;
            yield return CustomAnimation.RotateOverTime(transform, -90, 0.25f);
        }
    }
}