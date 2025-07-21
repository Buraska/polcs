using System.Collections;
using Inventory;
using UnityEngine;

namespace EventActions
{
    public class RemoveItemEA : EventAction
    {

        [SerializeField] private ItemModel item;
        public override IEnumerator ActionCoroutine()
        {
            yield return GameManager.Instance.InventoryManager.Remove(item);
            yield break;
        }
    }
}