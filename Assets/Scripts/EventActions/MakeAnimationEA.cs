using System.Collections;
using UnityEngine;

namespace EventActions
{
    public class MakeAnimationEA: EventAction
    {
        [SerializeField] private Animator animator;
        private static readonly int Property = Animator.StringToHash("trigger");

        public override IEnumerator ActionCoroutine()
        {
            Debug.Log("ANIMATION");
            animator.SetTrigger(Property);
            yield break;
        }





    }
}