using System.Collections;
using UnityEngine;

namespace EventActions
{
    public class WaitForClickEA: BaseEA
    {
        public override IEnumerator ActionCoroutine()
        {
            GameManager.Instance.BlockInterface();
            yield return new WaitUntil(() => Input.GetKeyUp(KeyCode.Mouse0));
            GameManager.Instance.UnblockInterface();
        }
    }
}