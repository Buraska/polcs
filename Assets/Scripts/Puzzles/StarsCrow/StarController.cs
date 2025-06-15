using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Update = UnityEngine.PlayerLoop.Update;

namespace Puzzles
{
    public class StarController : MonoBehaviour
    {
        
        [SerializeField]private Transform starAnchor;

        [SerializeField]private KnobController knobX;
        [SerializeField]private KnobController knobY;

        private Vector2 _startPos;
        private Vector2 _endPos;
        private SpriteRenderer _sprite;

        private const float BoardRadius = 4.4f;

        private void Update()
        {
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, GetCurrentPosition(), Time.deltaTime * 3);

            _sprite.enabled = !IsAfterTheRadius();
        }
        
        
        private void Awake()
        {   
            _startPos = gameObject.transform.position;
            var distanceToAnchor = (Vector2)starAnchor.position - _startPos;
            _endPos = distanceToAnchor / knobX.keyValue;
            _endPos.y = distanceToAnchor.y / knobY.keyValue;
            
            _sprite = ((SpriteRenderer) gameObject.GetComponent(typeof(SpriteRenderer)));

        }

        private bool IsAfterTheRadius()
        {
            if (Vector3.Distance(Vector3.zero, gameObject.transform.position) > BoardRadius)
            {
                return true;
            }
            return false;
        }

        private Vector2 GetCurrentPosition()
        {
            var currentPosition = _endPos;
            currentPosition.x *= knobX.CurrentValue;
            currentPosition.y *= knobY.CurrentValue;
            currentPosition += _startPos;
            return currentPosition;
        }

    }
}