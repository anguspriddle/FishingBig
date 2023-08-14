using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public int energyThreshold = 0; // Set the energy threshold for game over
    public string gameOverSceneName = "GameOverState"; // Name of the game over scene

    public Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        if (player.Energy <= energyThreshold)
        {
            // Load the game over scene
            SceneManager.LoadScene(gameOverSceneName);

            // Optionally, you can pause the game by setting Time.timeScale to 0
            Time.timeScale = 0;
        }
    }
    
        public void RestartGame()
        {
        // Reset the time scale to normal
        Time.timeScale = 1;

        // Reload the current scene (the main game scene)
        SceneManager.LoadScene("MainGame");
     }
    // Rest of the script...
}
