using System.Collections;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using Utils;

namespace MessageSystem
{
    public class MessageUI : MonoBehaviour
    {
        [SerializeField] private GameObject speechPanel;
        [SerializeField] private TextMeshProUGUI speechText;
        [SerializeField] [CanBeNull] private TextMeshProUGUI speecher;
        [SerializeField] private int fadeSpeed = 16;


        public IEnumerator ShowMessage(string message, string name = null)
        {
            if (message == "")
            {
                HidePanel();
                yield break;
            }
            
            if (speecher != null)
            {
                if (name != null)
                    speecher.text = name;
                else
                {
                    speecher.text = "";
                    message = $"<i>{message}</i>";
                    
                }
            }
            
            speechText.text = message.Replace("\\n", "\n");
            ShowPanel();
            yield return CustomAnimation.Fade(speechText, false, fadeSpeed);
        }

        public IEnumerator HideMessage()
        {
            yield return CustomAnimation.Fade(speechText, true, fadeSpeed);
            if (speecher != null) speecher.text = "";
            speechText.text = "Nothing";
        }

        public void HidePanel()
        {
            MyUtils.Log("Hide UI Panel");
            speechPanel.SetActive(false);
        }

        public void ShowPanel()
        {
            MyUtils.Log("Hide UI Panel");
            speechPanel.SetActive(true);
        }
    }
}