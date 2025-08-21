using System;
using EventActions;
using JetBrains.Annotations;
using UnityEngine;

public class Scene : MonoBehaviour
{
    public bool showInventory;
    [CanBeNull] public PlayAmbient PlayAmbientEA;

    private void OnEnable()
    {
        if (PlayAmbientEA != null)
        {
            Debug.Log($"Start playing ambient of scene {gameObject.name}");
            GameManager.Instance.StartCoroutine(GameManager.Instance.EventManager.RunAction(PlayAmbientEA.ActionCoroutine()));
        }
        
        if (GameManager.Instance.InventoryManager != null)
            GameManager.Instance.InventoryManager.SetActive(showInventory);
    }

    public void SetActive(bool value)
    {
        gameObject.SetActive(value);


    }
}