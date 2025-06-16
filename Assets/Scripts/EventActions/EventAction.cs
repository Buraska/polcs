using System.Collections;
using UnityEngine;

namespace EventActions
{
    [CreateAssetMenu(fileName = "Event action", menuName = "Event action")]
    public abstract class EventAction : MonoBehaviour
    {
        public abstract IEnumerator ActionCoroutine();
    }
}