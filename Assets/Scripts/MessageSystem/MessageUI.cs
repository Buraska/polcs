using TMPro;
using UnityEngine;

namespace MessageSystem
{
    public class MessageUI : MonoBehaviour
    {
        [SerializeField] private GameObject speechPanel;
        [SerializeField] private TextMeshProUGUI speechText;

        public void Show(string message)
        {
            speechPanel.SetActive(true);
            speechText.text = message;
        }

        public void Hide()
        {
            speechPanel.SetActive(false);
        }
    }
}