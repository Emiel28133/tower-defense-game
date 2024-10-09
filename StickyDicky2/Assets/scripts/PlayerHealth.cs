using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Add this namespace

public class PlayerHealth : MonoBehaviour
{
    private int startingHealth = 20; // Initial health
    [SerializeField] private Text healthText;         // UI Text to display health

    private int currentHealth;

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
    }

    private void Update()
    {
        if (currentHealth <= 0)
        {
            // Handle player death (e.g., end game)
            Debug.Log("WASTED");
            LoadGameOverScene();
        }
    }
    // Method to reduce health
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        UpdateHealthUI();

        
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