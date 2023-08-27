using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Player : MonoBehaviour
{
    private Vector3 currentPosition;
    private Vector3 movement;
    private Vector3 offset = Vector3.zero;
    private Dictionary<string, Vector3> gridPositions = new Dictionary<string, Vector3>();
    private Rigidbody rb;
    public TMP_Text commandBoxText;
    public float speed = 250f;
    public float rotationSpeed = 100f;
    public bool canMove = true;
    public TMP_Text shopAvailability;
    public TMP_Text restAvailability;
    public bool shopOpen;
    public int Energy;
    public int coins = 0;
    public TMP_Text energyText;
    public TMP_Text maxEnergyText;
    public TMP_Text highScoreText;
    public TMP_Text timerText;
    public TMP_Text biomeText;
    public FishingManager FishingManager;
    public CommandBoxManager commandBoxManager;


    private int energyLossPerDistance = 5;
    private Vector3 startingPosition;
    private Vector3 lastEnergyDeductionPosition;
    public bool restArea;
    public int MaxEnergy; // Maximum energy value

    private float energyRegenRate = 10f; // Energy regeneration rate per second
    private float restingTime = 1f; // Time interval for energy regeneration

    public bool isResting = false; // Flag to track if the player is resting
    private float restingTimer = 0f; // Timer for energy regeneration

    private float timer = 300f;

    private void Start()
    {

        // Call the method with resetScore set to false
        SaveScoreAndCheckHighScore(false);


        // ensure the resting command and shop are unavailable on launch
        shopAvailability.gameObject.SetActive(false);
        restAvailability.gameObject.SetActive(false);
        shopOpen = false;
        Energy = 450;
        MaxEnergy = 450;
        // Store the initial starting position of the player
        startingPosition = transform.position;
        rb = GetComponent<Rigidbody>();

        // Initialize the last energy deduction position
        lastEnergyDeductionPosition = startingPosition;

        // Set initial energy value
        UpdateEnergyDisplay();
    }

    private void Update()
    {
        // Timer logic
        if (timer > 0f)
        {
            timer -= Time.deltaTime;
            timerText.text = timer.ToString("F1"); // Convert timer to a formatted string with 1 decimal place
            if (timer <= 0f)
            {
                SaveScoreAndCheckHighScore(false); // Check high score before transitioning
                SceneManager.LoadScene("EndGame"); // Load the EndGame scene
            }
        }
        maxEnergyText.text = "Max Energy: " + MaxEnergy.ToString();
        if (Energy == 0)
        {
            SceneManager.LoadScene("GameOverState");
        }
        if (canMove)
        {
            // Calculate the distance moved since the last energy deduction
            float distanceMoved = Vector3.Distance(lastEnergyDeductionPosition, transform.position);

            // Calculate energy deduction and update display
            if (distanceMoved >= 200)
            {
                int energyDeduction = Mathf.FloorToInt(distanceMoved / 200) * energyLossPerDistance;
                Energy -= energyDeduction;
                lastEnergyDeductionPosition = transform.position;

                UpdateEnergyDisplay(); // Update energy display after energy deduction
            }
            if (isResting)
            {
                restingTimer += Time.deltaTime;
                if (restingTimer >= restingTime)
                {
                    int energyToAdd = Mathf.FloorToInt(energyRegenRate * restingTime);
                    Energy = Mathf.Min(Energy + energyToAdd, MaxEnergy);
                    restingTimer = 0f;

                    UpdateEnergyDisplay(); // Update energy display after energy regeneration
                }
            }
            
            // Calculate movement based on player's rotation
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            movement = (transform.forward * verticalInput + transform.right * horizontalInput) * speed;
            rb.velocity = movement;

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                RotatePlayer(rotationSpeed);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                RotatePlayer(-rotationSpeed);
            }
        }
    }


    public void ToggleMovement(bool enableMovement)
    {
        canMove = enableMovement;
    }


    void OnCollisionEnter(Collision targetObj)
    {
        if (targetObj.gameObject.tag == "Shop")
        {
            shopAvailability.gameObject.SetActive(true);
            shopAvailability.text = "Shop Open!";
            shopOpen = true;
            restArea = true;
        }
        if (targetObj.gameObject.tag == "RestingPoint")
        {
            restAvailability.gameObject.SetActive(true);
            restAvailability.text = "Can Rest";
            restArea = true;
        }
        if (targetObj.gameObject.tag == "Wall")
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        if (targetObj.gameObject.tag == "NPC")
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            Energy -= 50;
        }
    }

    void OnCollisionExit(Collision targetObj)
    {
        if (targetObj.gameObject.tag == "Shop")
        {
            shopAvailability.gameObject.SetActive(false);
            restArea = false;
            shopAvailability.text = "";
            shopOpen = false;
        }
        if (targetObj.gameObject.tag == "RestingPoint")
        {
            restAvailability.gameObject.SetActive(false);
            restArea = false;
            isResting = false;

        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("NormalBiome"))
        {
            Debug.Log("Entered the Normal Biome");
            biomeText.text = "Biome: Normal";
            FishingManager.currentBiomeTag = "NormalBiome";
        }
        if (other.gameObject.CompareTag("CoralReefBiome"))
        {
            biomeText.text = "Biome: Coral Reef";
            FishingManager.currentBiomeTag = "CoralReefBiome";
        }
        if (other.gameObject.CompareTag("IceBiome"))
        {
            biomeText.text = "Biome: Ice";
            FishingManager.currentBiomeTag = "IceBiome";
        }
        if (other.gameObject.CompareTag("SwampBiome"))
        {
            biomeText.text = "Biome: Swamp";
            FishingManager.currentBiomeTag = "SwampBiome";
        }
    }


    private void UpdateEnergyDisplay()
    {
        energyText.text = "Energy: " + Energy.ToString();
    }
    void RotatePlayer(float angle)
    {
        // Rotate the player around the y-axis
        transform.Rotate(0f, -angle * Time.deltaTime, 0f);
    }

    private void SaveScoreAndCheckHighScore(bool resetScore)
    {
        if (resetScore)
        {
            PlayerPrefs.SetInt("Score", 0); // Reset the score to 0
        }

        int currentScore = PlayerPrefs.GetInt("Score", 0); // Get the current score from PlayerPrefs
        int playerCoins = coins; // Get the player's current coins

        // Add the player's coins to the current score
        currentScore += playerCoins;

        // Save the updated score back to PlayerPrefs
        PlayerPrefs.SetInt("Score", currentScore);
        PlayerPrefs.Save(); // Save PlayerPrefs changes

        // Check if the current score is higher than the high score
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        if (currentScore > highScore)
        {
            PlayerPrefs.SetInt("HighScore", currentScore);
            PlayerPrefs.Save(); // Save PlayerPrefs changes
        }
    }


}