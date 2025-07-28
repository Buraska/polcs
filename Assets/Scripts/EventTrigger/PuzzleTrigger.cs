using System.Collections;
using System.Linq;
using Puzzles;
using UnityEngine;

namespace EventTrigger
{
    public class PuzzleTrigger : Trigger
    {
        [SerializeField] private BasePuzzleElement[] _puzzleElements;
        [SerializeField] private int waitWhenSolved = 3;


        private void Start()
        {
            GameManager.Instance.StartCoroutine(WaitForValues());
        }

        private IEnumerator WaitForValues()
        {
            while (true)
            {
                yield return new WaitUntil(() => ArePuzzlesSolved());

                yield return new WaitForSecondsRealtime(waitWhenSolved);

                if (!ArePuzzlesSolved()) continue;
                Debug.Log("HERE");
                
                GameManager.Instance.StartCoroutine(GameManager.Instance.EventManager.RunEvents(gameEvents));
                yield break;
            }
        }

        private bool ArePuzzlesSolved()
        {
            return _puzzleElements.All(puzzle => puzzle.IsSolved());
        }
    }
}