using System.Collections;
using UnityEngine;

namespace MessageSystem
{
    public class MessageManager : MonoBehaviour
    {
        [SerializeField] private MessageUI ui;


        public void SetMessageUi(MessageUI messageUI)
        {
            ui = messageUI;
        }
        
        public IEnumerator ShowMessages(string[] messages)
        {
            foreach (var message in messages)
            {
                yield return ui.ShowMessage(message);
                yield return new WaitUntil(() => !Input.GetKey(KeyCode.Mouse0));
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Mouse0));
                yield return ui.HideMessage();
            }
            ui.HidePanel();
        }

        public void ShowInstant(string message)
        {
            ui.ShowMessage(message);
        }

        public void HideInstant()
        {
            ui.HidePanel();
        }
    }
}