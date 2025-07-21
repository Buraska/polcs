using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace SceneSystem
{
    [RequireComponent(typeof(Image))]
    public class UIBlocker : MonoBehaviour
    {
        [FormerlySerializedAs("uiBlock")] [SerializeField] private Image uiBlocker;

        public bool IsBlocked { get; private set; }

        public void Block()
        {
            Debug.Log("Block");
            IsBlocked = true;
            uiBlocker.enabled = IsBlocked;
        }

        public void Unblock()
        {
            Debug.Log("UnBlock");
            IsBlocked = false;
            uiBlocker.enabled = IsBlocked;
        }
    


    }
}