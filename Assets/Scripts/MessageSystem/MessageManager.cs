using System.Collections;
using MessageSystem.ScriptElement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MessageSystem
{
    public class MessageManager : MonoBehaviour
    {
        [SerializeField] private MessageUI ui;
        [SerializeField] private GridLayoutGroup choiceHolder;
        [SerializeField] private Button choiceButtonPrefab;

        private Coroutine _chosenScript;
        private bool isWaitingForChoice;

        private Button CreateChoiceButton(Choice choice)
        {
            var choiceButton = Instantiate(choiceButtonPrefab);
            var buttonText = choiceButton.GetComponentInChildren<TextMeshProUGUI>();
            choiceButton.transform.SetParent(choiceHolder.transform, false);
            buttonText.text = choice.choiceText;
            choiceButton.onClick.AddListener(() => OnClickChoiceButton(choice));
            return choiceButton;
        }

        private void OnClickChoiceButton(Choice choice)
        {
            Debug.Log("Choice button is pressed");
            _chosenScript = StartCoroutine(DisplayScript(choice.script));
            RefreshChoiceView();
            isWaitingForChoice = false;
        }

        private void RefreshChoiceView()
        {
            if (choiceHolder != null)
                foreach (var button in choiceHolder.GetComponentsInChildren<Button>())
                    Destroy(button.gameObject);
        }


        private IEnumerator DisplayChoices(Choice[] choices)
        {
            isWaitingForChoice = true;
            if (choiceHolder.GetComponentsInChildren<Button>().Length > 0) yield break;
            foreach (var choice in choices)
            {
                var button = CreateChoiceButton(choice);
            }

            yield return new WaitUntil(() => !isWaitingForChoice);
            yield return _chosenScript;
        }

        public void SetMessageUi(MessageUI messageUI)
        {
            Debug.Log($"MessageUi set on {messageUI}");
            ui = messageUI;
        }

        private IEnumerator DisplayMessage(string message, string name = null)
        {
            yield return ui.ShowMessage(message, name);
            yield return new WaitUntil(() => !Input.GetKey(KeyCode.Mouse0) ||  !Input.GetKey(KeyCode.Space) ||  !Input.GetKey(KeyCode.RightArrow));
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.RightArrow));
            Debug.Log($"Stops DisplayMessage({message})");
            yield return ui.HideMessage();
        }
        

        public IEnumerator DisplayScript(DialogScript script)
        {
            yield return DisplayScript(script.scriptUnit);
        }

        public IEnumerator DisplayScript(SayMessageNameObj[] scriptObjects)
        {
            foreach (var scriptUnit in scriptObjects) yield return DisplayScriptUnit(scriptUnit);
            ui.HidePanel();
        }

        public IEnumerator DisplayScriptUnit(SayMessageNameObj scriptUnit)
        {
            Debug.Log($"Displaying {scriptUnit.Message}");
            if (!string.IsNullOrWhiteSpace(scriptUnit.eventTriggerId))
                GameManager.Instance.EventManager.InvokeFromStorage(scriptUnit.eventTriggerId);

            if (scriptUnit.choices.Length > 0)
            {
                ui.HidePanel();
                yield return DisplayChoices(scriptUnit.choices);
            }
            else if (scriptUnit.CharacterScript == null)
            {
                Debug.Log(scriptUnit.Message);
                yield return DisplayMessage(scriptUnit.Message);
            }
            else if (scriptUnit.CharacterScript != null)
            {
                Debug.Log($"Displaying {scriptUnit.Message}");
                yield return DisplayMessage(scriptUnit.Message, scriptUnit.CharacterScript.name);
            }

            ui.HidePanel();
        }
    }
}