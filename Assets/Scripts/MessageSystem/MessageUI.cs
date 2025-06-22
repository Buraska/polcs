using System.Collections;
using TMPro;
using UnityEngine;

namespace MessageSystem
{
    public class MessageUI : MonoBehaviour
    {
        [SerializeField] private GameObject speechPanel;
        [SerializeField] private TextMeshProUGUI speechText;
        [SerializeField] private int fadeSpeed = 16;


        public IEnumerator ShowMessage(string message)
        {
            speechText.text = message.Replace("\\n", "\n");
            speechPanel.SetActive(true);
            yield return (CustomAnimation.Fade(speechText, false, fadeSpeed));
        }
        
        public IEnumerator HideMessage()
        {
            yield return CustomAnimation.Fade(speechText, true, fadeSpeed);
            speechText.text = "Nothing";
        }

        public void HidePanel()
        {
            speechPanel.SetActive(false);
        }
    }
}