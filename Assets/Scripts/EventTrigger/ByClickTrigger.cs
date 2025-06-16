using Inventory;
using UnityEngine;
using UnityEngine.EventSystems;

namespace EventTrigger
{
    public class ByClickTrigger : Trigger, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log("OnPointerClick");
            if (GameManager.Instance.GameStateManager.GameState != GameState.Exploring) return;
            GameManager.Instance.StartCoroutine(GameManager.Instance.EventManager.RunEvents(gameEvents));
        }

    }
}
