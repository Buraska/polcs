using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using MessageSystem.ScriptElement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace MessageSystem
{
    public class MessageManager : MonoBehaviour
    {
        [SerializeField] private MessageUI ui;
        [SerializeField] private Button choiceButtonPrefab;
        [SerializeField] private ChoiceHolder choiceHolder;

        private Stack<DialogScript> runningScripts = new Stack<DialogScript>();
        private Coroutine _chosenScript;
        private bool isWaitingForChoice;


        private IEnumerator DisplayChoices(Choice[] choices)
        {
            choiceHolder.DisplayChoices(choices);

            yield return new WaitUntil(() => !choiceHolder.isWaitingForChoice);
        }

        public void SetMessageUi(MessageUI messageUI)
        {
            Debug.Log($"MessageUi set on {messageUI}");
            ui = messageUI;
        }

        private IEnumerator DisplayMessage(string message, string name = null)
        {
            yield return ui.ShowMessage(message, name);
            yield return new WaitUntil(() =>
                !Input.GetKey(KeyCode.Mouse0) || !Input.GetKey(KeyCode.Space) || !Input.GetKey(KeyCode.RightArrow));
            yield return new WaitUntil(() =>
                Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Space) ||
                Input.GetKeyDown(KeyCode.RightArrow));
            Debug.Log($"Stops DisplayMessage({message})");
            yield return ui.HideMessage();
        }


        public IEnumerator DisplayScript(DialogScript script, [CanBeNull] OmgTalkingSprite[] sprites = null)
        {
            MyUtils.Log($"Start Displaying script: {script.scriptUnit[0].Message}...");
            runningScripts.Push(script);

            try
            {
                foreach (var unit in script.scriptUnit)
                {
                    yield return HandleScriptUnit(unit, sprites);
                    yield return new WaitUntil(() => runningScripts.Peek() == script);
                }
            }
            finally
            {
                if (runningScripts.TryPop(out DialogScript lastScript) && lastScript != script)
                    Debug.LogError("DisplayScript Error. The deleted dialogScript is not the same that was started.");
        
                ui.HidePanel();
                MyUtils.Log($"Stop Displaying script: {script.scriptUnit[0].Message}...");
            }
        }
        public IEnumerator DisplayScript(SayMessageNameObj[] scriptObjects, OmgTalkingSprite[] sprites = null)
        {
            var dialogScript = ScriptableObject.CreateInstance<DialogScript>();
            dialogScript.scriptUnit = scriptObjects;
            yield return DisplayScript(dialogScript, sprites);
        }


        private IEnumerator HandleScriptUnit(SayMessageNameObj unit, OmgTalkingSprite[] sprites)
        {
            if (sprites == null)
            {
                yield return DisplayScriptUnit(unit);
                yield break;
            }

            var character = unit.CharacterScript;
            var sprite = sprites.FirstOrDefault(s => s.name == character?.name);

            if (sprite == null)
            {
                Debug.Log($"Cannot find sprite with name: {character?.name}");
                yield return DisplayScriptUnit(unit);
            }
            else
            {
                sprite.SpriteSpeaks();
                yield return DisplayScriptUnit(unit);
                sprite.SpriteListens();
            }
        }


        private IEnumerator DisplayScriptUnit(SayMessageNameObj scriptUnit)
        {
            Debug.Log($"Displaying {scriptUnit.Message}");
            if (!string.IsNullOrWhiteSpace(scriptUnit.eventTriggerId))
                GameManager.Instance.EventManager.InvokeFromStorage(scriptUnit.eventTriggerId);

            if (scriptUnit.choices is { Length: > 0 })
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
        }
    }
}