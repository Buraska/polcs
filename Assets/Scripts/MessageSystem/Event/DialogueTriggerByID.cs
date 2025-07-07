using UnityEngine;

namespace MessageSystem.Event
{
    [CreateAssetMenu(menuName = "DialogueEvents/TriggerByID")]
    public class DialogueEventTrigger : DialogEvent
    {
        public string eventId;

        public override void Trigger()
        {
            GameManager.Instance.EventManager.InvokeFromStorage(eventId);
        }
    }
}