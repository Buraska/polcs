using System.Collections;
using EventActions;
using UnityEngine;

namespace Puzzles.Nightmare
{
    public class NightmareZoomEA: EventAction
    {
        [SerializeField] private NightmareController _nightmareController;
        [SerializeField] private Animator animator;
        private static readonly int Property = Animator.StringToHash("Zoom Step");

        public override IEnumerator ActionCoroutine()
        {
            animator.SetInteger(Property, _nightmareController.ZoomIn());
            yield break;
        }
    }
}