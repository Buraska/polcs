using System;
using System.Collections;
using System.Collections.Generic;
using EventTrigger;
using GameEvent;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour
{
    public static SceneManager Instance;
    
    [SerializeField] private SceneController[] localScenes;
    [SerializeField] private int currentLocalScene = 0;
    [SerializeField] private Image sceneBlocker;
    
    public void Awake()
    {
        Instance = this;
        localScenes[currentLocalScene].SetActive(true);
        
    }
    
    public void ChangeSceneTo(int sceneNum, SceneController newScene)
    {
        localScenes[sceneNum] = newScene;
    }

    public IEnumerator DisableObjectCoroutine(GameObject obj, int fadeSpeed)
    {
        GameManager.Instance.BlockInterface();
        yield return StartCoroutine(Animation.FadeImage(obj.transform.GetComponent<SpriteRenderer>(), true, fadeSpeed));
        obj.SetActive(false);
        GameManager.Instance.UnblockInterface();
    }
    
    public IEnumerator EnableObjectCoroutine(GameObject obj, int fadeSpeed)
    {
        GameManager.Instance.BlockInterface();
        obj.SetActive(true);
        yield return StartCoroutine(Animation.FadeImage(obj.transform.GetComponent<SpriteRenderer>(), false, fadeSpeed));
        GameManager.Instance.UnblockInterface();
    }
    

    public IEnumerator GoToSceneCoroutine(int sceneNum)
    {
        GameManager.Instance.BlockInterface();

        yield return StartCoroutine(Animation.FadeImage(sceneBlocker, false));
        
        localScenes[currentLocalScene].SetActive(false);
        localScenes[sceneNum].SetActive(true);
        currentLocalScene = sceneNum;

        yield return StartCoroutine(Animation.FadeImage(sceneBlocker, true));
        GameManager.Instance.UnblockInterface();
    }



}

