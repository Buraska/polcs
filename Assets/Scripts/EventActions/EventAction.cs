using System.Collections;
using UnityEngine;

namespace EventActions
{
    public abstract class EventAction : MonoBehaviour
    {
        public abstract IEnumerator ActionCoroutine();
    }
}