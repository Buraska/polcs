using System.Collections;

namespace EventActions
{
    public class RunEventEA : EventAction
    {
        public GameEvent.GameEvent GameEvent;
        public override IEnumerator ActionCoroutine()
        {
            yield return GameManager.Instance.EventManager.RunEvent(GameEvent);
        }
    }
}