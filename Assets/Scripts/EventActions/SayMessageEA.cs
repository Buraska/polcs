using System.Collections;
using UnityEngine;

namespace EventActions
{
    [CreateAssetMenu(fileName = "Event action: Say message", menuName = "Event action: Say message")]
    public class SayMessageEA : EventAction
    {

        [SerializeField] private string[] messages;
        public override IEnumerator ActionCoroutine()
        {
            yield return (GameManager.Instance.MessageManager.ShowMessages(messages));
        }
    }
}