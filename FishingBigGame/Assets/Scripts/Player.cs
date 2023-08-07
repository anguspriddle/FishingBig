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

    private Vector3 CalculateWorldPosition(float rotationY, char letter, int number)
    {
        float angle = rotationY * Mathf.Deg2Rad;
        float sinAngle = Mathf.Sin(angle);
        float cosAngle = Mathf.Cos(angle);

        float offsetX = (letter - 'a') * gridSize; // Calculate offsetX based on the letter (column)
        float offsetZ = (number - 1) * gridSize;   // Calculate offsetZ based on the number (row)

        float newX = offsetX * cosAngle - offsetZ * sinAngle; // Swap sinAngle with -sinAngle
        float newZ = offsetX * sinAngle + offsetZ * cosAngle; // Swap cosAngle with sinAngle

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
            Vector3 newPosition = CalculateNewPosition(targetPosition, "c3");

            // Check if the newPosition collides with any obstacles
            Collider[] colliders = Physics.OverlapSphere(newPosition, 0.2f, LayerMask.GetMask("Obstacles"));
            if (colliders.Length > 0)
            {
                Debug.Log("Cannot move to this position. It's obstructed.");
                return; // Do not move if there's an obstacle in the way
            }

            MovePlayerToPosition(newPosition);
        }
        else
        {
            Debug.Log("Invalid target grid space provided.");
        }
    }

    private Vector3 CalculateNewPosition(Vector3 targetPosition, string referenceGridSpace)
    {
        Vector3 offsetToReferenceGridSpace = currentPosition - gridPositions[referenceGridSpace];
        Quaternion playerRotation = Quaternion.Euler(0f, transform.eulerAngles.y, 0f);
        Vector3 rotatedOffset = playerRotation * offsetToReferenceGridSpace;
        Vector3 newPosition = targetPosition + rotatedOffset;
        return newPosition;
    }
    private void MovePlayerToPosition(Vector3 newPosition)
    {
        currentPosition = newPosition;
        transform.position = currentPosition;
    }
}
