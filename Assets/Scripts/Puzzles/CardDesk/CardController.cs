using System;
using System.Collections;
using UnityEngine;

namespace Puzzles.CardDesk
{
    public class CardController : MonoBehaviour
    {
        [SerializeField] private CardAndEvent[] _cards;
        private int cardsTurned;

        public AudioSource cardSound;

        private void Update()
        {
            foreach (var card in _cards)
                if (card.Card.IsClicked && !card.Card.isTurned)
                    StartCoroutine(_openCard(card.Card));
        }

        private IEnumerator _openCard(Card card)
        { 
            cardSound.Play();
            card.isTurned = true;
            yield return GameManager.Instance.EventManager.RunAction(card.openCard(_cards[cardsTurned].CardFace));
            
            GameManager.Instance.StartCoroutine(
                GameManager.Instance.EventManager.RunEvents(new[] { _cards[cardsTurned].GameEvent }));
            cardsTurned++;
        }
    }

    [Serializable]
    public class CardAndEvent
    {
        public Card Card;
        public Sprite CardFace;
        public GameEvent.GameEvent GameEvent;
    }
}