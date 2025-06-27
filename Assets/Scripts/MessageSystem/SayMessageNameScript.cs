using System.Collections;
using System.Linq;
using MessageSystem;
using UnityEngine;

namespace MessageSystem
{
    [CreateAssetMenu(fileName = "SayMessageNameScript", menuName = "Dialogs/SayMessageNameScript")]
    public class SayMessageNameScript : ScriptableObject
    {

        public SayMessageNameObj[] messages;

    }
    
    [System.Serializable]
    public class SayMessageNameObj
    {
        public CharacterScript CharacterScript;
        public string Message;
    }
}
