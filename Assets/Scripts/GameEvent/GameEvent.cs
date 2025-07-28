using EventActions;
using UnityEngine;

namespace GameEvent
{
    public class GameEvent : MonoBehaviour
    {
        public bool runAtOnce;

        [SerializeField] public GameEvent[] requiredEvents;

        [SerializeField] public GameEvent[] forbiddenEvents;

        [SerializeField] public EventAction[] actions;

        public string EventName => name;

        public EventAction[] GetActions()
        {
            return actions;
        }


        public virtual bool CanBeRunCustom()
        {
            return true;
        }
    }
}