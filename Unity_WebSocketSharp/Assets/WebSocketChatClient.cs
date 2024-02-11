using System;
using UnityEngine;
using WebSocketSharp;

public class WebSocketChatClient : MonoBehaviour
{
    private WebSocket ws;

    void Start()
    {
        // WebSocketサーバーに接続
        ws = new WebSocket("ws://localhost:8080/");

        ws.OnMessage += (sender, e) =>
        {
            if (e.IsText)
            {
                // テキストメッセージの場合、そのまま使用
                Debug.Log("サーバーからのメッセージ: " + e.Data);
            }
            else if (e.IsBinary)
            {
                // バイナリメッセージの場合、UTF-8文字列にデコード
                string decodedMessage = System.Text.Encoding.UTF8.GetString(e.RawData);
                Debug.Log("サーバーからのデコードされたメッセージ: " + decodedMessage);
            }
        };

        ws.Connect();

        // サーバーにメッセージを送信
        ws.Send("Unityクライアントからこんにちは！");
    }

    void OnDestroy()
    {
        if (ws != null)
        {
            ws.Close();
            ws = null;
        }
    }

    // UIなどから呼び出すためのメッセージ送信メソッド
    public void SendMessageToServer(string message)
    {
        if (ws.IsAlive)
        {
            ws.Send(message);
        }
    }
}

