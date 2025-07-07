using System.Collections;
using MessageSystem;
using MessageSystem.ScriptElement;
using UnityEngine;

namespace EventActions
{
    public class SayMessageEA : EventAction
    {

        [SerializeField] private SayMessageScript script;
        public override IEnumerator ActionCoroutine()
        {
            yield return (GameManager.Instance.MessageManager.DisplayMessages(script.messages));
        }
    }
}