using System.Collections;
using UnityEngine;

namespace EventActions
{
    public class MakeAnimationEA: EventAction
    {
        [SerializeField] private Animator animator;
        private int zoomStep = 0;
        private static readonly int Property = Animator.StringToHash("Zoom Step");

        public override IEnumerator ActionCoroutine()
        {
            Debug.Log("ANIMATION");
            zoomStep++;
            animator.SetInteger(Property, zoomStep);
            yield break;
        }





    }
}