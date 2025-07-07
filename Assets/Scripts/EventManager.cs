using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EventActions;
using UnityEngine;
using GameEvent;

public class EventManager : MonoBehaviour
{
    [SerializeField] private EventEntry[] _eventStorage; 

    private List<GameEvent.GameEvent> _events = new();
    private Coroutine _currentCoroutine;

    public void InvokeFromStorage(string id)
    {
        var gEvent = _eventStorage?.First(x => x.id == id).gameEvent;
        if(gEvent != null)
        {
            Debug.Log(id);
            StartCoroutine(RunEvent(gEvent));
        }
        else
        {
            Debug.LogWarning($"No event registered for ID: {id}");
        }
    }

    public bool EventsExist(GameEvent.GameEvent[] events)
    {
        return events.Any(EventExists);
    } 

    public bool EventExists(GameEvent.GameEvent e) => _events.Any(x => x.EventName== e.EventName);

    public void AddEvent(GameEvent.GameEvent gEvent) => _events.Add(gEvent);

    public void StopCurrentEvent()
    {
        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
            _currentCoroutine = null;
        }
    }

    public IEnumerator RunEvent(GameEvent.GameEvent gEvent)
    {
        GameManager.Instance.UIBlocker.Block();

        _events.Add(gEvent);
        
        foreach (var action in gEvent.GetActions())
        {
            _currentCoroutine = StartCoroutine(action.ActionCoroutine());
            yield return _currentCoroutine;
        }

        GameManager.Instance.UIBlocker.Unblock();

    }


    public IEnumerator RunEvents(GameEvent.GameEvent[] gameEvents)
    {
        foreach (var gEvent in gameEvents)
        {
            if (gEvent == null) { continue; }

            Debug.Log(CanBeRun(gEvent));
            if (!CanBeRun(gEvent)) continue;

            yield return RunEvent(gEvent);
        }
    }
    
    public bool CanBeRun(GameEvent.GameEvent gEvent)
    {
        if (!gEvent.CanBeRunCustom())
        {
            return false;
        }
        if (gEvent.requiredEvents.Length != 0 && !EventsExist(gEvent.requiredEvents))
        {
            return false;
        }

        if (gEvent.forbiddenEvents.Length != 0 && EventsExist(gEvent.forbiddenEvents))
        {
            return false;
        }

        return true;
    }
    
    [System.Serializable]
    public class EventEntry
    {
        public string id;
        public GameEvent.GameEvent gameEvent;
    }

    
}