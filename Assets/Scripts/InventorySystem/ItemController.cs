using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    [SerializeField] private Item item;

    void Interact(GameObject source)
    {
        Inventory inventory = source.GetComponent<Inventory>();

        inventory.AddItem(item);
        
        Destroy(gameObject);
    }
}
