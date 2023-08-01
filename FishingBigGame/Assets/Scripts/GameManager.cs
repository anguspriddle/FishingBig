using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Transform objectToTrack;
    public TMP_Text coordinatesText; // Use TMP_Text instead of Text

    private void Start()
    {
        // Make sure the objectToTrack and coordinatesText references are set
        if (objectToTrack == null || coordinatesText == null)
        {
            Debug.LogError("Please assign the 'objectToTrack' and 'coordinatesText' in the Inspector.");
            enabled = false;
            return;
        }

        UpdateCoordinatesText();
    }

    private void Update()
    {
        UpdateCoordinatesText();
    }

    private void UpdateCoordinatesText()
    {
        // Get the X and Z coordinates of the objectToTrack
        float xCoordinate = objectToTrack.position.x;
        float zCoordinate = objectToTrack.position.z;

        // Update the text on the TMP Text
        coordinatesText.text = "X: " + xCoordinate.ToString("F2") + "   Z: " + zCoordinate.ToString("F2");
    }

    public void ToggleGameObjectActive(GameObject inventoryBG)
    {
        if (inventoryBG != null)
        {
            inventoryBG.SetActive(!inventoryBG.activeSelf);
        }
    }

}
