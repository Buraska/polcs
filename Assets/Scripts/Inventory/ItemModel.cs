using UnityEngine;

namespace Inventory
{
    [CreateAssetMenu(fileName = "New item", menuName = "New item")]
    public class ItemModel : ScriptableObject
    {
        // Start is called before the first frame update
        public string id;

        public Sprite sprite;

    }
}
