using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardBehavior : MonoBehaviour
{
    public int damageAmount = 10; // Amount of damage the hazard inflicts

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the colliding object has a "Player" tag (or use a layer check).
        if (collision.gameObject.CompareTag("Player"))
        {
            // Get the Player script and apply damage to the player.
            Player player = collision.gameObject.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(damageAmount);
                //Destroy(gameObject); // Destroy the hazard when the player touches it
            }
        }
    }
}

