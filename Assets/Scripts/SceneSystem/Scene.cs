using System;
using System.Collections;
using System.Collections.Generic;
using Inventory;
using UnityEngine;

public class Scene : MonoBehaviour
{
    public bool showInventory;
    
    public void SetActive(bool value)
    {
        gameObject.SetActive(value);
        // InventoryManager.Instance.SetActive(showInventory);
    }
}