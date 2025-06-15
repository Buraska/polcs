using System.Collections;
using TMPro;
using UnityEngine;

public class MessageSystem : MonoBehaviour 
{
	public static MessageSystem Instance;
		
	[SerializeField] private GameObject speechPanel;
	[SerializeField] private TextMeshProUGUI speechText;
		
	private bool IsSpeaking {get{return _speaking != null;}}

	private string _targetSpeech = "";
	private Coroutine _speaking = null;
	private bool _isSpeaking;
		
		
	void Awake()
	{
		Instance = this;
	}
	
	public void SayAdd(string speech)
	{
		StopSpeaking();

		speechText.text = _targetSpeech;

		_speaking = StartCoroutine(Speaking(speech));
	}
	

	public void StopSpeaking()
	{
		_isSpeaking = false;
	}

	public IEnumerator SayMessage(string[] messages)
	{
		_isSpeaking = true;
		GameManager.Instance.BlockInterface();
		speechPanel.SetActive(true);

			
		foreach (var s in messages)
		{
			speechText.text = s;
			do
			{
				if (!_isSpeaking)
				{
					GameManager.Instance.UnblockInterface();
					speechPanel.SetActive(false);
					yield break;
				}

				yield return null;
			}
			while (!Input.GetKeyUp(KeyCode.Mouse0));
		}
			
		GameManager.Instance.UnblockInterface();
		_isSpeaking = false;
		speechPanel.SetActive(false);
	}
		
	private IEnumerator Speaking(string speech, bool additive = false)
	{
		_targetSpeech = speech;

		// if (!additive)
		// 	speechText.text = "";
		// else
		// 	_targetSpeech = speechText.text + _targetSpeech;
		
		// while(speechText.text != _targetSpeech)
		// {
		// 	speechText.text += _targetSpeech[speechText.text.Length];
		// 	yield return new WaitForEndOfFrame();
		// }
		yield break;


	}

	
}