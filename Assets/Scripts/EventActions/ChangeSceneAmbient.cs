using System.Collections;
using UnityEngine;

namespace EventActions
{
    public class ChangeSceneAmbient : EventAction
    {
        [SerializeField] private Scene scene;
        [SerializeField] private PlayAmbient ambientEa;

        public override IEnumerator ActionCoroutine()
        {
            scene.PlayAmbientEA = ambientEa;
            yield break;
        }
    }
}