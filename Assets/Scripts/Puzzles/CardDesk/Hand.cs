using System;
using System.Collections;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

namespace Puzzles.CardDesk
{
    public class Hand : MonoBehaviour
    {
        [SerializeField] public SpriteRenderer moveZone;
        private Camera _camera;
        private Vector3 _min;
        private Vector3 _max;

        private void Awake()
        {
            _camera = Camera.main;
            var bounds = moveZone.bounds;
            _min = bounds.min;
            _max = bounds.max;
        }

        void Update()
        {
            var mousePos = Input.mousePosition;
            mousePos.z = 10f; // расстояние от камеры до объекта, важно для Camera.main.ScreenToWorldPoint
            var worldPos = _camera.ScreenToWorldPoint(mousePos);
            if (worldPos.x < _min.x)
            {
                worldPos.x = _min.x;
            }
            if (worldPos.y < _min.y)
            {
                worldPos.y = _min.y;
            }
            if (worldPos.x > _max.x)
            {
                worldPos.x = _max.x;
            }
            if (worldPos.y > _max.y)
            {
                worldPos.y = _max.y;
            }
            
            transform.position = worldPos;
        }


    }
}