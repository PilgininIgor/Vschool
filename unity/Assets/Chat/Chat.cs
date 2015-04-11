using UnityEngine;
using System.Collections.Generic;

public class Chat : MonoBehaviour
{

    public GUISkin skin;
    public bool showChat = false;
    private string inputField = "";
    private bool display = true;
    private List<ChatEntry> entries = new List<ChatEntry>();
    private Vector2 scrollPosition;

    private Rect window = new Rect(50, 50, 200, 300);

    void CloseChatWindow()
    {
        showChat = false;
        inputField = "";
        entries = new List<ChatEntry>();
    }

    void FocusControl()
    {
        // We can't select it immediately because the control might not have been drawn yet.
        // Thus it is not known to the system!
        GUI.FocusControl("Chat input field");
    }

    void OnGUI()
    {
        GUI.skin = skin;

        //if (GUILayout.Button(showChat ? "Hide Chat" : "Display Chat"))
        if (GUI.Button(new Rect(Screen.width - 100, Screen.height - 30, 90, 20), showChat ? "Hide Chat" : "Display Chat"))
        {
            // Focus first element
            if (showChat)
            {
                CloseChatWindow();
            }
            else
            {
                showChat = true;
                FocusControl();
            }
        }

        if (showChat)
            window = GUI.Window(1, window, GlobalChatWindow, "Chat");
    }

    void GlobalChatWindow(int id)
    {

        var closeButtonStyle = GUI.skin.GetStyle("close_button");
        if (GUI.Button(new Rect(4, 4, closeButtonStyle.normal.background.width, closeButtonStyle.normal.background.height), "", "close_button"))
        {
            CloseChatWindow();
        }

        // Begin a scroll view. All rects are calculated automatically - 
        // it will use up any available screen space and make sure contents flow correctly.
        // This is kept small with the last two parameters to force scrollbars to appear.
        scrollPosition = GUILayout.BeginScrollView(scrollPosition);

        foreach (var entry in entries)
        {
            GUILayout.BeginHorizontal();
            if (!entry.mine)
            {
                GUILayout.FlexibleSpace();
                GUILayout.Label(entry.text, "chat_rightaligned");
            }
            else
            {
                GUILayout.Label(entry.text, "chat_leftaligned");
                GUILayout.FlexibleSpace();
            }

            GUILayout.EndHorizontal();
            GUILayout.Space(3);

        }
        // End the scrollview we began above.
        GUILayout.EndScrollView();

        if (Event.current.type == EventType.keyDown && Event.current.character == '\n' && inputField.Length > 0)
        {
            //@TODO: This should be dependent on who actually sent the message
            //var mine = entries.Count % 2 == 0;
            ApplyGlobalChatText(inputField, true);
            networkView.RPC("ApplyGlobalChatText", RPCMode.Others, inputField, false);
            inputField = "";
        }
        GUI.SetNextControlName("Chat input field");
        inputField = GUILayout.TextField(inputField);

        GUI.DragWindow();
    }

    [RPC]
    void ApplyGlobalChatText(string str, bool mine)
    {
        var entry = new ChatEntry { sender = "Not implemented", text = str, mine = mine };

        entries.Add(entry);

        if (entries.Count > 50)
            entries.RemoveAt(0);

        scrollPosition.y = 1000000;
    }

    class ChatEntry
    {
        public string sender = "";
        public string text = "";
        public bool mine = true;
    }
}
