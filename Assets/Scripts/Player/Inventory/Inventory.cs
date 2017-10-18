using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Inventory {

    private List<ItemSlot> itemSlots = new List<ItemSlot>();
    private int _inventorySize;

    public int InventorySize {
        get { return _inventorySize; }
    }

    public Inventory(int inventorySize) {
        _inventorySize = inventorySize;
        for (int i = 0; i < inventorySize; i++) {
            itemSlots.Add(new ItemSlot());
        }
    }

    public Item GetItem(int slotId) {
        return itemSlots[slotId].item;
    }

    public Item GetItem(string itemId) {
        return Item.ItemList[itemSlots.Select(x => x.item.id).Where(x => x == itemId).First()];
    }

    //public List<Item> GetItems(string itemId) {
    //    return Item.ItemList[itemSlots.Select(x => x.item.id).Where(x => x == itemId).First()];
    //}

    public void AddItem(Item item, int slotId) {
        itemSlots[slotId].item = item;
    }

    public void AddItem(string itemId, int slotId) {
        itemSlots[slotId].item = Item.ItemList[itemId];
    }

    public bool SlotInRange(int slotToTest) {
        if (slotToTest < 0) return false;
        else if (slotToTest > _inventorySize - 1) return false;
        else return true;
    }
}
