using System.Collections;

namespace GameEvent
{
    public class ByEquipmentGE : BaseGE
    {
        public ItemModel item;
        
        protected override bool IsActiveCustom()
        {

            if (InventoryManager.Instance.GetSelectedItem().id != item.id)
            {
                return false;
            }
            
            return true;
        }
    }
    
}