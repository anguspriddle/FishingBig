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
        gridPositions.Add("a1", new Vector3(-309f, 10f, 317f));
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
            LogToCommandBox("Invalid target grid space" );
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
        commandBoxText.text += "/n" + "  - " + message ;
    }

}
