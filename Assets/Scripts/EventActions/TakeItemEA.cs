using System.Collections;
using Inventory;
using UnityEngine;

namespace EventActions
{
    [CreateAssetMenu(fileName = "Event action: TakeItemEA", menuName = "Event action: TakeItemEA")]
    public class TakeItemEA : EventAction
    {

        [SerializeField] private ItemModel item;
        public override IEnumerator ActionCoroutine()
        {
            GameManager.Instance.InventoryManager.Add(item);
            yield break;
        }
    }
}