using System;
using System.Collections;
using System.Linq;
using GameEvent;
using Puzzles;
using UnityEngine;

namespace EventTrigger
{
    public class PuzzleET: BaseET
    {
        [SerializeField] private BasePuzzleElement[] puzzleElements;
        [SerializeField] private int waitWhenSolved = 3;



        private void Start()
        {
            GameManager.Instance.StartCoroutine(WaitForValues());
        }

        private IEnumerator WaitForValues()
        {
            while (true)
            {
                yield return new WaitUntil((() => ArePuzzlesSolved()));

                yield return new WaitForSecondsRealtime(waitWhenSolved);

                if (!ArePuzzlesSolved())
                {
                    continue;
                }
                GameManager.Instance.StartCoroutine(GameManager.Instance.RunEvents(gameEvents));
                yield break;
            }

            
        }

        private bool ArePuzzlesSolved()
        {
            return puzzleElements.All(puzzle => puzzle.IsSolved());
        }
    }
}