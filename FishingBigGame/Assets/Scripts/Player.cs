using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour {
    private Vector3 currentPosition; // Will store the current position of the player
    private Vector3 offset = Vector3.zero; // Offset from C3
    private Dictionary<string, Vector3> gridPositions = new Dictionary<string, Vector3>();

    public TMP_Text commandBoxText; // Reference to the TextMeshProUGUI component


    private void Start()
    {
        currentPosition = transform.position; // Set the starting position to the player's initial position
        // Set up the grid square positions
        gridPositions.Add("a1", new Vector3(40f, 0f, -40f));
        gridPositions.Add("a2", new Vector3(40f, 0f, -20f));
        gridPositions.Add("a3", new Vector3(40f, 0f, 0f));
        gridPositions.Add("a4", new Vector3(40f, 0f, 20f));
        gridPositions.Add("a5", new Vector3(40f, 0f, 40f));

        gridPositions.Add("b1", new Vector3(20f, 0f, -40f));
        gridPositions.Add("b2", new Vector3(20f, 0f, -20f));
        gridPositions.Add("b3", new Vector3(20f, 0f, 0f));
        gridPositions.Add("b4", new Vector3(20f, 0f, 20f));
        gridPositions.Add("b5", new Vector3(20f, 0f, 40f));

        gridPositions.Add("c1", new Vector3(0f, 0f, -40f));
        gridPositions.Add("c2", new Vector3(0f, 0f, -20f));
        gridPositions.Add("c3", new Vector3(0f, 0f, 0f));
        gridPositions.Add("c4", new Vector3(0f, 0f, 20f));
        gridPositions.Add("c5", new Vector3(0f, 0f, 40f));

        gridPositions.Add("d1", new Vector3(-20f, 0f, -40f));
        gridPositions.Add("d2", new Vector3(-20f, 0f, -20f));
        gridPositions.Add("d3", new Vector3(-20f, 0f, 0f));
        gridPositions.Add("d4", new Vector3(-20f, 0f, 20f));
        gridPositions.Add("d5", new Vector3(-20f, 0f, 40f));

        gridPositions.Add("e1", new Vector3(-40f, 0f, -40f));
        gridPositions.Add("e2", new Vector3(-40f, 0f, -20f));
        gridPositions.Add("e3", new Vector3(-40f, 0f, 0f));
        gridPositions.Add("e4", new Vector3(-40f, 0f, 20f));
        gridPositions.Add("e5", new Vector3(-40f, 0f, 40f));
    }


    // Update the currentPosition every frame to keep it in sync with the player's position
    private void Update()
    {
        currentPosition = transform.position;
    }

    // Call this function when you want to move the player to a specific grid space
    public void PlayerMove(string targetGridSpace)
    {
        if (gridPositions.TryGetValue(targetGridSpace, out Vector3 targetPosition))
        {
            Vector3 newPosition = currentPosition + targetPosition - gridPositions["c3"] + offset;
            MovePlayerToPosition(newPosition);
        }
        else
        {
            Debug.Log("Invalid target grid space provided.");
        }
    }

    private void MovePlayerToPosition(Vector3 newPosition)
    {
        currentPosition = newPosition;
        transform.position = currentPosition;
    }
}
