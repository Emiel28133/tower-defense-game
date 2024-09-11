using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform[] waypoints;  // Array of waypoints (set these in the inspector)
    public float speed = 5f;       // Speed of the enemy movement

    private int waypointIndex = 0; // Index of the current waypoint

    void Update()
    {
        MoveTowardsWaypoint();
    }

    void MoveTowardsWaypoint()
    {
        // Check if there are waypoints
        if (waypoints.Length == 0) return;

        // Get the current target waypoint
        Transform targetWaypoint = waypoints[waypointIndex];

        // Calculate movement direction
        Vector3 direction = targetWaypoint.position - transform.position;
        direction.Normalize();  // Normalize to get unit vector

        // Move towards the waypoint
        transform.position += direction * speed * Time.deltaTime;

        // Check if the enemy has reached the waypoint
        if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            // Move to the next waypoint, or stop if at the end
            waypointIndex++;
            if (waypointIndex >= waypoints.Length)
            {
                // If it reaches the end, you can destroy the enemy or loop the path
                // For now, destroy the enemy
                Destroy(gameObject);
            }
        }
    }
}
