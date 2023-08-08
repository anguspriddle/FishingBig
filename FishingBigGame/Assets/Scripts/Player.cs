using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    private Vector3 currentPosition; // Will store the current position of the player
    private Vector3 offset = Vector3.zero; // Offset from C3
    private Dictionary<string, Vector3> gridPositions = new Dictionary<string, Vector3>();

    public TMP_Text commandBoxText; // Reference to the TextMeshProUGUI component

    public float speed = 250f;
    public float rotationSpeed = 100f;

    private void Start()
    {
        
    }



    // Update the currentPosition every frame to keep it in sync with the player's position
    private void Update()
    {
        currentPosition = transform.position;
        // Boat movement and rotation
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Move forward and backward
        transform.Translate(Vector3.left * verticalInput * speed * Time.deltaTime);

        // Rotate left and right
        transform.Rotate(Vector3.up * horizontalInput * rotationSpeed * Time.deltaTime);
    }

    
}
