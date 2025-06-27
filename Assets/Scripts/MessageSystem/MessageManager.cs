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

        public IEnumerator SayMessage(string message, string name = null)
        {
            yield return ui.ShowMessage(message, name);
            yield return new WaitUntil(() => !Input.GetKey(KeyCode.Mouse0));
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Mouse0));
            yield return ui.HideMessage();
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
        
        public IEnumerator ShowMessages(string[] messages, string[] names)
        {
            int i = 0;
            string message;
            string name;
            while (i < messages.Length)
            {
                message = messages[i];
                name = names[i];
                
                yield return ui.ShowMessage(message, name);
                yield return new WaitUntil(() => !Input.GetKey(KeyCode.Mouse0));
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Mouse0));
                yield return ui.HideMessage();
                i++;
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