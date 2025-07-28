using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace MessageSystem.ScriptElement
{
    [CreateAssetMenu(fileName = "SayMessageNameScript", menuName = "Dialogs/SayMessageNameScript")]
    public class DialogScript : ScriptableObject
    {
        [FormerlySerializedAs("messages")] public SayMessageNameObj[] scriptUnit;
    }

    [Serializable]
    public class SayMessageNameObj
    {
        public CharacterScript CharacterScript;
        public string Message;
        public Choice[] choices;
        public string eventTriggerId;
    }


    [Serializable]
    public class Choice
    {
        public string choiceText;
        [FormerlySerializedAs("nextScript")] public SayMessageNameObj[] script;
    }
}