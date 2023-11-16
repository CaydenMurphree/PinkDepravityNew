using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    // Add a reference to the PlayerMovement script
    public PlayerMovement playerMovement;

    // Add a reference to Dialogue Object
    public GameObject dialogueBox;

    // Update is called once per frame
    void Update()
    {
        if (dialogueBox.activeSelf)
        {
            // Disable movement by calling the SetCanMove method in PlayerMovement
            //playerMovement.SetCanMove(false);

            // Show cursor
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            //playerMovement.SetCanMove(true);

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

    }

}
