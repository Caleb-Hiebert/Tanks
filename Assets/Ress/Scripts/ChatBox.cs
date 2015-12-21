using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class ChatBox : MonoBehaviour {

    public static ChatBox cb;

    public Text chatDisplay;
    public InputField input;

	void Awake () {
        ChatCore.ChatMessageReceived += OnMessage;
        cb = this;
        input.gameObject.SetActive(false);
	}

    private void OnMessage(string formattedMessage)
    {
        chatDisplay.text = ChatCore.FormattedStringAll;
    }

    public void SendChatMessage(string message)
    {
        if (message == null || message == "")
        {
            input.gameObject.SetActive(false);
            EventSystem.current.SetSelectedGameObject(null);
            return;
        }

        ChatCore.Send(message, Player.LocalPlayer);

        input.text = string.Empty;

        input.Select();
        input.ActivateInputField();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            if((input.text != "" || input.text != null) && input.gameObject.activeSelf)
            {
                SendChatMessage(input.text);
                input.text = string.Empty;
            } else
            {
                input.gameObject.SetActive(true);
                input.ActivateInputField();
            }
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            input.text = string.Empty;
            input.DeactivateInputField();
            input.gameObject.SetActive(false);
        }
    }

    void OnDestroy()
    {
        ChatCore.ChatMessageReceived -= OnMessage;
    }
}
