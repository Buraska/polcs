using System.Collections;
using System.Linq;
using MessageSystem;
using MessageSystem.ScriptElement;
using UnityEngine;
using UnityEngine.Serialization;

namespace EventActions
{
    public class SayMessageSpritesEa : EventAction
    {
        [SerializeField] private DialogScript script;
        [SerializeField] private OmgTalkingSprite[] sprites;
        public override IEnumerator ActionCoroutine()
        {
            int i = 0;
            while (i < script.scriptUnit.Length)
            {
                var message = script.scriptUnit[i].Message;
                var character = script.scriptUnit[i].CharacterScript;

                var sprite = sprites.FirstOrDefault(sprite => sprite.name == character.name);
                if (sprite == null)
                {
                    Debug.Log($"Cannot find sprite with name: {character.name} in list of {sprites.Select(x => x.name).ToArray()}");
                    yield return (GameManager.Instance.MessageManager.DisplayMessage(message, character.name));
                }
                else
                {
                    sprite.SpriteSpeaks();
                    yield return (GameManager.Instance.MessageManager.DisplayMessage(message, character.name));
                    sprite.SpriteListens();    
                }
                i++;
            }

        }
    }
    [System.Serializable] 
    public class Speech
    {
        public OmgTalkingSprite Sprite;
        public string Message;
    }
    
}