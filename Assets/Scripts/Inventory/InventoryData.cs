namespace Inventory
{
    public class InventoryData
    {
        private readonly ItemModel[] items;

        public InventoryData(int size)
        {
            items = new ItemModel[size];
        }

        public ItemModel[] Items => items;

        public int GetFreeSlotIndex()
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i] == null) return i;
            }
            return -1;
        }

        public int GetItemIndex(ItemModel item)
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i]?.id == item.id) return i;
            }
            return -1;
        }

        public void AddItem(ItemModel item, int index)
        {
            items[index] = item;
        }

        public void RemoveItem(int index)
        {
            items[index] = null;
        }

        public ItemModel GetItem(int index) => items[index];
    }
}