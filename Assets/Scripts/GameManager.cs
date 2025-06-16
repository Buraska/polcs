using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EventTrigger;
using GameEvent;
using Inventory;
using MessageSystem;
using SceneSystem;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

[RequireComponent(typeof(MessageManager))]
[RequireComponent(typeof(InventoryUI))]
[RequireComponent(typeof(UIBlocker))]
[RequireComponent(typeof(SceneTransitionManager))]
[RequireComponent(typeof(EventManager))]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    public UIBlocker UIBlocker { get; private set; }
    public EventManager EventManager { get; private set; }
    public SceneTransitionManager SceneTransitionManager { get; private set; }
    
    public InventoryManager InventoryManager { get; private set; }
    
    public MessageManager MessageManager { get; private set; }
    
    public GameStateManager GameStateManager { get; private set; }


    public void Awake()
    {
        Instance = this;
        GameStateManager = GetComponent<GameStateManager>();
        InventoryManager = GetComponent<InventoryManager>();
        UIBlocker = GetComponent<UIBlocker>();
        EventManager = GetComponent<EventManager>();
        MessageManager = GetComponent<MessageManager>();
        MessageManager.Init(UIBlocker);
        InventoryManager = GetComponent<InventoryManager>();
        SceneTransitionManager = GetComponent<SceneTransitionManager>();
        SceneTransitionManager.Init(UIBlocker);
    }
}

