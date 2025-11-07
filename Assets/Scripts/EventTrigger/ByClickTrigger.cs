using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using Utils;

namespace EventTrigger
{
    public class ByClickTrigger : Trigger, IPointerClickHandler
    {
        
        public TypeOfClick typeOfClick = TypeOfClick.Default;
        public void OnPointerClick(PointerEventData eventData)
        {
            if (GameManager.Instance.GameStateManager.GameState != GameState.Exploring) return;
            GameManager.Instance.StartCoroutine(GameManager.Instance.EventManager.RunEvents(gameEvents));
        }

    }
    public enum TypeOfClick
    {
        Default,
        GoTo,
    }
}