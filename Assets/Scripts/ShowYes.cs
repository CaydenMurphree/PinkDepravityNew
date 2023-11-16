using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowYes : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public Item item;
    public Button yes;

    // Update is called once per frame
    void Update()
    {
        if (inventoryManager.findItem(item))
        {
            yes.interactable = true;
        }
        else
        {
            yes.interactable = false;
        }
    }
}
