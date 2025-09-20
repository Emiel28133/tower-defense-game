using UnityEngine;

public class Bullets : MonoBehaviour
{
    //public static event Action OnHit;
    [SerializeField] private float speed = 20f;
    [SerializeField] private float explosionRadius = 0f;
    [SerializeField] private GameObject impactEffect;
    [SerializeField] private Transform target;
    [SerializeField] private MoneySystem moneySystem; // Assign your MoneySystem in the Inspector

    // Function to assign a target to the bullet
    public void Seek(Transform _target)
    {
        target = _target;
    }

    void Start()
    {
        if (moneySystem == null)
        {
            moneySystem = FindObjectOfType<MoneySystem>();
        }
    }

    void Update()
    {
        // If there is no target, destroy the bullet
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        // Move the bullet towards the target
        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        // If the bullet reaches the target
        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        // Move the bullet forward
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    void HitTarget()
    {
        Damage(target);
        Destroy(gameObject); // Destroy the bullet
    }

    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Enemy")
            {
                Damage(collider.transform);
            }
        }
    }

    void Damage(Transform enemy)
    {
        // Here you can reduce the enemy's health or destroy it
        Destroy(enemy.gameObject); // Just destroying for now

        if (moneySystem != null)
        {
            moneySystem.AddMoney(1);
        }
    }

    // New method to increase bullet speed
    public void IncreaseSpeed(float amount)
    {
        speed += amount;
    }
}
