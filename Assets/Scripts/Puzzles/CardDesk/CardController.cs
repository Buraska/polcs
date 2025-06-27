using System;
using System.Collections;
using EventActions;
using UnityEngine;

namespace Puzzles.CardDesk
{
    public class CardController : MonoBehaviour
    {
        [SerializeField] private CardAndEvent[] _cards;
        private int cardsTurned = 0;
        
        private void Update()
        {
            foreach (var card in _cards)
            {
                if (card.Card.IsClicked && !card.Card.isTurned)
                {
                    StartCoroutine(_openCard(card.Card));
                }
            }
        }

        private IEnumerator _openCard(Card card)
        {
            //TODO BLOCK UI
            card.isTurned = true;
            yield return (card.openCard(_cards[cardsTurned].CardFace));
            yield return GameManager.Instance.EventManager.RunEvents(new []{_cards[cardsTurned].GameEvent});
            cardsTurned++;
        }
    }

    [System.Serializable]
    public class CardAndEvent
    {
        public Card Card;
        public Sprite CardFace;
        public GameEvent.GameEvent GameEvent;
    }
}