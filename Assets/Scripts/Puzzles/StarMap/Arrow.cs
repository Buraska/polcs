using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Puzzles.StarMap
{
    public class Arrow : BasePuzzleElement, IPointerDownHandler
    {
        [SerializeField] public int CurrentPositionInd = 2;
        [SerializeField] private int _truePos;
        [SerializeField] private int[] _positions = { 60, 38, 12, -12, -38, -60, -90 };
        [SerializeField] private AudioSource _arrowSound;

        private Quaternion _newPos;
        private Quaternion _previousLocation;
        private Quaternion _difference;
        [NonSerialized] public int CurrentValue;

        private int _transmission = 1;

        private void Awake()
        {
            _newPos = gameObject.transform.rotation;
            _previousLocation = _newPos;
        }
        
        private void Update()
        {
            var currentRotation = gameObject.transform.rotation;
            gameObject.transform.rotation = Quaternion.Slerp(currentRotation, _newPos, Time.deltaTime * 3);
            var positionIndex = GetCurrentPositionIndex(currentRotation);
            if (positionIndex != CurrentPositionInd)
            {
                Debug.Log("WTF");
                Debug.Log(positionIndex);
                Debug.Log(CurrentPositionInd);
                _arrowSound.Play();
            }
            CurrentPositionInd = positionIndex;
        }


        public void OnPointerDown(PointerEventData eventData)
        {
            var newPosition = GetRotation();
            _difference = newPosition * Quaternion.Inverse(_previousLocation);
            GameManager.Instance.StartCoroutine(GetPositionCoroutine());
        }

        private int GetCurrentPositionIndex(Quaternion q)

        {
            var angle = GetAngle(q);
            int tolerance = 14;
            
            for (int i = 0; i < _positions.Length; i++)
            {
                var pos = _positions[i];
                if (angle >= (pos - tolerance) && angle <= (pos + tolerance))
                {
                    return i;
                }
            }

            Debug.Log("Cant find proper position.");
            return -1;
        }

        private float GetAngle(Quaternion quaternion)
        {
            var curPos = (quaternion.eulerAngles.z);
            if (curPos > 180) curPos -= 360;
            return curPos;
        }
        private IEnumerator GetPositionCoroutine()
        {
            Quaternion currentPos;
            while (!Input.GetKeyUp(KeyCode.Mouse0))
            {
                var newPosition = GetRotation();
                currentPos =  newPosition * Quaternion.Inverse(_difference);
                var curPos = GetAngle(currentPos);

                if (curPos is < 75 and > -93)
                {
                    _newPos = currentPos;
                }

                yield return null;
            }
            _previousLocation = _newPos;
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
            if (Input.GetKey(KeyCode.Mouse0))
            {
                return false;
            }
            return _truePos == CurrentPositionInd;
        }
        
        private Quaternion GetRotation()
        {
            var mousePos = Camera.main!.ScreenToWorldPoint(Input.mousePosition);
            var objectPos = gameObject.transform.position;
            var dir = mousePos - objectPos;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            return Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}