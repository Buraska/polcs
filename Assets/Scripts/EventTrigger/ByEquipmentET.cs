using GameEvent;
using UnityEngine;
using UnityEngine.EventSystems;

namespace EventTrigger
{
    public class ByEquipmentET : BaseET, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            if (InventoryManager.Instance.IsAnySlotSelected)
            {
                GameManager.Instance.StartCoroutine(GameManager.Instance.RunEvents(gameEvents)); ;
            }

        }

    }
}
