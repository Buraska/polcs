using System.Collections;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace EventActions
{
    public class WaitForClickEA : EventAction
    {
        public bool includeMovementKeys = false;
        public override IEnumerator ActionCoroutine()
        {
            KeyCode[] keys = { KeyCode.Mouse0, KeyCode.Space, KeyCode.RightArrow };
            if (includeMovementKeys)
            {
                keys = keys.Concat(new []{KeyCode.LeftArrow, KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.A, KeyCode.D, KeyCode.W, KeyCode.S}).ToArray();
            }
            yield return new WaitUntil(() => keys.Any(Input.GetKeyUp));
        }
    }
}