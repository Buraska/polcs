using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace EventActions
{
    public class WaitForTimeEA : EventAction
    {
        public float time;


        public override IEnumerator ActionCoroutine()
        {
            yield return new WaitForSeconds(time);
        }

    }
}