using UnityEngine;
using TMPro;

public class PositionDisplay : MonoBehaviour
{
    public TextMeshProUGUI playerPositionText; // Reference to the TMP text field

    private Transform playerTransform;

    void Start()
    {
        // Assuming the player's GameObject has the "Player" tag.
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("Player not found. Make sure the player has the 'Player' tag.");
        }
    }

    void Update()
    {
        if (playerTransform != null)
        {
            Vector3 playerPosition = playerTransform.position;
            playerPositionText.text = "Player Position: " + playerPosition.ToString("F2");
        }
    }
}
