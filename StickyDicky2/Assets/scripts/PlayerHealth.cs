using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Add this namespace

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 20; // Initial health
    public Text healthText;         // UI Text to display health

    private int currentHealth;

    void Start()
    {
        currentHealth = startingHealth;
        UpdateHealthUI();
    }

    private void Update()
    {
        
    }
    // Method to reduce health
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        UpdateHealthUI();

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