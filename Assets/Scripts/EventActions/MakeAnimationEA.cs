using System.Collections;
using UnityEngine;

namespace EventActions
{
    public class MakeAnimationEA : EventAction
    {
        private static readonly int Property = Animator.StringToHash("trigger");
        [SerializeField] private Animator animator;

        public override IEnumerator ActionCoroutine()
        {
            Debug.Log("ANIMATION");
            animator.SetTrigger(Property);
            yield break;
        }
    }
}