using System.Collections;
using UnityEngine;

namespace EventActions
{
    public class SayMessageEA : EventAction
    {

        [SerializeField] private string[] messages;
        public override IEnumerator ActionCoroutine()
        {
            yield return (GameManager.Instance.MessageManager.ShowMessages(messages));
        }
    }
}