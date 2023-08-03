using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class fishingManager : MonoBehaviour
{
    [System.Serializable]
    public class Fish
    {
        public string name;
        public float catchChance;
    }

    public List<Fish> availableFish;

    public TextMeshProUGUI fishingText;

    public void StartFishing()
    {
        StartCoroutine(FishingProcess());
    }

    private IEnumerator FishingProcess()
    {
        fishingText.text = "Fishing..."; // Show "Fishing..." text.

        yield return new WaitForSeconds(2f);

        // Check if the fishing attempt is successful.
        if (Random.value <= 0.5f)
        {
            fishingText.color = Color.green;
            fishingText.text = "Success!"; // Fishing attempt is successful.

            yield return new WaitForSeconds(2f);

            // Determine which fish has been caught.
            Fish caughtFish = DetermineCaughtFish();

            if (caughtFish != null)
            {
                fishingText.text = "Caught 1x " + caughtFish.name + "!"; // Display the caught fish's name.
                // TODO: Add the caughtFish to the player's collection or inventory.
            }
            else
            {
                fishingText.text = "Caught nothing!";
            }
        }
        else
        {
            fishingText.color = Color.red;
            fishingText.text = "Failed!"; // Fishing attempt failed.
            yield return new WaitForSeconds(2f);
        }

        fishingText.color = Color.white;
        fishingText.text = ""; // Reset the text after the fishing process is done.
    }

    private Fish DetermineCaughtFish()
    {
        // Determine the fish that has been caught based on their respective catch chances.

        float totalCatchChance = 0f;
        foreach (Fish fish in availableFish)
        {
            totalCatchChance += fish.catchChance;
        }

        float randomValue = Random.value * totalCatchChance;
        foreach (Fish fish in availableFish)
        {
            if (randomValue < fish.catchChance)
            {
                return fish;
            }
            randomValue -= fish.catchChance;
        }

        return null; // If no fish is caught, return null.
    }
}
