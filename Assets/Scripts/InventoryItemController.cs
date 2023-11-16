using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemController : MonoBehaviour
{
    Item item;

    public Button RemoveButton;


    public void RemoveItem()
    {
        InventoryManager.Instance.Remove(item);

        Destroy(gameObject);
    }

    public void AddItem(Item newItem)
    {
        item = newItem;
    }

    public void UseItem()
    {
        switch (item.itemType)
        {
            case ItemType.HealthPack:
                Player.Instance.TakeHealth(item.value);
                RemoveItem();
                break;
            case ItemType.Food:
                Player.Instance.TakeHealth(item.value);
                RemoveItem();
                break;
            case ItemType.Book:
                
                break;
            case ItemType.Weapon:
                RemoveItem();
                break;
            default:
                
                Debug.LogError("Unknown item type: " + item.itemType);
                break;
        }

        //RemoveItem();
    }

}
