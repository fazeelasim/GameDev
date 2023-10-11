using UnityEngine;
using TMPro;

public class VelocityDisplay : MonoBehaviour
{
    public TextMeshProUGUI playerVelocityText; // Reference to the TMP text field

    private Rigidbody playerRigidbody;

    void Start()
    {
        // Assuming the player's GameObject has a Rigidbody component.
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerRigidbody = player.GetComponent<Rigidbody>();
        }
        else
        {
            Debug.LogError("Player not found or doesn't have a Rigidbody component.");
        }
    }

    void Update()
    {
        if (playerRigidbody != null)
        {
            Vector3 playerVelocity = playerRigidbody.velocity;
            playerVelocityText.text = "Player Velocity: " + playerVelocity.ToString("F2");
        }
    }
}
