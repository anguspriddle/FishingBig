using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public GameObject ShopScreen;
    public Player playerScript;
    // Start is called before the first frame update
    void Start()
    {
        ShopScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CloseShop()
    {
        ShopScreen.SetActive(false);
        playerScript.canMove = true;
    }
}
