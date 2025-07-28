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
            foreach (var scriptUnit in script.scriptUnit)
            {
                var character = scriptUnit.CharacterScript;

                var sprite = sprites.FirstOrDefault(sprite => sprite.name == character?.name);
                if (sprite == null)
                {
                    Debug.Log(
                        $"Cannot find sprite with name: {character?.name} in list of {sprites.Select(x => x.name).ToArray()}");
                    yield return GameManager.Instance.MessageManager.DisplayScriptUnit(scriptUnit);
                }
                else
                {
                    sprite.SpriteSpeaks();
                    yield return GameManager.Instance.MessageManager.DisplayScriptUnit(scriptUnit);
                    sprite.SpriteListens();
                }
            }
        }
    }

    [Serializable]
    public class Speech
    {
        public OmgTalkingSprite Sprite;
        public string Message;
    }
}