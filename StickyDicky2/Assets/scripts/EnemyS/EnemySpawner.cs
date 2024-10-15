using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;        // Prefab of the enemy to spawn
    [SerializeField] private Transform[] waypoints;         // Array of waypoints to pass to the enemy
    [SerializeField] private float spawnWaitTime = 0.5f;    // Time between each enemy spawn
    [SerializeField] private float timeBetweenWaves = 10f;  // Time between waves
    [SerializeField] private int minEnemiesPerWave = 10;    // Minimum number of enemies per wave
    [SerializeField] private int maxEnemiesPerWave = 20;    // Maximum number of enemies per wave
    [SerializeField] private float speedIncrement = 0.5f;   // Speed increment after each wave

    private int enemiesInThisWave;
    private bool waveInProgress = false;  // To track if a wave is currently in progress
    private float currentEnemySpeed = 3f; // Initial speed of the enemies

    void Start()
    {
        // Start the first wave
        StartCoroutine(SpawnWaves());
    }

    private void Update()
    {
        enemiesInThisWave = Random.Range(minEnemiesPerWave, maxEnemiesPerWave);
    }

    IEnumerator SpawnWaves()
    {
        while (true)  // Infinite loop for continuous waves
        {
            // Wait for 10 seconds between waves
            if (waveInProgress == false)
            {
                yield return new WaitForSeconds(timeBetweenWaves);
            }

            // Begin spawning enemies
            waveInProgress = true;

            for (int i = 0; i < enemiesInThisWave; i++)
            {
                // Spawn the enemy
                GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);

                // Pass waypoints to the enemy (this assumes the enemy has a script that uses waypoints)
                EnemyMovement enemyMovement = enemy.GetComponent<EnemyMovement>();
                if (enemyMovement != null)
                {
                    enemyMovement.Waypoints = waypoints; // Assign waypoints to the enemy
                    enemyMovement.Speed = (int)currentEnemySpeed; // Set the current speed
                }

                // Wait for the next enemy to spawn
                yield return new WaitForSeconds(spawnWaitTime);
            }

            // Wait until all enemies are destroyed before starting a new wave
            yield return new WaitUntil(AllEnemiesDestroyed);

            // Mark wave as finished
            waveInProgress = false;

            // Increase the speed for the next wave
            currentEnemySpeed += speedIncrement;

            // Update the speed of all active enemies
            UpdateEnemySpeeds(currentEnemySpeed);
        }
    }

    bool AllEnemiesDestroyed()
    {
        // Check if there are no more enemies in the scene
        return GameObject.FindGameObjectsWithTag("Enemy").Length == 0;
    }

    void UpdateEnemySpeeds(float newSpeed)
    {
        // Find all enemies in the scene and update their speed
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            EnemyMovement enemyMovement = enemy.GetComponent<EnemyMovement>();
            if (enemyMovement != null)
            {
                enemyMovement.Speed = (int)newSpeed;
            }
        }
    }
}
