using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<Item> items = new();
    [SerializeField] private Vector2Int dimensions;
    
    private int maxSize;
    
    // Start is called before the first frame update
    void Start()
    {
        // Get max size from the dimensions of the inventory
        maxSize = dimensions.x * dimensions.y;
    }
    
    /// <summary>
    /// Get an item based on its index in the inventory's underlying list.
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public Item GetItem(int index)
    {
        if (index < 0 || index >= items.Count) return null;
        
        return items[index];
    }

    /// <summary>
    /// Gets an item from the inventory based on its position in the inventory grid
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public Item GetItem(Vector2Int position)
    {
        if (position.x < 0 || position.x >= dimensions.x || position.y < 0 || position.y >= dimensions.y) return null;
        
        return items[position.x + position.y * dimensions.x];
    }

    /// <summary>
    /// Gets an item from the inventory by name
    /// </summary>
    /// <param name="itemName"></param>
    /// <returns></returns>
    public Item GetItem(string itemName)
    {
        return items.Find(i => i.itemName == itemName);
    }

    public Item RemoveItem(int index, int count = int.MaxValue)
    {
        Item item = GetItem(index);
        return RemoveItem(item, count);
    }

    private Item RemoveItem(Item item, int count)
    {
        if (item == null) return null;

        if (count >= item.stackSize)
        {
            items.Remove(item);
            return item;
        }
        else
        {
            return item.RemoveFromStack(count);
        }
    }

    public Item RemoveItem(Vector2Int position, int count = int.MaxValue)
    {
        Item item = GetItem(position);
        return RemoveItem(item, count);
    }

    public Item RemoveItem(string itemName, int count = int.MaxValue)
    {
        Item item = GetItem(itemName);
        return RemoveItem(item, count);
    }
    
    /// <summary>
    /// Tries to add an item to this inventory. It returns true if it was successfully added and false if it failed.
    /// </summary>
    /// <param name="item">Item to add</param>
    /// <returns>True on success, false on fail</returns>
    public bool AddItem(Item item)
    {
        // If an existing copy of the item is in the inventory, try to stack the item
        Item find = items.Find(i => i.itemName == item.itemName);
        if (find != null)
        {
            return find.AddToStack(item.stackSize);
        }
        else
        {
            if (items.Count >= maxSize) return false;
            
            items.Add(item);
            return true;
        }
    }
}
