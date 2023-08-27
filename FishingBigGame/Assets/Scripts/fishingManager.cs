using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FishingManager : MonoBehaviour
{
    [System.Serializable]
    public class Fish
    {
        public string displayName;
        public string name;
        public float catchChance;
        public float fishWorth;
        public float caughtAmount;
        public string biomeTag;
    }

    public Player playerScript;
    public List<Fish> availableFish;
    public TextMeshProUGUI fishingText;
    public TextMeshProUGUI caughtFishText; // TextMeshProUGUI element to display caught fish info
    public TextMeshProUGUI totalWorthText; // TextMeshProUGUI element to display total worth

    private float totalWorth; // Total worth of caught fish

    public string currentBiomeTag; // Store the current biome's tag

    public void StartFishing()
    {
        StartCoroutine(FishingProcess());
    }

    private IEnumerator FishingProcess()
    {
        playerScript.ToggleMovement(false);
        playerScript.Energy -= 20;
        fishingText.text = "Fishing...";

        yield return new WaitForSeconds(2f);

        if (Random.value <= 0.5f)
        {
            fishingText.color = Color.green;
            fishingText.text = "Success!";

            yield return new WaitForSeconds(2f);

            Fish caughtFish = DetermineCaughtFish();

            if (caughtFish != null)
            {
                fishingText.text = "Caught 1x " + caughtFish.displayName + "!";
                caughtFish.caughtAmount += 1;
                totalWorth += caughtFish.fishWorth; // Update total worth

                UpdateCaughtFishText();
                UpdateTotalWorthText();
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
            yield return new WaitForSeconds(2f);
        }

        fishingText.color = Color.white;
        fishingText.text = "";
        playerScript.ToggleMovement(true);
    }

    private Fish DetermineCaughtFish()
    {
        float totalCatchChance = 0f;
        foreach (Fish fish in availableFish)
        {
            if (fish.biomeTag == currentBiomeTag)
            {
                totalCatchChance += fish.catchChance;
            }
        }

        float randomValue = Random.value * totalCatchChance;
        foreach (Fish fish in availableFish)
        {
            if (fish.biomeTag == currentBiomeTag)
            {
                randomValue -= fish.catchChance;

                if (randomValue <= 0f)
                {
                    return fish;
                }
            }
        }

        return null;
    }

    private void UpdateCaughtFishText()
    {
        caughtFishText.text = "Caught Fish:\n";
        foreach (Fish fish in availableFish)
        {
            if (fish.caughtAmount > 0)
            {
                caughtFishText.text += fish.displayName + ": " + fish.caughtAmount + "\n";
            }
        }
    }

    private void UpdateTotalWorthText()
    {
        totalWorthText.text = "Total Worth: $" + totalWorth.ToString("F2");
    }
}
