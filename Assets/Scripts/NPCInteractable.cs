using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteractable : MonoBehaviour
{
    public GameObject interactText;
    public GameObject chatText;
    

    void Update()
    {
        hideInteractText();
    }

    public void withinRange()
    {
        if (interactText != null)
        {
            interactText.SetActive(true);
            //Debug.Log("Interact!");
        }
    }

    public void hideInteractText()
    {
        interactText.SetActive(false);
    }

    public void interact()
    {
        chatText.SetActive(true);
        interactText.SetActive(false);
        Debug.Log("Interact!");
    }
}

