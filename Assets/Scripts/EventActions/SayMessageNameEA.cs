using System.Collections;
using System.Linq;
using MessageSystem;
using MessageSystem.ScriptElement;
using UnityEngine;

namespace EventActions
{
    public class SayMessageNameEA : EventAction
    {

        [SerializeField] private DialogScript script;
        public override IEnumerator ActionCoroutine()
        {
            yield return (GameManager.Instance.MessageManager.DisplayScript(script));
        }
    }
}