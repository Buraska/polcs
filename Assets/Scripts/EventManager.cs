using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    [SerializeField] private EventEntry[] _eventStorage;

    private readonly List<GameEvent.GameEvent> _events = new();
    private readonly List<Coroutine> _runningCoroutines = new();

    public void InvokeFromStorage(string id)
    {
        var gEvent = _eventStorage.FirstOrDefault(x => x.id == id)?.gameEvent;
        if (gEvent != null)
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

    public bool EventExists(GameEvent.GameEvent e)
    {
        return _events.Any(x => x.EventName == e.EventName);
    }

    public void AddEvent(GameEvent.GameEvent gEvent)
    {
        _events.Add(gEvent);
    }


    public bool isEventRunning()
    {
        if (_runningCoroutines.Count != 0) Debug.Log($"Cannot run Event. Event count is {_runningCoroutines.Count}");
        return _runningCoroutines.Count != 0;
    }

    public void UnBlockUI()
    {
        if (!isEventRunning()) GameManager.Instance.UIBlocker.Unblock();
    }

    public void BlockUI()
    {
        GameManager.Instance.UIBlocker.Block();
    }

    public IEnumerator RunAction(IEnumerator action)
    {
        BlockUI();
        yield return action;
        UnBlockUI();
    }

    public IEnumerator RunEvent(GameEvent.GameEvent gEvent)
    {
        Debug.Log($"Run event {gEvent.EventName}");
        _events.Add(gEvent); // It has to be at the begging. In other case Game event can be properly forbidden to be reproduced twice

        GameManager.Instance.UIBlocker.Block();

        if (gEvent.runAtOnce)
        {
            var tempCurrentCoroutines = new List<Coroutine>();
            foreach (var action in gEvent.GetActions())
            {
                var currentCoroutine = StartCoroutine(action.ActionCoroutine());
                tempCurrentCoroutines.Add(currentCoroutine);
            }

            foreach (var coroutine in tempCurrentCoroutines) yield return coroutine;
            tempCurrentCoroutines.Clear();
        }
        else
        {
            foreach (var action in gEvent.GetActions())
            {
                var currentCoroutine = StartCoroutine(action.ActionCoroutine());
                _runningCoroutines.Add(currentCoroutine);
                yield return currentCoroutine;
                _runningCoroutines.Remove(currentCoroutine);
            }
        }

        UnBlockUI();
        Debug.Log($"End running event {gEvent.EventName}");
    }


    public IEnumerator RunEvents(GameEvent.GameEvent[] gameEvents)
    {
        Debug.Log("Start running events");
        foreach (var gEvent in gameEvents)
        {
            if (gEvent == null) continue;

            if (!CanBeRun(gEvent))
            {
                Debug.Log($"Cant run event {gEvent.EventName}");
                continue;
            }

            yield return RunEvent(gEvent);
        }
    }

    public bool CanBeRun(GameEvent.GameEvent gEvent)
    {
        if (!gEvent.CanBeRunCustom()) return false;

        if (gEvent.requiredEvents.Length != 0 && !gEvent.requiredEvents.All(x => _events.Contains(x))) return false;

        if (gEvent.forbiddenEvents.Length != 0 && gEvent.forbiddenEvents.Any(x => _events.Contains(x))) return false;

        return true;
    }

    [Serializable]
    public class EventEntry
    {
        public string id;
        public GameEvent.GameEvent gameEvent;
    }
}