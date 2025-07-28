using System.Collections;
using UnityEngine;

public enum GameState
{
    Exploring,
    UsingItem,
    Dialogue
}

public class GameStateManager : MonoBehaviour
{
    public GameState GameState { get; private set; }

    public Coroutine SetUsingItem()
    {
        return StartCoroutine(SetUsingItemCoroutine());
    }

    private IEnumerator SetUsingItemCoroutine()
    {
        GameState = GameState.UsingItem;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Mouse0));
        yield return new WaitUntil(() => Input.GetKeyUp(KeyCode.Mouse0));
        GameState = GameState.Exploring;
    }
}