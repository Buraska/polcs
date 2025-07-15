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
    private List<Coroutine> _runningCoroutines = new List<Coroutine>();

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


    public bool isEventRunning()
    {
        return _runningCoroutines.Count != 0;
    }

    public IEnumerator RunEvent(GameEvent.GameEvent gEvent)
    {
        Debug.Log($"Run event {gEvent.EventName}");
        GameManager.Instance.UIBlocker.Block();

        _events.Add(gEvent);
        
        foreach (var action in gEvent.GetActions())
        {
            var currentCoroutine = StartCoroutine(action.ActionCoroutine());
            _runningCoroutines.Add(currentCoroutine);
            yield return currentCoroutine;
            _runningCoroutines.Remove(currentCoroutine);
        }

        if (!isEventRunning())
        {
            GameManager.Instance.UIBlocker.Unblock();
        }
    }


    public IEnumerator RunEvents(GameEvent.GameEvent[] gameEvents)
    {
        foreach (var gEvent in gameEvents)
        {
            if (gEvent == null) { continue; }

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

        if (gEvent.requiredEvents.Length != 0 && !gEvent.requiredEvents.All(x => _events.Contains(x)))
        {
            return false;
        }

        if (gEvent.forbiddenEvents.Length != 0 && gEvent.forbiddenEvents.Any(x => _events.Contains(x)))
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