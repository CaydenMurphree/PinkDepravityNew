using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item Item;

    void Pickup()
    {
        InventoryManager.Instance.Add(Item);
        Destroy(gameObject);
    }

    private void OnMouseDown()
    {
        Pickup();
    }

    private void OnTriggerEnter(Collider other)
    {
        // if name of objrct is bullet bullet

        if (other.gameObject.name == "bullet")
        {
            Pickup();
        }
    }
}
