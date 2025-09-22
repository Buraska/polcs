using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace EventActions
{
    public class SkipMessageAfterTimeEA : EventAction
    {
        public float time;


        public override IEnumerator ActionCoroutine()
        {
            GameManager.Instance.StartCoroutine(SkipAfterTime(time));
            yield break;
        }

        private IEnumerator SkipAfterTime(float time)
        {
            yield return new WaitForSeconds(time);
            GameManager.Instance.MessageManager.NextMessage();
        }
    }
}