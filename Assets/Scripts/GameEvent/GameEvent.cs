using System.Collections;
using System.Linq;
using EventActions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace GameEvent
{
    
    [CreateAssetMenu(fileName = "Game event", menuName = "Game event")]
    public class GameEvent : MonoBehaviour
    {
        [SerializeField] protected GameEvent[] requiredEvents;
        
        [SerializeField] protected  GameEvent[] forbiddenEvents;

        [SerializeField] protected EventAction[] actions;
        
        public string EventName => name;

        public EventAction[] GetActions()
        {
            return actions;
        }
        
        public bool IsActive()
        {
            if (!IsActiveCustom())
            {
                return false;
            }
            if (requiredEvents.Length != 0 && !GameManager.Instance.EventManager.EventsExist(requiredEvents))
            {
                return false;
            }

            if (forbiddenEvents.Length != 0 && GameManager.Instance.EventManager.EventsExist(forbiddenEvents))
            {
                return false;
            }

            return true;
        }

        protected virtual bool IsActiveCustom()
        {
            return true;
        }


        public IEnumerator Act()
        {
            foreach (var action in actions)
            {
                yield return GameManager.Instance.StartCoroutine(action.ActionCoroutine());
            }
        
        }
    }
}