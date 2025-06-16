using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class UIBlocker : MonoBehaviour
{
    [SerializeField] private Image uiBlock;

    public bool IsBlocked { get; private set; }

    public void Block()
    {
        IsBlocked = true;
        uiBlock.enabled = IsBlocked;
    }

    public void Unblock()
    {
        IsBlocked = false;
        uiBlock.enabled = IsBlocked;
    }
}