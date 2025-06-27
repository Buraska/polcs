using UnityEngine;

namespace MessageSystem
{
    public class OmgTalkingSprite: MonoBehaviour
    {
        public string name;
        public SpriteRenderer talkingSprite;
        public SpriteRenderer listeningSprite;

        public void SpriteSpeaks()
        {
            talkingSprite.enabled = true;
            listeningSprite.enabled = false;
        }
        
        public void SpriteListens()
        {
            listeningSprite.enabled = true;
            talkingSprite.enabled = false;
        }
    }
}