using System.Collections;
using UnityEngine.Serialization;

namespace EventActions
{
    public class PlayAmbient : EventAction
    {

        public string audioName;

        public override IEnumerator ActionCoroutine()
        {
            GameManager.Instance.AudioManager.PlayAmbient(audioName);
            yield break;
        }
    }
}