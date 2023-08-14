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
            doCommand(command);
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
        else if (commandWords[0] == "shop")
        {
            playerScript.canMove = false;
            ShopScreen.SetActive(true);
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
        ScrollRect scrollRect = commandBoxText.GetComponentInParent<ScrollRect>();
        scrollRect.normalizedPosition = new Vector2(0f, 0f);
    }
}
