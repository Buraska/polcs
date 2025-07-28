using UnityEngine;
using UnityEngine.EventSystems;

namespace Puzzles.StarMap
{
    public class Arrow : BasePuzzleElement, IPointerDownHandler
    {
        [SerializeField] public int CurrentPositionInd = 2;
        [SerializeField] private int _truePos;
        [SerializeField] private int[] _positions = { 60, 38, 12, -12, -38, -60, -90 };
        private int _transmission = 1;

        private void Awake()
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 0, _positions[CurrentPositionInd]);
        }


        public void OnPointerDown(PointerEventData eventData)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 0, _positions[Transmit()]);
        }

        private int Transmit()
        {
            if (CurrentPositionInd == 0)
                _transmission = 1;
            else if (CurrentPositionInd == _positions.Length - 1) _transmission = -1;
            CurrentPositionInd += _transmission;
            return CurrentPositionInd;
        }

        public override bool IsSolved()
        {
            return _truePos == CurrentPositionInd;
        }
    }
}