using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FishingManager : MonoBehaviour
{
    [System.Serializable]
    public class Fish
    {
        public string name;
        public float catchChance;
    }

    public Player playerScript;
    public List<Fish> availableFish;

    public TextMeshProUGUI fishingText;

    private bool isFishing;

    public void StartFishing()
    {
        if (!isFishing)
        {
            StartCoroutine(FishingProcess());
        }
    }

    private IEnumerator FishingProcess()
    {
        isFishing = true;
        playerScript.ToggleMovement(false);
        fishingText.text = "Fishing...";

        yield return new WaitForSeconds(2f);

        // Start the skill check part.
        SkillCheckManager.Instance.StartSkillCheck(this);
    }

    public void EndFishing(bool success, Fish caughtFish)
    {
        if (success)
        {
            fishingText.color = Color.green;
            fishingText.text = "Success!";

            if (caughtFish != null)
            {
                fishingText.text = "Caught 1x " + caughtFish.name + "!";
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
            fishingText.text = "Failed!";
        }

        StartCoroutine(ResetFishing());
    }

    private IEnumerator ResetFishing()
    {
        yield return new WaitForSeconds(2f);
        fishingText.color = Color.white;
        fishingText.text = ""; // Reset the text after the fishing process is done.
        playerScript.ToggleMovement(true);
        isFishing = false;
    }

    public Fish DetermineCaughtFish()
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
            randomValue -= fish.catchChance; // Subtract the catch chance of the current fish.

            if (randomValue <= 0f) // Compare against 0 or <= instead of <
            {
                return fish;
            }
        }

        return null; // If no fish is caught, return null.
    }
}
