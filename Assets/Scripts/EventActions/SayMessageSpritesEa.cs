using System.Collections;
using System.Linq;
using MessageSystem;
using UnityEngine;
using UnityEngine.Serialization;

namespace EventActions
{
    public class SayMessageSpritesEa : EventAction
    {
        [SerializeField] private SayMessageNameScript script;
        [SerializeField] private OmgTalkingSprite[] sprites;
        public override IEnumerator ActionCoroutine()
        {
            int i = 0;
            while (i < script.messages.Length)
            {
                var message = script.messages[i].Message;
                var character = script.messages[i].CharacterScript;

                var sprite = sprites.First(sprite => sprite.name == character.name);
                if (sprite == null)
                {
                    Debug.Log($"Cannot find sprite with name: {character.name} in list of {sprites.Select(x => x.name).ToArray()}");
                }
                else
                {
                    sprite.SpriteSpeaks();
                    yield return (GameManager.Instance.MessageManager.SayMessage(message, character.name));
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