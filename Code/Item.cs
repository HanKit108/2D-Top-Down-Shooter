using System;
using UnityEngine;

[Serializable]
public class Item {
    [SerializeField, Range(1, 100)] private int amount;
    public int Amount
    {
        get {
            return amount;
            }
        set {
            if(value >= 0) {
                amount = value;
                }
            }
    }

    public ItemSO ItemSO;

    public Sprite GetSprite() {
        return ItemSO.Sprite;
    }

    public string GetName() {
        return ItemSO.Name;
    }

    public bool IsStackable() {
        return ItemSO.IsStackable;
    }
}
