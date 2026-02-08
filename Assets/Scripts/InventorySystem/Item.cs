using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[Serializable]
public class Item : IEquatable<Item>, ICloneable
{
    public string itemName;
    public Sprite sprite;
    public int stackSize;
    public int maxStackSize;
    
    public Item(Item item)
    {
        itemName = item.itemName;
        sprite = item.sprite;
        stackSize = item.stackSize;
        maxStackSize = item.maxStackSize;
    }
    
    public Item(string itemName, Sprite sprite, int stackSize, int maxStackSize)
    {
        this.itemName = itemName;
        this.sprite = sprite;
        this.stackSize = stackSize;
        this.maxStackSize = maxStackSize;
    }

    protected Item()
    {
        itemName = string.Empty;
        sprite = null;
        stackSize = 1;
        maxStackSize = 1;
    }

    public bool IsStackable()
    {
        return maxStackSize > 1;
    }

    public bool AddToStack(int amount = 1)
    {
        if (!IsStackable()) return false;

        if (stackSize + amount > maxStackSize) return false;

        stackSize += amount;
        return true;
    }
    
    public Item RemoveFromStack(int amount = 1)
    {
        if (amount >= stackSize) return this;

        stackSize -= amount;
        Item newItem = (Item)Clone();
        newItem.stackSize = amount;
        return newItem;
    }

    public bool Equals(Item other)
    {
        if (other == null) return false;
        return itemName == other.itemName;
    }

    public object Clone()
    {
        return new Item(this);
    }
}

[Serializable]
public class Weapon : Item
{
    public float damage;
    public float attackSpeed;
    public float attackSize;
}