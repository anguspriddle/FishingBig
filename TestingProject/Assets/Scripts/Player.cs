using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    private Vector3 targetPosition;
    private float movementSpeed = 50f;
    private Dictionary<string, Vector3> gridPositions = new Dictionary<string, Vector3>();

    public TMP_Text commandBoxText; // Reference to the TextMeshProUGUI component

    private void Start()
    {
        float gridSize = 90f; // The distance between each grid square

        char[] letters = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h' }; // Letters for the columns
        int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8 }; // Numbers for the rows

        for (int i = 0; i < letters.Length; i++)
        {
            for (int j = 0; j < numbers.Length; j++)
            {
                string key = letters[i] + numbers[j].ToString(); // Generate the key

                float x = -308f + (i * gridSize); // Calculate x position based on column index
                float z = 317f - (j * gridSize); // Calculate z position based on row index

                Vector3 position = new Vector3(x, 10f, z); // Create the position vector

                gridPositions.Add(key, position); // Add the key and position to the dictionary
            }
        }
        // gridPositions.Add("a1", new Vector3(-308f, 10f, 317f));
        // gridPositions.Add("a2", new Vector3(-218f, 10f, 317f));
        // gridPositions.Add("a3", new Vector3(-128f, 10f, 317f));
        // go on until a8
        // gridPositions.Add("b1", new Vector3(-308f, 10f, 227f));
        // continue grid positions
    }

    public void PlayerMove(string targetGridSpace)
    {
        if (gridPositions.ContainsKey(targetGridSpace))
        {
            targetPosition = gridPositions[targetGridSpace]; // Retrieve the target position from the dictionary
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
        while (transform.position != targetPosition)
        {
            // Move towards the target position using interpolation
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, movementSpeed * Time.deltaTime);
            yield return null;
        }

        LogToCommandBox("Reached the target position");
    }

    private void LogToCommandBox(string message)
    {
        commandBoxText.text += "/n" + "  - " + message;
    }

}