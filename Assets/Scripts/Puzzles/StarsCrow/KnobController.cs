using System;
using System.Collections;
using System.Collections.Generic;
using Puzzles;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class KnobController :  BasePuzzleElement, IPointerDownHandler
{
    private Quaternion _previousLocation;
    private Quaternion _difference;
    private Quaternion _newPos;

    [NonSerialized] public float CurrentValue;
    public float keyValue;
    private const float Tolerance = 0.05f;
    
    private void Update()
    {
        gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, _newPos, Time.deltaTime * 3);
        CurrentValue = GetCurrentValue(gameObject.transform.rotation);
    }

    private void Awake()
    {
        CurrentValue = 0;
        _newPos = Quaternion.identity;
        _previousLocation = Quaternion.identity;
    }
    
    public override bool IsSolved()
    {
        if (Input.GetMouseButton(0))
        {
            return false;
        }
        
        return Math.Abs(CurrentValue - keyValue) < Tolerance;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
        var newPosition = GetRotation();
        _difference = newPosition * Quaternion.Inverse(_previousLocation);
        GameManager.Instance.StartCoroutine(GetPositionCoroutine());
    }

    
    private IEnumerator GetPositionCoroutine()
    {
        while (!Input.GetKeyUp(KeyCode.Mouse0))
        {
            var newPosition = GetRotation();
            
            _newPos = newPosition * Quaternion.Inverse(_difference);

            yield return  null;
        }
        _previousLocation = _newPos;

    }

    private Quaternion GetRotation()
    {
        var mousePos = Camera.main!.ScreenToWorldPoint(Input.mousePosition);
        var objectPos =  gameObject.transform.position ;
        var dir = mousePos - objectPos;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        return Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private static float GetCurrentValue(Quaternion quaternion)
    
    {
        var key = quaternion.eulerAngles.z;
        if (key > 180)
        {
            key = 360 - key;
        }
        key /= 180;

        return key;
    }


}
