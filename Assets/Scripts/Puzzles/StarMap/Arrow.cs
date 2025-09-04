using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using Utils;

namespace Puzzles.StarMap
{
    public class Arrow : BasePuzzleElement, IPointerDownHandler
    {
        private const int PositionTolerance = 15;
        private const int MinAllowedAngle = -93;
        private const int MaxAllowedAngle = 75;
        
        [SerializeField] public int CurrentPositionInd = 2;
        [SerializeField] private int _truePos;
        [SerializeField] private int[] _positions = { 60, 38, 12, -12, -38, -60, -90 };
        [SerializeField] private AudioSource _arrowSound;

        private Quaternion _newPos;
        private Quaternion _previousLocation;
        private Quaternion _difference;
        [NonSerialized] public int CurrentValue;
        private Transform _tr;
        private Camera _cam;


        private int _transmission = 1;

        private void Awake()
        {
            _cam = Camera.main;
            _tr = transform;
            _newPos = _tr.rotation;
            _previousLocation = _newPos;
        }
        
        private void Update()
        {
            var currentRotation = gameObject.transform.rotation;
            _tr.rotation = Quaternion.RotateTowards(currentRotation, _newPos, 180 * Time.deltaTime);
            var positionIndex = GetCurrentPositionIndex(currentRotation);
            if (positionIndex != CurrentPositionInd)
            {
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
            
            for (int i = 0; i < _positions.Length; i++)
            {
                var pos = _positions[i];
                if (angle >= (pos - PositionTolerance) && angle <= (pos + PositionTolerance))
                {
                    return i;
                }
            }
            MyUtils.Log($"Cant find proper position. Position array is {string.Join(", ", _positions)}");
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

                if (curPos is < MaxAllowedAngle and > MinAllowedAngle)
                {
                    _newPos = currentPos;
                }

                yield return null;
            }
            _previousLocation = _newPos;
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
            var mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);
            var objectPos = gameObject.transform.position;
            var dir = mousePos - objectPos;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            return Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}