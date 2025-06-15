using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new item", menuName = "create new item")]
public class ItemModel : ScriptableObject
{
    // Start is called before the first frame update
    public string id;

    public Sprite sprite;

}
