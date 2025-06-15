using System.Collections;
using UnityEngine;

namespace EventActions
{
    public class SayMessageEA : BaseEA
    {

        [SerializeField] private string[] messages;
        public override IEnumerator ActionCoroutine()
        {
            yield return GameManager.Instance.StartCoroutine(MessageSystem.Instance.SayMessage(messages));
        }
    }
}