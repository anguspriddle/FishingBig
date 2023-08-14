using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventoryManager : MonoBehaviour
{
    public GameObject inventoryScreen;
    private bool isInventoryOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        inventoryScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the "i" key is pressed
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            // Toggle the boolean variable isInventoryOpen
            isInventoryOpen = !isInventoryOpen;

            // Set the inventory screen active state based on the boolean variable
            inventoryScreen.SetActive(isInventoryOpen);
        }
    }
}
