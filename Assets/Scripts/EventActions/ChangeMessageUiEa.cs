using System.Collections;
using Inventory;
using MessageSystem;
using UnityEngine;
using UnityEngine.Serialization;

namespace EventActions
{
    public class ChangeMessageUiEa: EventAction
    {
        [SerializeField] private MessageUI MessageUI;
        
        public override IEnumerator ActionCoroutine()
        {
            GameManager.Instance.MessageManager.SetMessageUi(MessageUI);
            yield break;
        }
    }
}