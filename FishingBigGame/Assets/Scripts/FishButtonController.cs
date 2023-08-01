using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FishButtonController : MonoBehaviour
{
    // UI TextMeshPro Text to display the player's fish info
    public TMP_Text playerFishInfoText; // Use TMP_Text instead of Text

    [System.Serializable]
    public class FishData
    {
        public string name;
        public string fishName;
        public int worth;
        public string averageLength;
        public bool discovered;
        public int count;
    }


    // Define multiple instances of the FishData class for different fish
    public FishData fishData1; // Example fish data 1
    public FishData fishData2; // Example fish data 2
    // Add more fishData variables as needed for each type of fish

    // Function to handle the click event of the fish button
    public void OnFishButtonClicked()
    {
        // Get the fish name from the clicked button's name
        string fishName = gameObject.name;

        // Find the fish data by name
        FishData fish = FindFishDataByName(fishName);

        // Check if the fish data is valid and discovered
        if (fish != null && fish.discovered)
        {
            // If discovered, format and show the fish data
            string fishInfo = $"Fish Name: {fish.fishName}\n" +
                              $"Worth: {fish.worth}g\n" +
                              $"Avg. Length: {fish.averageLength}\n" +
                              $"Amount: {fish.count}";
            playerFishInfoText.text = fishInfo;
        }
        else
        {
            // If not discovered, display a message indicating it's not discovered
            playerFishInfoText.text = "You have not discovered this fish yet.";
        }
    }

    // Function to find fish data by name
    private FishData FindFishDataByName(string name)
    {
        if (name == fishData1.name)
        {
            return fishData1;
        }
        else if (name == fishData2.name)
        {
            return fishData2;
        }
        // Add more if-else conditions as needed for additional fish data

        // If the fish name is not found, return null
        return null;
    }
}
