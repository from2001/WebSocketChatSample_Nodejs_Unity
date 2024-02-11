using System;
using UnityEngine;
using WebSocketSharp;

public class WebSocketChatClient : MonoBehaviour
{
    private WebSocket ws;

    void Start()
    {
        // Connect to the server
        ws = new WebSocket("ws://localhost:8080/");

        ws.OnMessage += (sender, e) =>
        {
            if (e.IsText)
            {
                Debug.Log("Message from the server: " + e.Data);
            }
            else if (e.IsBinary)
            {
                // Decode the byte array to string
                string decodedMessage = System.Text.Encoding.UTF8.GetString(e.RawData);
                Debug.Log("Message from the server: " + decodedMessage);
            }
        };

        ws.Connect();

        // Send a message to the server
        ws.Send("Hello, server! I'm an Unity client.");
    }

    void OnDestroy()
    {
        if (ws != null)
        {
            ws.Close();
            ws = null;
        }
    }

    // Send a message to the server
    public void SendMessageToServer(string message)
    {
        if (ws.IsAlive)
        {
            ws.Send(message);
        }
    }
}

