using UnityEngine;

namespace Puzzles
{
    public class BasePuzzleElement : MonoBehaviour
    {
        public virtual bool IsSolved()
        {
            return false;
        }
    }
}