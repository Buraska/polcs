using System.Collections;
using MessageSystem.ScriptElement;
using UnityEngine;

namespace EventActions
{
    public class SayMessageNameEA : EventAction
    {
        [SerializeField] private DialogScript script;

        public override IEnumerator ActionCoroutine()
        {
            yield return GameManager.Instance.MessageManager.DisplayScript(script);
        }
    }
}