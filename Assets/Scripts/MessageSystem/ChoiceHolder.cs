using System;
using System.Collections;
using MessageSystem.ScriptElement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MessageSystem
{
    public class ChoiceHolder : MonoBehaviour
    {
        public AudioSource buttonClickSound;
        
        [SerializeField] private Button choiceButtonPrefab;
        public bool isWaitingForChoice { get; private set; }

        private Button CreateChoiceButton(Choice choice)
        {
            var choiceButton = Instantiate(choiceButtonPrefab);
            var buttonText = choiceButton.GetComponentInChildren<TextMeshProUGUI>();
            choiceButton.transform.SetParent(transform, false);
            buttonText.text = choice.choiceText;
            choiceButton.onClick.AddListener(() => OnClickChoiceButton(choice));
            return choiceButton;
        }
        
        private void OnClickChoiceButton(Choice choice)
        {
            Debug.Log("Choice button is pressed");
            buttonClickSound.Play();
            GameManager.Instance.MessageManager.StartCoroutine(GameManager.Instance.MessageManager.DisplayScript((choice.script)));
            isWaitingForChoice = false;
            RefreshChoiceView();
        }
        
        private void RefreshChoiceView()
        {
            foreach (var button in GetComponentsInChildren<Button>())
                Destroy(button.gameObject);
        }
        
        public void DisplayChoices(Choice[] choices)
        {
            isWaitingForChoice = true;
            foreach (var choice in choices)
            {
                var button = CreateChoiceButton(choice);
            }
        }


    }
}