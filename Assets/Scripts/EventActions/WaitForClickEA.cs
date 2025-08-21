using System.Collections;
using UnityEngine;

namespace EventActions
{
    public class WaitForClickEA : EventAction
    {
        public override IEnumerator ActionCoroutine()
        {
            yield return new WaitUntil(() => Input.GetKeyUp(KeyCode.Mouse0) || Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.RightArrow));
        }
    }
}