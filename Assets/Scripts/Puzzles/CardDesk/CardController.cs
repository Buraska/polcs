using System;
using System.Collections;
using EventActions;
using UnityEngine;

namespace Puzzles.CardDesk
{
    public class CardController : MonoBehaviour
    {
        [SerializeField] private Card[] _cards;
        [SerializeField] private Sprite[] _cardsFaces;
        [SerializeField] private SayMessageEA[] _sayMessageActions;
        private int cardsTurned = 0;
        
        private void Update()
        {
            foreach (var card in _cards)
            {
                if (card.IsClicked && !card.isTurned)
                {
                    StartCoroutine(_openCard(card));
                }
            }
        }

        private IEnumerator _openCard(Card card)
        {
            //TODO BLOCK UI
            card.isTurned = true;
            yield return (card.openCard(_cardsFaces[cardsTurned]));
            yield return _sayMessageActions[cardsTurned].ActionCoroutine();
            cardsTurned++;
        }
    }
}