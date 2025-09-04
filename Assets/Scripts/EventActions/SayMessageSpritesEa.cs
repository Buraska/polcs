using System;
using System.Collections;
using System.Linq;
using MessageSystem.ScriptElement;
using UnityEngine;

namespace EventActions
{
    public class SayMessageSpritesEa : EventAction
    {
        [SerializeField] private DialogScript script;
        [SerializeField] private OmgTalkingSprite[] sprites;

        public override IEnumerator ActionCoroutine()
        {
            yield return GameManager.Instance.MessageManager.DisplayScript(script, sprites);
        }
    }

    [Serializable]
    public class Speech
    {
        public OmgTalkingSprite Sprite;
        public string Message;
    }
}