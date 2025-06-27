using System.Collections;
using System.Linq;
using MessageSystem;
using UnityEngine;

namespace MessageSystem
{
    [CreateAssetMenu(fileName = "SayMessageScript", menuName = "Dialogs/SayMessageScript")]
    public class SayMessageScript : ScriptableObject
    {

        public string[] messages;

    }
}
