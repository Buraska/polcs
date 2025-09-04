using System;
using EventActions;
using JetBrains.Annotations;
using UnityEngine;

public class Scene : MonoBehaviour
{
    public bool showInventory;
    [CanBeNull] public PlayAmbient PlayAmbientEA;
    
    void Start()
    {
        // FitScreen();
    }
    
    private void OnEnable()
    {
        if (PlayAmbientEA != null)
        {
            Debug.Log($"Start playing ambient of scene {gameObject.name}");
            GameManager.Instance.StartCoroutine(GameManager.Instance.EventManager.RunAction(PlayAmbientEA.ActionCoroutine()));
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
        gameObject.SetActive(value);
    }
}