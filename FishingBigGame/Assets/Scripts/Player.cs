using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    private Vector3 targetPosition;
    private Vector3 currentPosition = new Vector3(-8f, -346.7815f, 118f); // Set the starting position here
    private float movementSpeed = 50f;
    private Dictionary<string, Vector3> gridPositions = new Dictionary<string, Vector3>();

    public TMP_Text commandBoxText; // Reference to the TextMeshProUGUI component

    private void Start()
    {
        float gridSize = 20f; // The distance between each grid square

        char[] letters = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h' }; // Letters for the columns
        int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8 }; // Numbers for the rows

        for (int i = 0; i < letters.Length; i++)
        {
            for (int j = 0; j < numbers.Length; j++)
            {
                string key = letters[i] + numbers[j].ToString(); // Generate the key

                float x = currentPosition.x + (i * gridSize); // Calculate x position based on column index
                float z = currentPosition.z + (j * gridSize); // Calculate z position based on row index

                Vector3 position = new Vector3(x, currentPosition.y, z); // Create the position vector

                gridPositions.Add(key, position); // Add the key and position to the dictionary
            }
        }
    }

    public void PlayerMove(string targetGridSpace)
    {
        if (gridPositions.ContainsKey(targetGridSpace))
        {
            targetPosition = gridPositions[targetGridSpace]; // Retrieve the target position from the dictionary

            // Update the current position to the new target position
            currentPosition = targetPosition;

            // Move the player to the new position
            LogToCommandBox("Moved to " + targetGridSpace);
            StartCoroutine(MoveToTargetPosition());
        }
        else
        {
            LogToCommandBox("Invalid target grid space");
        }
    }

    private IEnumerator MoveToTargetPosition()
    {
        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            // Move towards the target position using interpolation
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, movementSpeed * Time.deltaTime);
            yield return null;
        }

        LogToCommandBox("Reached the target position");
    }

    private void LogToCommandBox(string message)
    {
        commandBoxText.text += "\n" + "  - " + message;
    }
}
