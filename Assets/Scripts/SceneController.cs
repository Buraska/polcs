using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public bool showInventory;
    
    public void SetActive(bool value)
    {
        gameObject.SetActive(value);
        InventoryManager.Instance.SetActive(showInventory);
    }
}