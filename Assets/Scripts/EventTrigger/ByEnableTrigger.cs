using System.Collections;
using UnityEngine;

namespace EventTrigger
{
    public class ByEnableTrigger : Trigger
    {
        private void OnEnable()
        {
            Debug.Log("BY enable triggered.");
            GameManager.Instance.StartCoroutine(RunWhenGameNotBusy());
        }

        public IEnumerator RunWhenGameNotBusy()
        {
            yield return new WaitUntil(() => !GameManager.Instance.UIBlocker.IsBlocked);
            GameManager.Instance.StartCoroutine(GameManager.Instance.EventManager.RunEvents(gameEvents));
        }
    }
}