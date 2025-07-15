using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Puzzles.Hand
{
    public class PrayerHandController : BasePuzzleElement
    {
        [SerializeField] private GameObject prefabWithLine;
        private List<HandStarController> stars;
        private const int MAX_LINES = 3;
        private List<GameObject> _lineRenderers = new List<GameObject>();

        private LineRenderer _currentLine;
        private GameObject _currentLinePrefab;
        private Vector3 startMousePos;
        private bool isDrawing = false;

        private bool isSolved = false;

        private void Start()
        {
            stars = GetComponentsInChildren<HandStarController>().ToList();
        }


        void Update()
        {
            if (isSolved) return;
            if (_lineRenderers.Count > 0)
            {
                GameManager.Instance.UIBlocker.Block();
            }else GameManager.Instance.UIBlocker.Unblock();

            if (Input.GetMouseButtonDown(0)) // ЛКМ нажата
            {
                startMousePos = GetMouseWorldPosition();
                isDrawing = true;
                _currentLinePrefab = Instantiate(prefabWithLine);
                _currentLine = _currentLinePrefab.GetComponent<LineRenderer>();
                
                if (_lineRenderers.Count >= MAX_LINES)
                {
                    DestroyAllLines();

                    foreach (var star in stars)
                    {
                        star.SetIsSeparated(false);
                    }
                    _lineRenderers = new List<GameObject>();
                }
                
                _currentLine.positionCount = 2;
                _currentLine.SetPosition(0, startMousePos);
                _currentLine.SetPosition(1, startMousePos);
            }

            if (isDrawing && Input.GetMouseButton(0)) // ЛКМ удерживается
            {
                var currentMousePos = GetMouseWorldPosition();
                _currentLine.SetPosition(1, currentMousePos);
            }

            if (Input.GetMouseButtonUp(0)) // ЛКМ отпущена
            {
                isDrawing = false;
                if (Vector3.Distance(_currentLine.GetPosition(0), _currentLine.GetPosition(1)) < 1f)
                {
                    Destroy(_currentLinePrefab);
                }
                else _lineRenderers.Add(_currentLinePrefab);
                ArePointsSeparated();
                isSolved = stars.All(star => star.isSeparated);
            }
        }

        private void DestroyAllLines()
        {
            foreach (var line in _lineRenderers)
            {
                Destroy(line);
            }
        }

        private void OnDisable()
        {
            DestroyAllLines();
        }

        void ArePointsSeparated()
        {
            foreach (var pointA in stars)
            {
                var separated = true;
                foreach (var pointB in stars)
                {
                    if (pointA == pointB) continue;

                    separated = _lineRenderers.Any(line => DoesLineIntersectSegment(pointA.transform.position, pointB.transform.position, line.GetComponent<LineRenderer>()));
                    if (!separated)
                    {
                        break;
                    }
                }

                pointA.SetIsSeparated(separated);
            }
        }

        bool DoesLineIntersectSegment(Vector3 a, Vector3 b, LineRenderer line)
        {
            for (int i = 0; i < line.positionCount - 1; i++)
            {
                Vector3 l1 = line.GetPosition(i);
                Vector3 l2 = line.GetPosition(i + 1);

                if (DoSegmentsIntersect(a, b, l1, l2))
                    return true;
            }

            return false;
        }
        bool DoSegmentsIntersect(Vector2 p1, Vector2 p2, Vector2 q1, Vector2 q2)
        {
            return (Orientation(p1, p2, q1) != Orientation(p1, p2, q2)) &&
                   (Orientation(q1, q2, p1) != Orientation(q1, q2, p2));
        }

        int Orientation(Vector2 a, Vector2 b, Vector2 c)
        {
            float value = (b.y - a.y) * (c.x - b.x) - (b.x - a.x) * (c.y - b.y);
            if (value == 0) return 0;  // colinear
            return (value > 0) ? 1 : 2; // clock or counterclock wise
        }


        private Vector3 GetMouseWorldPosition()
        {
            Vector3 screenPos = Input.mousePosition;
            screenPos.z = 10f; // расстояние от камеры — важно, если камера не в (0, 0, -10)
            return Camera.main.ScreenToWorldPoint(screenPos);
        }

        public override bool IsSolved()
        {
            return isSolved;
        }
    }
    
}