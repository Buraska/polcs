using System.Collections;
using UnityEngine;

namespace MessageSystem
{
    public class MessageManager : MonoBehaviour
    {
        [SerializeField] private MessageUI ui;

        private UIBlocker blocker;

        public void Init(UIBlocker blocker)
        {
            this.blocker = blocker;
        }

        public IEnumerator ShowMessages(string[] messages)
        {
            foreach (var message in messages)
            {
                ui.Show(message);
                yield return new WaitUntil(() => !Input.GetKey(KeyCode.Mouse0));
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Mouse0));
            }
            ui.Hide();
        }

        public void ShowInstant(string message)
        {
            ui.Show(message);
        }

        public void HideInstant()
        {
            ui.Hide();
        }
    }
}