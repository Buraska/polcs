using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EventTrigger;
using GameEvent;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;


    private List<BaseGE> _events = new();

    private IEnumerator _currentCor = null;

    [SerializeField] private Image uiBlock;

    [NonSerialized]
    public bool GameIsBusy;


    public void Awake()
    {
        Instance = this;
    }
    
    public void BlockInterface()
    {
        uiBlock.enabled = true;
        GameIsBusy = true;
    }

    public void UnblockInterface()
    {
        uiBlock.enabled = false;
        GameIsBusy = false;
    }


    public bool EventsExist(BaseGE[] events)
    {

        return events.Any(EventExists);
    }
    public bool EventExists(BaseGE e)
    {

        return _events.Any(x => x.GetName() == e.GetName());
    }

    public void BreakCurrentCoroutine()
    {
        if (_currentCor == null)
        {
            return;
        }
        print("Hi");
        StopCoroutine(_currentCor);
    }

    
    public void AddEvent(BaseGE gEvent)
    {
        _events.Add(gEvent);
    }



    public IEnumerator RunEvents(BaseGE[] gameEvents)
    {
        Instance.GameIsBusy = true;
        var ranEvents = new List<BaseGE>();

        foreach (var gEvent in gameEvents)
        {

            if (gEvent.IsActive())
            {
                print("Is active");
                foreach ( var action in gEvent.GetActions())
                {
                    _currentCor = action.ActionCoroutine();
                    yield return StartCoroutine(_currentCor);
                }
                ranEvents.Add(gEvent);
            }
        }

        _events.AddRange(ranEvents);
            
        Instance.GameIsBusy = false;

    }
}

