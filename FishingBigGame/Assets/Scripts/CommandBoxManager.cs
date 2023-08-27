using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System.Collections.Generic;

public class CommandBoxManager : MonoBehaviour
{
    public TMP_InputField commandInput;
    public TextMeshProUGUI commandBoxText;
    public Player playerScript;
    public FishingManager FishingManager;
    public ShopManager ShopManager;
    public GameObject ShopScreen;


    private List<string> commandHistory = new List<string>();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            OnCommandEntered(commandInput.text);
            ClearCommandInput();
            ScrollToBottom();
        }
        
    }

    private void OnCommandEntered(string command)
    {
        if (!string.IsNullOrEmpty(command))
        {
            string lowercaseCommand = command.ToLower(); // Convert input to lowercase
            doCommand(lowercaseCommand);
            UpdateCommandHistory();
        }
    }

    public void doCommand(string command)
    {
        string[] commandWords = command.Split(' '); // Split the command into separate words
        if (commandWords[0] == "fish")
        {
            FishingManager.StartFishing();
        }
        else if (commandWords[0] == "shop" && playerScript.shopOpen == true)
        {
            playerScript.canMove = false;
            ShopScreen.SetActive(true);
        }
        else if (commandWords[0] == "shop" && playerScript.shopOpen == false) {
            commandHistory.Add("-" + ' ' + "Not in shop area!");
            commandBoxText.text = string.Join("\n", commandHistory);
        }
        else if (commandWords[0] == "rest" && playerScript.restArea == true){
            playerScript.isResting = true;
        }
        else if (commandWords[0] == "rest" && playerScript.restArea == false)
        {
            commandHistory.Add("-" + ' ' + "Not in rest area!");
            commandBoxText.text = string.Join("\n", commandHistory);
        }


    }


    private void RotatePlayer(float angle)
    {
        Vector3 currentEulerAngles = playerScript.transform.eulerAngles;
        playerScript.transform.eulerAngles = new Vector3(currentEulerAngles.x, angle, currentEulerAngles.z);
        Debug.Log("Player rotated to angle: " + angle);
    }

    private void UpdateCommandHistory()
    {
        commandHistory.Add(">" + ' ' + commandInput.text);
        commandBoxText.text = string.Join("\n", commandHistory);
    }

    private void ClearCommandInput()
    {
        commandInput.text = string.Empty;
        EventSystem.current.SetSelectedGameObject(commandInput.gameObject);
    }

    private void ScrollToBottom()
    {
        Canvas.ForceUpdateCanvases(); // Ensure the UI updates before scrolling

        // Get the scroll rect component
        ScrollRect scrollRect = commandBoxText.GetComponentInParent<ScrollRect>();

        // Set the vertical normalized position to fully scrolled down
        scrollRect.verticalNormalizedPosition = 0f;
    }
    public void AddBiomeInfoToHistory(string biomeInfo)
    {
        commandHistory.Add("- " + biomeInfo);
        commandBoxText.text = string.Join("\n", commandHistory);
    }
}
