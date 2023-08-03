using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class commandDetailsManager : MonoBehaviour
{
    public GameObject commandScreen;
    private bool isCommandScreen = false;
    // Start is called before the first frame update
    void Start()
    {
        commandScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the "i" key is pressed
        if (Input.GetKeyDown(KeyCode.C))
        {
            // Toggle the boolean variable isCommandScreen
            isCommandScreen = !isCommandScreen;

            // Set the inventory screen active state based on the boolean variable
            commandScreen.SetActive(isCommandScreen);
        }
    }
}
