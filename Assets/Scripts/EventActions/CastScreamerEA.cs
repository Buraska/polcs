using System.Collections;
using UnityEngine;

namespace EventActions
{
    public class CastScreamerEA: BaseEA
    {
        [SerializeField] private Animator animator;
        [SerializeField] private int waitBeforeStart;
        [SerializeField] private int waitAfterStart;
        [SerializeField] private SayMessageEA fakeMessage;

        private bool _isScreamerCanBeCasted;
        private static readonly int TrScreamer = Animator.StringToHash("TrScreamer");

        public override IEnumerator ActionCoroutine()
        {
            _isScreamerCanBeCasted = false;
            GameManager.Instance.StartCoroutine(CoroutineWrapper(new WaitForSecondsRealtime(waitBeforeStart))); 

            if (fakeMessage != null)
            {
                GameManager.Instance.StartCoroutine(CoroutineWrapper(fakeMessage.ActionCoroutine()));
                
                while (!_isScreamerCanBeCasted)
                {
                    yield return null;
                }
                
                MessageSystem.Instance.StopSpeaking();
            }
            
            animator.SetTrigger(TrScreamer);

            yield return new WaitForSecondsRealtime(waitAfterStart);            
        }

        private IEnumerator CoroutineWrapper(IEnumerator routine)
        {
            yield return GameManager.Instance.StartCoroutine(routine);
            _isScreamerCanBeCasted = true;
        }



    }
}