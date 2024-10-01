using UnityEngine;

public class Tower : MonoBehaviour
{
    public float range = 2f;       // The radius of the tower's shooting range
    [SerializeField] private GameObject Bullet;  // Bullet prefab to shoot
    public Transform shootPoint;     // The point where the bullet will be spawned
    public float fireRate = 1f;      // Time between shots
    private Transform target;        // Current target enemy
    private float fireCountdown = 0f;

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
        GameObject bulletGO = (GameObject)Instantiate(Bullet, shootPoint.position, shootPoint.rotation);
        Bullets bullet = bulletGO.GetComponent<Bullets>();

        if (bullet != null)
        {
            bullet.Seek(target); // Pass the target to the bullet script
        }
    }
}