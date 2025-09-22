using System.Collections;
using UnityEngine;

namespace EventActions
{
    public class RunEventEA : EventAction
    {
        public GameEvent.GameEvent GameEvent;
        public float timeToWait = 0f;
        public override IEnumerator ActionCoroutine()
        {
            if (timeToWait != 0f)
            {
                GameManager.Instance.StartCoroutine(RunAfterTime());
                yield break;
            }
            yield return GameManager.Instance.EventManager.RunEvent(GameEvent);
        }

        public IEnumerator RunAfterTime()
        {
            yield return new WaitForSeconds(timeToWait);
            yield return GameManager.Instance.EventManager.RunEvent(GameEvent);
        }
    }
}