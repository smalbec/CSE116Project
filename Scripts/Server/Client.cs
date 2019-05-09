using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class Client : MonoBehaviour
{
    public Action<Client> OnConnected = delegate { };
    public Action<Client> OnDisconnected = delegate { };
    public Action<string> OnLog = delegate { };


    public bool IsConnected
    {
        get { return socketConnection != null && socketConnection.Connected; }
    }

    public string IPAddress = "localhost";
    public int Port = 8052;

    private TcpClient socketConnection;
    private Thread clientReceiveThread;
    private NetworkStream stream;
    private bool running;
	
    public void ConnectToTcpServer()
    {


        clientReceiveThread = new Thread(new ThreadStart(ListenForData));
        clientReceiveThread.IsBackground = true;
        clientReceiveThread.Start();

    }
   
    private void ListenForData()
    {

        socketConnection = new TcpClient(IPAddress, Port);
        OnConnected(this);
        Byte[] bytes = new Byte[1024];
        running = true;
        while (running)
        {
            // Get a stream object for reading
            using (stream = socketConnection.GetStream())
            {
                int length;
                // Read incoming stream into byte array. 					
                while (running && stream.CanRead)
                {
                    length = stream.Read(bytes, 0, bytes.Length);
                    if (length != 0)
                    {
                        var incomingData = new byte[length];
                        Array.Copy(bytes, 0, incomingData, 0, length);
                        // Convert byte array to string message. 						
                        string serverJson = Encoding.ASCII.GetString(incomingData);


                    }
                }
            }
        }
        socketConnection.Close();
        OnDisconnected(this);

    }

    public void CloseConnection()
    {
        running = false;
    }

}