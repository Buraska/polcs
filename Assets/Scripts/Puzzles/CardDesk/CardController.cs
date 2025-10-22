using System;
using System.Collections;
using UnityEngine;

namespace Puzzles.CardDesk
{
    public class CardController : MonoBehaviour
    {
        [SerializeField] private CardAndEvent[] _cardsEvents;
        private Card[] _cards;
        private int cardsTurned;

        public AudioSource cardSound;

        private void Awake()
        {
            _cards = GetComponentsInChildren<Card>();
        }

        private void Update()
        {
            foreach (var card in _cards)
                if (card.IsClicked)
                {
                    if (!card.isTurned)
                    {
                        var cardEvent = _cardsEvents[cardsTurned];
                        cardSound.Play();
                        StartCoroutine(card.openCard(cardEvent.CardFace, cardsTurned));
                        cardsTurned++;
                    }
                    GameManager.Instance.StartCoroutine(
                        GameManager.Instance.EventManager.RunEvents(new[] { _cardsEvents[card.cardOrder].GameEvent}));
                    card.IsClicked = false;
                }
        }
    }

    [Serializable]
    public class CardAndEvent
    {
        public Sprite CardFace;
        public GameEvent.GameEvent GameEvent;
    }
}