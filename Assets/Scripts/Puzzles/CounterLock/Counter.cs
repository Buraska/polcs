using TMPro;
using UnityEngine;

namespace Puzzles.CounterLock
{
    public class Counter : BasePuzzleElement
    {
        [SerializeField] private TextMeshProUGUI _text;

        [SerializeField] private int keyNumber;

        [SerializeField] private int currentNumber;

        private void Awake()
        {
            _text = gameObject.GetComponentInChildren(typeof(TextMeshProUGUI)) as TextMeshProUGUI;
            UpdateNum();
        }

        public override bool IsSolved()
        {
            return currentNumber == keyNumber;
        }

        public void MinusNum()
        {
            currentNumber--;
            if (currentNumber < 0) currentNumber = 9;
            UpdateNum();
        }

        public void PlusNum()
        {
            currentNumber++;
            if (currentNumber > 9) currentNumber = 0;
            UpdateNum();
        }

        private void UpdateNum()
        {
            _text.text = currentNumber.ToString();
        }
    }
}