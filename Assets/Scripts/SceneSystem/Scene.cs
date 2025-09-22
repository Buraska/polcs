using System;
using DigitalRuby.SoundManagerNamespace;
using EventActions;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

public class Scene : MonoBehaviour
{
    public bool showInventory;
    [CanBeNull] public PlayAmbient PlayAmbientEA;
    [CanBeNull] public PlayMusic PlayMusicEA;
    
    void Start()
    {
        // FitScreen();
    }
    
    private void OnEnable()
    {
        MyUtils.Log($"Scene {gameObject.name} enabled");
        SceneManager.sceneLoaded += OnSceneScriptLoaded;
    }

    private void OnSceneScriptLoaded(UnityEngine.SceneManagement.Scene s, UnityEngine.SceneManagement.LoadSceneMode m)
    { 
        PrepareScene();
    }

    private void PrepareScene()
    {
        if (PlayAmbientEA != null)
        {
            GameManager.Instance.StartCoroutine(GameManager.Instance.EventManager.RunAction(PlayAmbientEA.ActionCoroutine()));
        }
        if (PlayMusicEA != null)
        {
            GameManager.Instance.StartCoroutine(GameManager.Instance.EventManager.RunAction(PlayMusicEA.ActionCoroutine()));
        }
        if (GameManager.Instance.InventoryManager != null)
        {
            GameManager.Instance.InventoryManager.SetActive(showInventory);
        }
    }
    
    void FitScreen()
    {
        float worldScreenHeight = Camera.main.orthographicSize * 2f;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        // transform.localScale = new Vector3(
        //     worldScreenWidth / this.transform.bounds.size.x,
        //     worldScreenHeight / sr.bounds.size.y, 
        //     1);
        var scaleMultiplier = Camera.main.aspect / (16f / 9f);
        transform.localScale *= scaleMultiplier;
    }

    public void SetActive(bool value)
    {
        if (value)
        {
            PrepareScene();
        }
        gameObject.SetActive(value);
        
    }
}