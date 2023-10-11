using UnityEngine;
using TMPro;

public class DistanceDisplay : MonoBehaviour
{
    public TextMeshProUGUI distanceText; // Reference to the TMP text field
    public string targetTag = "PickUp"; // Tag of the target GameObjects
    public Material greenMaterial; // Material for the green pickup

    private Transform playerTransform;
    private Transform nearestTargetTransform;
    private float nearestDistance = float.MaxValue;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("Player not found. Make sure the player has the 'Player' tag.");
        }
        UpdateNearestTarget();
    }

    void Update()
    {
        if (playerTransform != null)
        {
            float distance = Vector3.Distance(playerTransform.position, nearestTargetTransform.position);
            distanceText.text = "Distance to Nearest " + targetTag + ": " + distance.ToString("F2");
        }
    }

    void UpdateNearestTarget()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag(targetTag);
        foreach (GameObject target in targets)
        {
            float distance = Vector3.Distance(playerTransform.position, target.transform.position);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestTargetTransform = target.transform;
            }
        }

        // Change the material of the closest pickup to green
        Renderer nearestRenderer = nearestTargetTransform.GetComponent<Renderer>();
        if (nearestRenderer != null)
        {
            nearestRenderer.material = greenMaterial;
        }
    }
}
