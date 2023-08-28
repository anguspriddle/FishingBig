using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndGameManager : MonoBehaviour
{
    public TMP_Text GameoverScore;

    // Start is called before the first frame update
    void Start()
    {
        int playerScore = PlayerPrefs.GetInt("Score", 0); // Get the player's score from PlayerPrefs
        GameoverScore.text = "Game Over! You Scored: " + "\n" + playerScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
