using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    private int count;
    private int numPickups = 3; // Put here the number of pickups you have .
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI winText;
    void Start()
    {
        count = 0;
        winText.text = " ";
        SetCountText();
    }
    void Update()
    {
        // Get input for movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate movement direction based on camera
        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;

        forward.y = 0; // Ensure the character stays upright
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        // Calculate the desired movement direction
        Vector3 moveDirection = forward * verticalInput + right * horizontalInput;

        // Move the character using the CharacterController attached to this GameObject
        CharacterController characterController = GetComponent<CharacterController>();
        characterController.Move(moveDirection * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PickUp")
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }
    }

    private void SetCountText()
    {
        scoreText.text = " Score : " + count.ToString();
        if (count >= numPickups)
        {
            winText.text = " You win ! ";
        }
    }
}
