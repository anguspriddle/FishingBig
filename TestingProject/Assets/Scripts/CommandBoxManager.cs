using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System.Collections.Generic;

public class CommandBoxManager : MonoBehaviour
{
    public TMP_InputField commandInput;
    public TextMeshProUGUI commandBoxText;

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
            ProcessCommand(command);
            UpdateCommandHistory();
        }
    }

    private void ProcessCommand(string command)
    {
        // Process the command here, perform game actions accordingly
        // This is just a placeholder example
        Debug.Log("Command entered: " + command);
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
