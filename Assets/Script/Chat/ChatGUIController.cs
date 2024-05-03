using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Configuration;
using System.Threading;

public class ChatGUIController : MonoBehaviour
{
    [SerializeField] TMP_InputField inputField;
    [SerializeField] TextMeshProUGUI chatLog;
    [SerializeField] GameObject parent;
    [SerializeField] string userName;
    [SerializeField] string userIP;
    [SerializeField] int serverPort;
    public bool chatting = false;
    IPEndPoint ep;

    UdpClient client;
    Thread chatListener;
    bool start = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void InitChat(string name,string ip,int port)
    {
        userName = name;
        userIP = ip;
        serverPort = port;

        client = new UdpClient();
        ep = new IPEndPoint(IPAddress.Parse(userIP), serverPort);
        client.Connect(ep);

        chatListener = new Thread(ListenToPort);
        chatListener.Start();
        chatLog.text = "";
        inputField.onDeselect.AddListener(ExitChat);
        inputField.onSubmit.AddListener(PushMessageToServer);
        start = true;

    }

    private void FixedUpdate()
    {
        UpdateChatLogGUI();
    }

    public void EnterChat()
    {
        chatting = true;
        inputField.ActivateInputField();
    }

    public void ExitChat(string text)
    {
        chatting = false;
        inputField.DeactivateInputField();
        
    }
    public void ExitChat()
    {
        inputField.DeactivateInputField();
        chatting = false;

    }

    public void PushMessageToServer(string message)
    {
        string messageWithSender = userName + ":" + message;
        byte[] encodedMessage = Encoding.ASCII.GetBytes(messageWithSender);
        client.Send(encodedMessage,encodedMessage.Length);
        inputField.SetTextWithoutNotify("");
        ExitChat();
    }

    string chatLogText = "";
    public void PushMessageToChatLog(string message)
    {
        chatLogText += message + "\n";
    }
    public void UpdateChatLogGUI()
    {
        chatLog.text = chatLogText;
    }

    // Update is called once per frame
    void Update()
    {
        if (!start)
            return;
        if (Input.GetKeyDown(KeyCode.Return) && !chatting)
            EnterChat();
        if (chatting && Input.GetKeyDown(KeyCode.Escape))
            ExitChat(null);  
    }

    void ListenToPort()
    {

        Debug.Log("Listening");
        while (true)
        {
            byte[] receivedData = client.Receive(ref ep);
            string message = Encoding.ASCII.GetString(receivedData);
            PushMessageToChatLog(message);
        }
    }
}
