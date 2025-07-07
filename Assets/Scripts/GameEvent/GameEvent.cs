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
        [SerializeField] public GameEvent[] requiredEvents;
        
        [SerializeField] public  GameEvent[] forbiddenEvents;

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


        public IEnumerator Act()
        {
            foreach (var action in actions)
            {
                yield return GameManager.Instance.StartCoroutine(action.ActionCoroutine());
            }
        
        }
    }
}