using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public GameObject ShopScreen;
    public Player playerScript;
    public FishingManager FishingManager;
    public TextMeshProUGUI coinText;
    // Start is called before the first frame update
    void Start()
    {
        ShopScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCoinUI();
    }

    public void CloseShop()
    {
        ShopScreen.SetActive(false);
        playerScript.canMove = true;
    }

    public void SellAll()
    {
        Debug.Log("SellAll button clicked.");
        float totalValue = 0;

        foreach (FishingManager.Fish fish in FishingManager.availableFish)
        {
            totalValue += fish.fishWorth * fish.caughtAmount;
            fish.caughtAmount = 0; // Reset the caughtAmount to zero after selling.
        }

        playerScript.coins += (int)totalValue; // Convert float to int and increase player's coins.
        UpdateCoinUI(); // Update the UI to display the new coin count.
    }

    public void MaxEnergyUpgrade()
    {
        if (playerScript.coins >= 300 && playerScript.MaxEnergy < 1000) 
        {
            playerScript.coins -= 300;
            playerScript.MaxEnergy += 100;
            playerScript.Energy = playerScript.MaxEnergy;
        } 
    }

    public void MaxSpeedUpgrade()
    {
        if (playerScript.coins >= 500 && playerScript.speed < 600)
        {
            playerScript.coins -= 500;
            playerScript.speed += 75;
            playerScript.rotationSpeed += 20;
        }
    }

    private void UpdateCoinUI()
    {
        coinText.text = "Coins: " + playerScript.coins.ToString();
    }
}
