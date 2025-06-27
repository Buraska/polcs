using System.Collections;
using System.Linq;
using MessageSystem;
using UnityEngine;

namespace EventActions
{
    public class SayMessageNameEA : EventAction
    {

        [SerializeField] private SayMessageNameScript script;
        public override IEnumerator ActionCoroutine()
        {
            yield return (GameManager.Instance.MessageManager.ShowMessages(script.messages.Select(x => x.Message).ToArray(), script.messages.Select(x => x.CharacterScript.Name).ToArray()));
        }
    }
}