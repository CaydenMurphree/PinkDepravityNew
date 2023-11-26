using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading;

public class Player : MonoBehaviour
{
    public static Player Instance;

    public int maxHealth = 100;
    public int currentHealth;

    public int maxSanity = 100;
    public int currentSanity;

    // Reference to the Health Bar UI element
    public HealthBar healthBar;

    public HealthBar sanityBar;

    // Reference to the game over text UI element
    public GameObject deathDisplay;

    // Reference to the invnetory UI element
    public GameObject inventory;

    public InventoryManager inventoryManager;

    // Add a reference to the PlayerMovement script
    public PlayerMovement playerMovement;

    // Add a reference to Dialogue Object
    public GameObject dialogueBox;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        currentSanity = maxSanity;
        sanityBar.SetMaxHealth(maxSanity);
        StartCoroutine(TimerCoroutine());
    }

    void Update()
    {

        if (currentSanity > 100)
        {
            currentSanity = 100;
        }

        sanityBar.SetHealth(currentSanity);

        // Dialogue
        if (dialogueBox.activeSelf)
        {
            // Disable movement by calling the SetCanMove method in PlayerMovement
            //playerMovement.SetCanMove(false);

            // Show cursor
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        // Opening/Closing Inventory
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            // Toggle inventory's active state
            bool inventoryActive = !inventory.activeSelf;
            inventory.gameObject.SetActive(inventoryActive);

            // Toggle cursor visibility and lock state based on the inventory's active state
            Cursor.visible = inventoryActive;
            Cursor.lockState = inventoryActive ? CursorLockMode.None : CursorLockMode.Locked;

            // Call the ListItems method from the InventoryManager
            if (inventoryManager != null)
            {
                inventoryManager.ListItems();
            }
        }


        // Check if currentHealth is greater than maxHealth
        if (currentHealth > maxHealth)
        {
            // If it is, set currentHealth to maxHealth
            currentHealth = maxHealth;
        }

        // Check if health is zero or below, and disable movement if necessary
        if (currentHealth <= 0 || currentSanity <= 0)
        {
            // Disable movement by calling the SetCanMove method in PlayerMovement
            playerMovement.SetCanMove(false);

            // Show the game over text
            deathDisplay.gameObject.SetActive(true);

            //show cursor
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
        currentSanity -= 10;
    }

    public void TakeHealth(int health)
    {
        currentHealth += health;

        healthBar.SetHealth(currentHealth);
        currentSanity += 30;
    }

    private IEnumerator TimerCoroutine()
    {
        Debug.Log("Timer started");
        yield return new WaitForSeconds(20);
        TimerCallback();
    }

    private void TimerCallback()
    {
        Debug.Log("Timer ended");
        // Code to execute when timer elapses
        currentSanity -= 10;
        Debug.Log("Current sanity: " + currentSanity);

        StartCoroutine(TimerCoroutine());
    }
}
