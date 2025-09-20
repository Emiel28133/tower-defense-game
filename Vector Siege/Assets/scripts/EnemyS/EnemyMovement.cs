using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints;  // Array of waypoints (set these in the inspector)
    [SerializeField] private float speed = 5f;       // Speed of the enemy movement

    private PlayerHealth playerHealth; // Reference to the PlayerHealth script
    private int waypointIndex = 0; // Index of the current waypoint

    public Transform[] Waypoints
    {
        set { waypoints = value; }
    }

    public int Speed
    {
        set { speed = value; }
    }
    void Start()
    {
        // Find the PlayerHealth script in the scene
        playerHealth = FindObjectOfType<PlayerHealth>();
        
    }

    void Update()
    {
        MoveTowardsWaypoint();
    }

    void MoveTowardsWaypoint()
    {
        // Check if there are waypoints
        if (waypoints == null || waypoints.Length == 0) return;

        // Get the current target waypoint
        Transform targetWaypoint = waypoints[waypointIndex];

        // Calculate movement direction
        Vector3 direction = targetWaypoint.position - transform.position;
        direction.Normalize();  // Normalize to get unit vector

        // Move towards the waypoint
        transform.position += direction * speed * Time.deltaTime;

        // Face the direction of movement (left or right)
        if (direction.x != 0)
        {
            Vector3 localScale = transform.localScale;
            localScale.x = Mathf.Sign(direction.x) * Mathf.Abs(localScale.x);
            transform.localScale = localScale;
        }

        // Check if the enemy has reached the waypoint
        if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.2f)
        {
            // Move to the next waypoint, or stop if at the end
            waypointIndex++;
            if (waypointIndex >= waypoints.Length)
            {
                // If it reaches the end, reduce player health and destroy the enemy
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(1);
                }
                Destroy(gameObject);
            }
        }
    }



}
