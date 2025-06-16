using GameEvent;
using Inventory;
using UnityEngine;
using UnityEngine.EventSystems;

namespace EventTrigger
{
    public class ByEquipmentTrigger : Trigger, IPointerClickHandler
    {
        [SerializeField] protected ItemModel requiredItem;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (GameManager.Instance.GameStateManager.GameState == GameState.UsingItem && GameManager.Instance.InventoryManager.GetSelectedItem() == requiredItem)
            {
                GameManager.Instance.StartCoroutine(GameManager.Instance.EventManager.RunEvents(gameEvents)); ;
            }

        }

    }
}
