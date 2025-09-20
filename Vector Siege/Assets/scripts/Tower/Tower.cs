using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private float range = 2f;       // The radius of the tower's shooting range
    [SerializeField] private GameObject Bullet;  // Bullet prefab to shoot
    [SerializeField] private Transform shootPoint;     // The point where the bullet will be spawned
    [SerializeField] private float baseFireRate = 0.5f;  // Base time between shots
    [SerializeField] private Transform target;        // Current target enemy
    [SerializeField] private float fireCountdown = 0f;

    private static List<Tower> allTowers = new List<Tower>();
    private static float fireRateUpgrade = 0f; // Static variable to keep track of fire rate upgrade

    [SerializeField] private float fireRate; // Instance variable to store the effective fire rate

    void Awake()
    {
        allTowers.Add(this);
        fireRate = baseFireRate + fireRateUpgrade; // Initialize fire rate with the upgrade
    }

    void OnDestroy()
    {
        allTowers.Remove(this);
    }

    void Update()
    {
        // Find the closest enemy in range
        FindTarget();

        if (target == null)
            return;

        // Check if it's time to shoot
        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    // Method to find the closest enemy within range
    void FindTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");  // Find all enemies
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        // Loop through all enemies to find the closest one
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            // Check if the enemy is closer than the previous ones
            if (distanceToEnemy < shortestDistance && distanceToEnemy <= range)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    // Method to shoot a bullet towards the enemy
    void Shoot()
    {
        Debug.Log("Shooting at target: " + target.name);
        GameObject bulletGO = (GameObject)Instantiate(Bullet, shootPoint.position, shootPoint.rotation);
        Bullets bullet = bulletGO.GetComponent<Bullets>();

        if (bullet != null)
        {
            bullet.Seek(target); // Pass the target to the bullet script
        }
    }

    public static void UpgradeFireRate(float amount)
    {
        fireRateUpgrade += amount; // Update the static fire rate upgrade

        foreach (Tower tower in allTowers)
        {
            tower.fireRate = tower.baseFireRate + fireRateUpgrade; // Update the fire rate of existing towers
        }
    }
}
