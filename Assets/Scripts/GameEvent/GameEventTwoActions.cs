using EventActions;
using UnityEngine;

namespace GameEvent
{
    public class GameEventTwoActions : GameEvent
    {

        [SerializeField] public EventAction[] actionsOtherTimes;

        private bool hasRun = false;

        public string EventName => name;

        public override EventAction[] GetActions()
        {
            if (!hasRun)
            {
                hasRun = true;
                return actions;
            }
            return actionsOtherTimes;
        }

    }
}