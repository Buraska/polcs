using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using GameEvent;

public class EventManager : MonoBehaviour
{
    private List<GameEvent.GameEvent> _events = new();
    private Coroutine _currentCoroutine;

    public bool EventsExist(GameEvent.GameEvent[] events) => events.Any(EventExists);

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

    public IEnumerator RunEvents(GameEvent.GameEvent[] gameEvents)
    {
        GameManager.Instance.UIBlocker.Block();

        var ranEvents = new List<GameEvent.GameEvent>();

        foreach (var gEvent in gameEvents)
        {
            if (gEvent == null) { continue; }
            if (!gEvent.IsActive()) continue;

            foreach (var action in gEvent.GetActions())
            {
                _currentCoroutine = StartCoroutine(action.ActionCoroutine());
                yield return _currentCoroutine;
            }

            ranEvents.Add(gEvent);
        }

        _events.AddRange(ranEvents);
        GameManager.Instance.UIBlocker.Unblock();
    }
}