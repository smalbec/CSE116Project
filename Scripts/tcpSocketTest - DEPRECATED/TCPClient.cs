using System.Collections.Generic;
using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class TCPClient : MonoBehaviour
{
    private TCPClient client;
    public string ipAdress = "127.0.0.1";
    public int port = 13000;
    public void setupClient()
    {
        TcpClient client = new TcpClient();
        client.Connect(ipAdress, port);
        Debug.Log("Client Started!");
        if(client.Connected)
        {
            Debug.Log("Client Connected to Server!");
        }

        NetworkStream stream = client.GetStream();

        string sendMsg = "message yeet message";
        byte[] msg = Encoding.ASCII.GetBytes(sendMsg);

        stream.Write(msg,0,msg.Length);
        Debug.Log("Message sent to Server: " + sendMsg);
    }
    public void Connect(string Server, String message)
    {
        Int32 port = 13000;
        TcpClient client = new TcpClient(Server,port); //client object is made

        Debug.Log("Client Started!");

        Byte[] data = System.Text.Encoding.ASCII.GetBytes(message); //passed message gets put into byte array

        NetworkStream stream = client.GetStream(); // client stream for reading/writing
         // Send the message to the connected TcpServer. 
        stream.Write(data, 0, data.Length);
        Debug.Log("Sent: " + message);   

        data = new Byte[256]; //buffer used to store response bytes
        String responseData = String.Empty; //string used to store the response ascii
        // Read the first batch of the TcpServer response bytes.
        Int32 bytes = stream.Read(data, 0, data.Length);
        responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
        Debug.Log("Recieved: " + responseData);         

        // Close everything.
        stream.Close();         
        client.Close(); 
    }
}