namespace Inventory
{
    public class InventoryData
    {
        public InventoryData(int size)
        {
            Items = new ItemModel[size];
        }

        public ItemModel[] Items { get; }

        public int GetFreeSlotIndex()
        {
            for (var i = 0; i < Items.Length; i++)
                if (Items[i] == null)
                    return i;
            return -1;
        }

        public int GetItemIndex(ItemModel item)
        {
            for (var i = 0; i < Items.Length; i++)
                if (Items[i]?.id == item.id)
                    return i;
            return -1;
        }

        public void AddItem(ItemModel item, int index)
        {
            Items[index] = item;
        }

        public void RemoveItem(int index)
        {
            Items[index] = null;
        }

        public ItemModel GetItem(int index)
        {
            return Items[index];
        }
    }
}