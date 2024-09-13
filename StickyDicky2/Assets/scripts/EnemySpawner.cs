using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;        // Prefab of the enemy to spawn
    public Transform[] waypoints;         // Array of waypoints to pass to the enemy
    public float spawnWaitTime = 0.5f;    // Time between each enemy spawn
    public float timeBetweenWaves = 10f;  // Time between waves
    public int minEnemiesPerWave = 10;    // Minimum number of enemies per wave
    public int maxEnemiesPerWave = 20;    // Maximum number of enemies per wave
    int enemiesInThisWave;
    private bool waveInProgress = false;  // To track if a wave is currently in progress

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
                    enemyMovement.waypoints = waypoints; // Assign waypoints to the enemy
                }

                // Wait for the next enemy to spawn
                yield return new WaitForSeconds(spawnWaitTime);
            }

            // Wait until all enemies are destroyed before starting a new wave
            yield return new WaitUntil(AllEnemiesDestroyed);

            // Mark wave as finished
            waveInProgress = false;
        }
    }

    bool AllEnemiesDestroyed()
    {
        // Check if there are no more enemies in the scene
        return GameObject.FindGameObjectsWithTag("Enemy").Length == 0;
    }
}
