using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    public bool shopOpen;
    public int Energy;
    public int coins = 0;
    public TMP_Text energyText; // Reference to the TextMeshProUGUI component for energy display
    public TMP_Text maxEnergyText;

    private int energyLossPerDistance = 5;
    private Vector3 startingPosition;
    private Vector3 lastEnergyDeductionPosition;
    private bool initialEnergyDeductionDone = false;
    public bool restArea;
    public int MaxEnergy; // Maximum energy value

    private float energyRegenRate = 10f; // Energy regeneration rate per second
    private float restingTime = 1f; // Time interval for energy regeneration

    public bool isResting = false; // Flag to track if the player is resting
    private float restingTimer = 0f; // Timer for energy regeneration

    private void Start()
    {
        shopAvailability.gameObject.SetActive(false);
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
        maxEnergyText.text = "Max Energy: " + MaxEnergy.ToString();
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
            currentPosition = transform.position;
            movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * speed;
            rb.velocity = movement;
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
            restArea = true;
        }
        if (targetObj.gameObject.tag == "Wall")
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
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
            restArea = false;
            isResting = false;

        }
    }

    private void UpdateEnergyDisplay()
    {
        energyText.text = "Energy: " + Energy.ToString();
    }
}