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

    // Define the grid size and offset variables
    public int gridSize = 200;
    public int gridOffset = 200;

    private void Start()
    {
        currentPosition = transform.position; // Set the starting position to the player's initial position

        // Calculate the player's rotation on the y-axis in degrees
        float playerRotationY = transform.eulerAngles.y;

        // Define the grid letters and numbers
        char[] letters = { 'a', 'b', 'c', 'd', 'e' };
        int[] numbers = { 1, 2, 3, 4, 5 };

        // Loop through all possible combinations of grid positions and calculate their world positions based on player's rotation
        foreach (char letter in letters)
        {
            foreach (int number in numbers)
            {
                string gridSpace = letter.ToString() + number.ToString();
                Vector3 worldPosition = CalculateWorldPosition(playerRotationY, letter, number);
                gridPositions.Add(gridSpace, worldPosition);
            }
        }
    }

    // Calculate the world position of a grid space based on player's rotation
    private Vector3 CalculateWorldPosition(float rotationY, char letter, int number)
    {
        float angle = rotationY * Mathf.Deg2Rad;
        float sinAngle = Mathf.Sin(angle);
        float cosAngle = Mathf.Cos(angle);

        float offsetX = (number - 3) * gridSize;
        float offsetZ = (letter - 'c') * gridSize;

        float newX = offsetX * cosAngle + offsetZ * sinAngle;
        float newZ = offsetX * -sinAngle + offsetZ * cosAngle;

        float worldX = currentPosition.x + newX + gridOffset;
        float worldZ = currentPosition.z + newZ + gridOffset;

        return new Vector3(worldX, 0f, worldZ);
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
            Vector3 newPosition = currentPosition + targetPosition - gridPositions["c3"];
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
