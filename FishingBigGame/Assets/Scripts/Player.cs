using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    private Vector3 currentPosition;
    private Vector3 offset = Vector3.zero;
    private Dictionary<string, Vector3> gridPositions = new Dictionary<string, Vector3>();

    public TMP_Text commandBoxText;
    public float speed = 250f;
    public float rotationSpeed = 100f;
    public bool canMove = true;
    public TMP_Text shopAvailability;
    public bool shopOpen;
    public int Energy;

    public TMP_Text energyText; // Reference to the TextMeshProUGUI component for energy display

    private int energyLossPerDistance = 5;
    private Vector3 startingPosition;
    private Vector3 lastEnergyDeductionPosition;
    private bool initialEnergyDeductionDone = false;

    private void Start()
    {
        shopAvailability.gameObject.SetActive(false);
        shopOpen = false;
        Energy = 450;
        // Store the initial starting position of the player
        startingPosition = transform.position;

        // Initialize the last energy deduction position
        lastEnergyDeductionPosition = startingPosition;

        // Set initial energy value
        UpdateEnergyDisplay();
    }

    private void Update()
    {
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

            currentPosition = transform.position;
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            // Move forward and backward
            transform.Translate(Vector3.left * verticalInput * speed * Time.deltaTime);

            // Rotate left and right
            transform.Rotate(Vector3.up * horizontalInput * rotationSpeed * Time.deltaTime);
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
        }
    }

    void OnCollisionExit(Collision targetObj)
    {
        shopAvailability.gameObject.SetActive(false);
    }

    private void UpdateEnergyDisplay()
    {
        energyText.text = "Energy: " + Energy.ToString();
    }
}
