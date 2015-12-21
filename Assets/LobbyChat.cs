using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class LobbyChat : MonoBehaviour {

    public Text chatDisplay;
    public InputField typingBox;

	void Start () {
        ChatCore.ChatMessageReceived += OnMessageReceived;
	}

    private void OnMessageReceived(string formattedMessage)
    {
        string chatString = string.Empty;

        foreach (var item in ChatCore.ChatMessages)
        {
            chatString += item.Formatted + "\n";
        }

        chatDisplay.text = chatString;
    }

    void OnDestroy()
    {
        ChatCore.ChatMessageReceived -= OnMessageReceived;
    }

    public void Send(string message)
    {
        if (message == null || message == "")
            return;

        ChatCore.Send(message, Player.LocalPlayer);
        typingBox.text = "";
        typingBox.Select();
        typingBox.ActivateInputField();
    }
}
