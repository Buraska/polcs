using System.Collections;
using System.Linq;
using JetBrains.Annotations;
using Puzzles;
using UnityEngine;

namespace EventTrigger
{
    public class PuzzleTrigger : Trigger
    {
        [SerializeField] private BasePuzzleElement[] _puzzleElements;
        [SerializeField] private int waitWhenSolved = 3;
        [SerializeField] [CanBeNull] private AudioSource solvingSound;


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
                Debug.Log("Puzzle solved");
                if (solvingSound != null)
                {
                    solvingSound.Play();
                }
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