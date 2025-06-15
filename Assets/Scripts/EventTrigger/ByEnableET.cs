using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace EventTrigger
{
    public class ByEnableET : BaseET
    {

        private void OnEnable()
        {
            GameManager.Instance.StartCoroutine(RunWhenGameNotBusy());
        }
        
        public IEnumerator RunWhenGameNotBusy()
        {
            yield return new WaitUntil(() => !GameManager.Instance.GameIsBusy);
            GameManager.Instance.StartCoroutine(GameManager.Instance.RunEvents(gameEvents));
        }
    }
}
