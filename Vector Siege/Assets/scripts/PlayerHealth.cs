using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    private int startingHealth = 20; // Initial health
    [SerializeField] private Text healthText;         // UI Text to display health
    [SerializeField] private int currentHealth;
    private ScreenShake screenShake; // Reference to the ScreenShake component

    public int StartingHealth
    {
        get { return startingHealth; }
    }

    public int CurrentHealth
    {
        set
        {
            currentHealth = value;
            UpdateHealthUI();
        }
    }

    void Start()
    {
        currentHealth = startingHealth;
        UpdateHealthUI();
        screenShake = Camera.main.GetComponent<ScreenShake>(); // Get the ScreenShake component from the main camera
    }

    private void Update()
    {

    }

    // Method to reduce health
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        UpdateHealthUI();

        if (screenShake != null)
        {
            StartCoroutine(screenShake.Shake(0.2f, 0.1f)); // Call the Shake method with duration and magnitude
        }

        if (currentHealth <= 0)
        {
            // Handle player death (e.g., end game)
            Debug.Log("WASTED");
            LoadGameOverScene();
        }
    }

    // Method to update the health UI
    void UpdateHealthUI()
    {
        healthText.text = "Health: " + currentHealth;
    }

    // Method to load the game over scene
    void LoadGameOverScene()
    {
        SceneManager.LoadScene(2); // Load scene with index 2
    }
}
