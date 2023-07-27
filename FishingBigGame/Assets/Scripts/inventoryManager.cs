using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventoryManager : MonoBehaviour
{
    public GameObject inventoryScreen;
    // Start is called before the first frame update
    void Start()
    {
        inventoryScreen.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the "i" key is pressed
        if (Input.GetKeyDown(KeyCode.i))
        {
            // Set the boolean variable of the targetGameObject to true
            inventoryScreen.gameObject.SetActive(true);
        }
    }
}
