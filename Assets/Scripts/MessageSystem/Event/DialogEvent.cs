using UnityEngine;

namespace MessageSystem.Event
{
    public abstract class DialogEvent : ScriptableObject
    {
        public abstract void Trigger();
    }
}