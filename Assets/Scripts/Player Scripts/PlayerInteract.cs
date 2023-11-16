using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private bool interact = false;

    private void Update()
    {
        float interactRange = 2f;
        Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);

        foreach (Collider collider in colliderArray)
        {
            if (collider.TryGetComponent(out NPCInteractable npcInteractable))
            {
                if (interact == false)
                {
                    npcInteractable.withinRange();
                }

                if (Input.GetKeyDown(KeyCode.E))
                {
                    interact = true;
                    npcInteractable.hideInteractText();
                    npcInteractable.interact();
                }
            }
            else
            {
                interact = false;
            }
        }
    }
}

