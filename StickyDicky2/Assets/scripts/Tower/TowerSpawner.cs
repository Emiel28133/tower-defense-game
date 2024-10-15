using UnityEngine;
using UnityEngine.UI;

public class TowerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject prefab; // Assign your prefab in the Inspector
    [SerializeField] private Button spawnButton; // Assign your button in the Inspector
    [SerializeField] private MoneySystem moneySystem; // Assign your MoneySystem in the Inspector
    private Camera mainCamera;
    private GameObject spawnedObject;
    private bool isPlacing;
    private const float checkRadius = 0.5f; // Radius for collision check

    void Start()
    {
        mainCamera = Camera.main;
        spawnButton.onClick.AddListener(SpawnObject);
    }

    void Update()
    {
        if (isPlacing && spawnedObject != null)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10f; // distance from the camera to the object
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);
            spawnedObject.transform.position = worldPosition;

            if (Input.GetMouseButtonDown(0)) // Left mouse button click
            {
                if (worldPosition.y >= -4.5f && !IsCollidingWithTower(worldPosition))
                {
                    isPlacing = false;
                    EnableCollider(spawnedObject, true); // Re-enable the collider
                    spawnedObject = null; // Allow spawning a new object
                    spawnButton.interactable = true; // Re-enable the button
                }
                else
                {
                    Debug.Log("Cannot place object here");
                }
            }
        }
    }

    void SpawnObject()
    {
        if (!isPlacing && moneySystem != null && moneySystem.Money >= 10)
        {
            Debug.Log("Spawning object");

            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10f; // Set this to the distance from the camera to the object
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);

            spawnedObject = Instantiate(prefab, worldPosition, Quaternion.identity);
            EnableCollider(spawnedObject, false); // Disable the collider
            isPlacing = true;
            moneySystem.SpendMoney();
        }
    }

    private bool IsCollidingWithTower(Vector3 position)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, checkRadius, LayerMask.GetMask("Towers"));
        Debug.Log($"Checking collision at position {position}, found {colliders.Length} colliders");
        return colliders.Length > 0;
    }

    private void EnableCollider(GameObject obj, bool enable)
    {
        Collider2D collider = obj.GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.enabled = enable;
        }
    }
}
