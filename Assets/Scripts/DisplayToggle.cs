using UnityEngine;

public class DisplayToggle : MonoBehaviour
{
    public GameObject positionDisplay;
    public GameObject velocityDisplay;
    public GameObject distanceDisplay;
    public LineRenderer playerVelocityLineRenderer;
    public string targetTag = "PickUp";

    private Transform playerTransform;
    private Material originalMaterial;
    private GameObject[] pickups;

    public DebugMode currentDebugMode = DebugMode.Normal; // Initial mode

    void Start()
    {
        ToggleDebugMode(currentDebugMode);
        pickups = GameObject.FindGameObjectsWithTag(targetTag);
        if (pickups.Length > 0)
        {
            originalMaterial = pickups[0].GetComponent<Renderer>().material;
        }
        else
        {
            Debug.LogError("No objects found with tag: " + targetTag);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentDebugMode = (DebugMode)(((int)currentDebugMode + 1) % System.Enum.GetValues(typeof(DebugMode)).Length);
            ToggleDebugMode(currentDebugMode);
        }
    }

    void ToggleDebugMode(DebugMode mode)
    {
        positionDisplay.SetActive(mode == DebugMode.Normal || mode == DebugMode.Distance);
        velocityDisplay.SetActive(mode == DebugMode.Normal || mode == DebugMode.Distance);
        distanceDisplay.SetActive(mode == DebugMode.Distance);

        if (mode == DebugMode.Vision)
        {
            EnableVisionMode();
        }
        else
        {
            DisableVisionMode();
        }
    }

    void EnableVisionMode()
    {
        playerVelocityLineRenderer.enabled = true;

        Transform closestPickup = DetermineClosestPickup();

        foreach (GameObject pickup in pickups)
        {
            Renderer renderer = pickup.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = originalMaterial.color;
            }
        }

        if (closestPickup != null)
        {
            Renderer closestRenderer = closestPickup.GetComponent<Renderer>();
            if (closestRenderer != null)
            {
                closestRenderer.material.color = Color.green;
                closestPickup.LookAt(playerTransform);
            }
        }
    }

    void DisableVisionMode()
    {
        playerVelocityLineRenderer.enabled = false;

        foreach (GameObject pickup in pickups)
        {
            Renderer renderer = pickup.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = originalMaterial.color;
            }
        }
    }

    Transform DetermineClosestPickup()
    {
        float closestDistance = float.MaxValue;
        Transform closestPickup = null;

        foreach (GameObject pickup in pickups)
        {
            float distance = Vector3.Distance(playerTransform.position, pickup.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestPickup = pickup.transform;
            }
        }

        return closestPickup;
    }
}
