using UnityEngine;

namespace EventActions.utils
{
    [CreateAssetMenu(fileName = "TimeHolder", menuName = "Holders/TimeHolder")]
    public class TimeHolder : ScriptableObject
    {
        public float time;
    }
}