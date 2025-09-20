using System.Collections;
using UnityEngine;
using UnityEngine.UI; // Add this to use the UI components

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;        // Prefab of the enemy to spawn
    [SerializeField] private Transform[] waypoints;         // Array of waypoints to pass to the enemy
    [SerializeField] private float spawnWaitTime = 0.5f;    // Time between each enemy spawn
    [SerializeField] private float timeBetweenWaves = 5f;  // Time between waves
    [SerializeField] private int minEnemiesPerWave = 10;    // Minimum number of enemies per wave
    [SerializeField] private int maxEnemiesPerWave = 20;    // Maximum number of enemies per wave
    [SerializeField] private float speedIncrement = 0.5f;   // Speed increment after each wave
    [SerializeField] private Text waveText;                 // UI Text component to display the wave number

    private int enemiesInThisWave;
    private bool waveInProgress = false;  // To track if a wave is currently in progress
    private float currentEnemySpeed = 3f; // Initial speed of the enemies
    [SerializeField] private WaveType currentWave = WaveType.Initial; // Current wave type

    private enum WaveType
    {
        Initial,
        Regular,
        Advanced
    }

    void Start()
    {
        // Start the first wave
        StartCoroutine(SpawnWaves());
    }

    private void Update()
    {
        enemiesInThisWave = Random.Range(minEnemiesPerWave, maxEnemiesPerWave);
    }

    //enumerator to spawn waves of enemies
    IEnumerator SpawnWaves()
    {
        while (true)  // Infinite loop for continuous waves
        {
            // Wait between waves
            if (waveInProgress == false)
            {
                switch (currentWave)
                {
                    case WaveType.Initial:
                        yield return new WaitForSeconds(10f);
                        break;
                    case WaveType.Regular:
                        yield return new WaitForSeconds(timeBetweenWaves);
                        break;
                    case WaveType.Advanced:
                        yield return new WaitForSeconds(2f);
                        break;
                }
            }

            // Begin spawning enemies
            waveInProgress = true;

            // Increment the wave number and update the UI text
            currentWave = GetNextWaveType(currentWave);
            waveText.text = "Wave: " + currentWave;

            // Adjust the number of enemies and spawn wait time after wave 30
            if (currentWave == WaveType.Advanced)
            {
                minEnemiesPerWave += 2;
                maxEnemiesPerWave += 2;
                spawnWaitTime = Mathf.Max(0.05f, spawnWaitTime - 0.05f); // Ensure spawnWaitTime doesn't go below 0.1
            }
            else
            {
                // Increase the speed for the next wave
                currentEnemySpeed += speedIncrement;
            }

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

            // Update the speed of all active enemies
            UpdateEnemySpeeds(currentEnemySpeed);
        }
    }

    private WaveType GetNextWaveType(WaveType currentWave)
    {
        switch (currentWave)
        {
            case WaveType.Initial:
                return WaveType.Regular;
            case WaveType.Regular:
                return WaveType.Advanced;
            case WaveType.Advanced:
                return WaveType.Advanced; // Keep it advanced after reaching advanced
            default:
                return WaveType.Regular;
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
