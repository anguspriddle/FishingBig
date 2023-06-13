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

        if (commandWords[0] == "move")
        {
            if (commandWords.Length > 1)
            {
                string targetGridSpace = commandWords[1]; // Get the additional input after "move"
                playerScript.PlayerMove(targetGridSpace); // Pass the target grid space to the PlayerMove() function
            }
            else
            {
                Debug.Log("No target grid space provided."); // Handle case when no target grid space is provided
            }
        }
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
