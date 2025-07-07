using UnityEngine;

namespace MessageSystem.ScriptElement
{
    [CreateAssetMenu(fileName = "SayMessageScript", menuName = "Dialogs/SayMessageScript")]
    public class SayMessageScript : ScriptableObject
    {

        public string[] messages;

    }
}
