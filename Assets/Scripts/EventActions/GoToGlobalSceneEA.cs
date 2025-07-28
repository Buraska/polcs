using System.Collections;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace EventActions
{
    public class GoToGlobalSceneEA : EventAction
    {
        [SerializeField] private int changeSceneNum;

        public override IEnumerator ActionCoroutine()
        {
            yield return GameManager.Instance.SceneTransitionManager.StartTransition(GameConstants.GlobalSceneTransitionSpeed);
            SceneManager.LoadScene(changeSceneNum);
            yield return
                new WaitForSeconds(
                    1); // At the end of any event scene is unblocked. If you delete this line you will see splash. To solve this we need separate scene blocker for transitions and game events.  
        }
    }
}