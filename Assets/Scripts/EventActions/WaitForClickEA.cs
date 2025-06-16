using System.Collections;
using UnityEngine;

namespace EventActions
{
    [CreateAssetMenu(fileName = "WaitForClickEA", menuName = "WaitForClickEA")]
    public class WaitForClickEA: EventAction
    {
        public override IEnumerator ActionCoroutine()
        {
            GameManager.Instance.UIBlocker.Block();
            yield return new WaitUntil(() => Input.GetKeyUp(KeyCode.Mouse0));
            GameManager.Instance.UIBlocker.Unblock();
        }
    }
}