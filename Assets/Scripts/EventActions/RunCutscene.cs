using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

namespace EventActions
{
    public class RunCutscene: EventAction
    {
        public PlayableDirector PlayableDirector;
        public override IEnumerator ActionCoroutine()
        {
            PlayableDirector.Play();
            yield return new WaitForSeconds((float)PlayableDirector.duration);
             yield break;
        }
    }
}