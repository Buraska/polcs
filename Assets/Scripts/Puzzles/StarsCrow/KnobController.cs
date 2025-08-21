using System;
using System.Collections;
using JetBrains.Annotations;
using Puzzles;
using UnityEngine;
using UnityEngine.EventSystems;

public class KnobController : BasePuzzleElement, IPointerDownHandler
{
    private const float Tolerance = 0.05f;
    public float keyValue;
    
    [CanBeNull] public KnobController connectedKnob;
    private Quaternion _difference;
    private Quaternion _newPos;
    private Quaternion _previousLocation;

    [NonSerialized] public float CurrentValue;

    private void Awake()
    {
        CurrentValue = 0;
        _newPos = Quaternion.identity;
        _previousLocation = Quaternion.identity;
    }

    private void Update()
    {
        gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, _newPos, Time.deltaTime * 3);
        CurrentValue = GetCurrentValue(gameObject.transform.rotation);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        var newPosition = GetRotation();
        _difference = newPosition * Quaternion.Inverse(_previousLocation);
        GameManager.Instance.StartCoroutine(GetPositionCoroutine());
        if (connectedKnob != null)
        {
            connectedKnob.OnPointerDown(eventData);
        }
    }

    public override bool IsSolved()
    {
        if (Input.GetMouseButton(0)) return false;

        return Math.Abs(CurrentValue - keyValue) < Tolerance;
    }


    private IEnumerator GetPositionCoroutine()
    {
        while (!Input.GetKeyUp(KeyCode.Mouse0))
        {
            var newPosition = GetRotation();

            _newPos = newPosition * Quaternion.Inverse(_difference);

            yield return null;
        }

        _previousLocation = _newPos;
    }

    private Quaternion GetRotation()
    {
        var mousePos = Camera.main!.ScreenToWorldPoint(Input.mousePosition);
        var objectPos = gameObject.transform.position;
        var dir = mousePos - objectPos;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        return Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private static float GetCurrentValue(Quaternion quaternion)

    {
        var key = quaternion.eulerAngles.z;
        if (key > 180) key = 360 - key;
        key /= 180;

        return key;
    }
}