using UnityEngine;

namespace EventTrigger
{
    public class Trigger : MonoBehaviour
    {
        [SerializeField] protected GameEvent.GameEvent[] gameEvents;
    }
}