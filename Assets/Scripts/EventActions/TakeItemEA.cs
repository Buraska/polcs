using System.Collections;
using UnityEngine;

namespace EventActions
{
    public class TakeItemEA : BaseEA
    {

        [SerializeField] private ItemModel item;
        public override IEnumerator ActionCoroutine()
        {
            InventoryManager.Instance.Add(item);
            yield break;
        }
    }
}