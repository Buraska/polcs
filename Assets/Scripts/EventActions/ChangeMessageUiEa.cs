using System.Collections;
using MessageSystem;
using UnityEngine;

namespace EventActions
{
    public class ChangeMessageUiEa : EventAction
    {
        [SerializeField] private MessageUI MessageUI;

        public override IEnumerator ActionCoroutine()
        {
            Debug.Log("Changing MessageUI");
            GameManager.Instance.MessageManager.SetMessageUi(MessageUI);
            yield break;
        }
    }
}