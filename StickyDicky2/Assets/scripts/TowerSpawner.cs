using UnityEngine;
using UnityEngine.UI;

public class TowerSpawner : MonoBehaviour
{
    public GameObject prefab;
    public Button spawnButton;
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
            mousePosition.z = 10f;
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);
            spawnedObject.transform.position = worldPosition;

            if (Input.GetMouseButtonDown(0))
            {
                isPlacing = false;
                spawnedObject = null;
                spawnButton.interactable = true;
            }
        }
    }

    void SpawnObject()
    {
        if (!isPlacing)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10f;
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);

            spawnedObject = Instantiate(prefab, worldPosition, Quaternion.identity);
            isPlacing = true;
            spawnButton.interactable = false;
        }
    }
}
