using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
    
    [SerializeField] private UI_Inventory uiInventory;
    [SerializeField] private List<Item> itemList;
    [SerializeField] private ItemSO bulletItem;

    public event EventHandler OnItemListChanged;

    private void Awake() {
        uiInventory.SetInventory(this);
    }

    public List<Item> GetItemList() {
        return itemList;
    }

    public void AddItem(Item item) {
        if(item.IsStackable()) {
            bool itemAlreadyInInventory = false;
            foreach(Item inventoryItem in itemList) {
                if(inventoryItem.GetName() == item.GetName()) {
                    inventoryItem.Amount += item.Amount;
                    itemAlreadyInInventory = true;
                }
            }
            if(!itemAlreadyInInventory) {
                itemList.Add(item);
            }
        } else {
            itemList.Add(item);
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public void RemoveItem(Item item) {
        itemList.Remove(item);
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public float GetNumberBullets() {
        foreach(Item item in itemList) {
            if(item.GetName() == bulletItem.Name) {
                return item.Amount;
            }
        }
        return 0f;
    }

    public void ClearInventory() {
        itemList.Clear();
    }

    public bool SpendBullet() {
        foreach(Item item in itemList) {
            if(item.GetName() == bulletItem.Name ) {
                if(item.Amount > 0) {
                    item.Amount--;
                    OnItemListChanged?.Invoke(this, EventArgs.Empty);
                    return true;
                } else {
                    //itemList.Remove(item);
                }
            }
        }
        return false;
    }
}
