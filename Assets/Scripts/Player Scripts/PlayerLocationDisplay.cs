using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerLocationDisplay : MonoBehaviour
{
    public Transform playerTransform; // Reference to the player's Transform component
    public TextMeshProUGUI locationText; // Reference to a UI Text element where the location will be displayed
    public TextMeshProUGUI rotationText; // Reference to another TextMeshPro Text element for displaying rotation

    private void Update()
    {
        if (playerTransform != null && locationText != null)
        {
            // Get the player's position
            Vector3 playerPosition = playerTransform.position;

            // Extract the X and Y components of the player's position
            float xLocation = playerPosition.x;
            float yLocation = playerPosition.y;

            // Display the X and Y location on the UI Text element
            locationText.text = $"X: {xLocation:F2}\nY: {yLocation:F2}";



            // Get the player's rotation
            Quaternion playerRotation = playerTransform.rotation;

            // Extract the Y rotation component (in degrees)
            float yRotation = playerRotation.eulerAngles.y;

            // Display the Y rotation on the second TextMeshPro Text element
            rotationText.text = $"R: {yRotation:F2}°";
        }
        else
        {
            Debug.LogWarning("Player or locationText references not set in the inspector.");
        }
    }
}
