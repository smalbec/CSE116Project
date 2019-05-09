using System.Collections.Generic;
using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class TCPServer : MonoBehaviour
{
    public string ipAdress = "127.0.0.1";
    public int port = 13000;
    public float waitingMessagesFrequency = 5;
    public string responseMessage = "Close";
    private TcpListener server = null;
    private TcpClient client = null;
    
    private List<TcpClient> clientList  = new List<TcpClient>(); 
    public void StartServer()
    {
        IPAddress ip = IPAddress.Parse("127.0.0.1");
        server = new TcpListener(ip,port);
        server.Start();
        server.BeginAcceptTcpClient(OnServerConnect, null);
        //server.AcceptTcpClient();
        Debug.Log("Server Started: " + ipAdress + ":" + port);
        Debug.Log("Waiting for connections...");

    }

    void OnServerConnect(IAsyncResult ar)
    {
        Byte[] bytes = new Byte[256];
        String data = null;

        TcpClient tcpClient = server.EndAcceptTcpClient(ar);
        clientList.Add(tcpClient);
        server.BeginAcceptTcpClient(OnServerConnect, null);
        while(true)
        {
            data = null;

            NetworkStream stream = tcpClient.GetStream(); //network stream for reading and writing

            int i; //i initialized

            while((i = stream.Read(bytes, 0, bytes.Length))!=0) //while data is being read
            {
                data = System.Text.Encoding.ASCII.GetString(bytes, 0, i); // data converted to ascii string
                Debug.Log("Recieved from client: " + data);

                data = data.ToUpper(); //process data sent by client
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);

                stream.Write(msg, 0, msg.Length);
                Debug.Log("Sent to client: " + data);
            }
            client.Close(); //shutdown client when nothing is beng recieved
        }
    }

}
