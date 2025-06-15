using System.Collections;
using System.Linq;
using EventActions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace GameEvent
{
    public class BaseGE : MonoBehaviour
    {
        [SerializeField] protected BaseGE[] requiredEvents;
        
        [SerializeField] protected  BaseGE[] forbiddenEvents;

        [SerializeField] protected BaseEA[] actions;

        public BaseEA[] GetActions()
        {
            return actions;
        }

        public string GetName()
        {
            return gameObject.name;
        }
        public bool IsActive()
        {

            if (!IsActiveCustom())
            {
                return false;
            }
            if (requiredEvents.Length != 0 && !GameManager.Instance.EventsExist(requiredEvents))
            {
                return false;
            }

            if (forbiddenEvents.Length != 0 && GameManager.Instance.EventsExist(forbiddenEvents))
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