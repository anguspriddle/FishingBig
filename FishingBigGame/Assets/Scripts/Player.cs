using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    private Vector3 currentPosition = new Vector3(160f, 0f, 200f); // Set the starting position here
    private Vector3 offset = Vector3.zero; // Offset from C3
    private Dictionary<string, Vector3> gridPositions = new Dictionary<string, Vector3>();

    public TMP_Text commandBoxText; // Reference to the TextMeshProUGUI component

    private void Start()
    {
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

    public void PlayerMove(string targetGridSquare)
    {
        if (IsValidGridSquare(targetGridSquare))
        {
            Vector3 targetPosition = gridPositions[targetGridSquare];

            // Calculate the adjustment to the player's current position
            Vector3 adjustment = targetPosition - currentPosition;

            // Update the current position by adding the adjustment
            currentPosition += adjustment;

            // Move the player to the new position
            transform.position = currentPosition;

            LogToCommandBox("Adjusted to " + targetGridSquare);
        }
        else
        {
            LogToCommandBox("Invalid target grid square");
        }
    }
    private bool IsValidGridSquare(string gridSquare)
    {
        return gridPositions.ContainsKey(gridSquare);
    }

    private void LogToCommandBox(string message)
    {
        commandBoxText.text += "\n" + "  - " + message;
    }
}
