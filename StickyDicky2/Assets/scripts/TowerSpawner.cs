using UnityEngine;
using UnityEngine.UI;

public class TowerSpawner : MonoBehaviour
{
    public GameObject prefab; // Assign your prefab in the Inspector
    public Button spawnButton; // Assign your button in the Inspector
    private Camera mainCamera;
    private GameObject spawnedObject;
    private bool isPlacing;

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
            mousePosition.z = 10f; // Set this to the distance from the camera to the object
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);
            spawnedObject.transform.position = worldPosition;

            if (Input.GetMouseButtonDown(0)) // Left mouse button click
            {
                if (worldPosition.y >= -4.5f)
                {
                    isPlacing = false;
                    spawnedObject = null; // Allow spawning a new object
                    spawnButton.interactable = true; // Re-enable the button
                }
                else
                {
                    Debug.Log("Cannot place object below y = -4.5");
                }
            }
        }
    }

    void SpawnObject()
    {
        if (!isPlacing)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10f; // Set this to the distance from the camera to the object
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);

            spawnedObject = Instantiate(prefab, worldPosition, Quaternion.identity);
            isPlacing = true;
            spawnButton.interactable = false; // Disable the button
        }
    }
}
