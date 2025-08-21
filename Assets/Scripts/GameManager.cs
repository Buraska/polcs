using DigitalRuby.SoundManagerNamespace.MySoundManager;
using Inventory;
using JetBrains.Annotations;
using MessageSystem;
using SceneSystem;
using UnityEngine;

[RequireComponent(typeof(UIBlocker))]
[RequireComponent(typeof(SceneTransitionManager))]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] public EventManager EventManager;

    public UIBlocker UIBlocker { get; private set; }
    public SceneTransitionManager SceneTransitionManager { get; private set; }

    [SerializeField] [CanBeNull] public InventoryManager InventoryManager;

    [SerializeField] public MessageManager MessageManager;

    public GameStateManager GameStateManager { get; private set; }

    public AudioManager AudioManager;


    public void Awake()
    {
        Instance = this;
        GameStateManager = GetComponent<GameStateManager>();
        UIBlocker = GetComponent<UIBlocker>();
        SceneTransitionManager = GetComponent<SceneTransitionManager>();
    }
}

