using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Puzzles.Hand
{
    public class PrayerHandController : BasePuzzleElement
    {
        private const int MAX_LINES = 3;
        [SerializeField] private GameObject prefabWithLine;

        private LineRenderer _currentLine;
        private GameObject _currentLinePrefab;
        private List<GameObject> _lineRenderers = new();
        private bool isDrawing;

        private bool isSolved;
        private List<HandStarController> stars;
        private Vector3 startMousePos;

        private void Start()
        {
            stars = GetComponentsInChildren<HandStarController>().ToList();
        }


        private void Update()
        {
            if (isSolved) return;
            if (GameManager.Instance.EventManager.isEventRunning()) return;


            if (Input.GetMouseButtonDown(0)) // ЛКМ нажата
            {
                startMousePos = GetMouseWorldPosition();
                isDrawing = true;
                _currentLinePrefab = Instantiate(prefabWithLine);
                _currentLine = _currentLinePrefab.GetComponent<LineRenderer>();

                if (_lineRenderers.Count >= MAX_LINES)
                {
                    DestroyAllLines();

                    foreach (var star in stars) star.SetIsSeparated(false);
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

            if (isDrawing && !Input.GetMouseButton(0)) // ЛКМ отпущена
            {
                isDrawing = false;
                if (Vector3.Distance(_currentLine.GetPosition(0), _currentLine.GetPosition(1)) < 1f)
                    Destroy(_currentLinePrefab);
                else _lineRenderers.Add(_currentLinePrefab);
                ArePointsSeparated();
                isSolved = stars.All(star => star.isSeparated);

                if (_lineRenderers.Count > 0)
                    GameManager.Instance.UIBlocker.Block();
                else GameManager.Instance.UIBlocker.Unblock();
            }
        }

        private void OnDisable()
        {
            DestroyAllLines();
        }

        private void DestroyAllLines()
        {
            foreach (var line in _lineRenderers) Destroy(line);
            isDrawing = false;
        }

        private void ArePointsSeparated()
        {
            foreach (var pointA in stars)
            {
                var separated = true;
                foreach (var pointB in stars)
                {
                    if (pointA == pointB) continue;

                    separated = _lineRenderers.Any(line => DoesLineIntersectSegment(pointA.transform.position,
                        pointB.transform.position, line.GetComponent<LineRenderer>()));
                    if (!separated) break;
                }

                pointA.SetIsSeparated(separated);
            }
        }

        private bool DoesLineIntersectSegment(Vector3 a, Vector3 b, LineRenderer line)
        {
            for (var i = 0; i < line.positionCount - 1; i++)
            {
                var l1 = line.GetPosition(i);
                var l2 = line.GetPosition(i + 1);

                if (DoSegmentsIntersect(a, b, l1, l2))
                    return true;
            }

            return false;
        }

        private bool DoSegmentsIntersect(Vector2 p1, Vector2 p2, Vector2 q1, Vector2 q2)
        {
            return Orientation(p1, p2, q1) != Orientation(p1, p2, q2) &&
                   Orientation(q1, q2, p1) != Orientation(q1, q2, p2);
        }

        private int Orientation(Vector2 a, Vector2 b, Vector2 c)
        {
            var value = (b.y - a.y) * (c.x - b.x) - (b.x - a.x) * (c.y - b.y);
            if (value == 0) return 0; // colinear
            return value > 0 ? 1 : 2; // clock or counterclock wise
        }


        private Vector3 GetMouseWorldPosition()
        {
            var screenPos = Input.mousePosition;
            screenPos.z = 10f; // расстояние от камеры — важно, если камера не в (0, 0, -10)
            return Camera.main.ScreenToWorldPoint(screenPos);
        }

        public override bool IsSolved()
        {
            return isSolved;
        }
    }
}