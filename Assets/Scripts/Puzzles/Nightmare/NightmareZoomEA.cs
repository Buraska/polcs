using System.Collections;
using EventActions;
using UnityEngine;

namespace Puzzles.Nightmare
{
    public class NightmareZoomEA : EventAction
    {
        private static readonly int Property = Animator.StringToHash("Zoom Step");
        [SerializeField] private NightmareController _nightmareController;
        [SerializeField] private Animator animator;

        public override IEnumerator ActionCoroutine()
        {
            animator.SetInteger(Property, _nightmareController.ZoomIn());
            yield break;
        }
    }
}