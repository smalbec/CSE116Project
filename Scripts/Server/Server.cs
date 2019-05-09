using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

namespace wig {
    public class Server : MonoBehaviour
    {
        [Serializable]
        public class ClientData
        {
            public static int MAX_ID;

            public int ID;
            public string Name;
        }

        public class ConnectedClient
        {
            public ClientData ClientData;
            public TcpClient Client;

            public ConnectedClient(ClientData data, TcpClient client)
            {
                ClientData = data;
                Client = client;
            }
        }


        public bool IsConnected
        {
            get { return tcpListenerThread != null && tcpListenerThread.IsAlive; }
        }

        public string IPAddress = "127.0.0.1";
        public int Port = 8052;
        private TcpListener tcpListener;
        private Thread tcpListenerThread;

        private List<ConnectedClient> connectedClients = new List<ConnectedClient>();

        // Use this for initialization
        public void StartServer()
        {
            // Start TcpServer background thread 		
            tcpListenerThread = new Thread(ListenForIncomingRequests);
            tcpListenerThread.IsBackground = true;
            tcpListenerThread.Start();
            Debug.Log("on jah");
        }

        private void ListenForIncomingRequests()
        {

            // Create listener on localhost port 8052. 			
            tcpListener = new TcpListener(System.Net.IPAddress.Any, Port);
            tcpListener.Start();

            ThreadPool.QueueUserWorkItem(ListenerWorker, null);



        }

        private void ListenerWorker(object token)
        {
            while (tcpListener != null)
            {
                var client = tcpListener.AcceptTcpClient();
                ThreadPool.QueueUserWorkItem(HandleClientWorker, client);
            }
        }

        private void HandleClientWorker(object token)
        {
            Byte[] bytes = new Byte[1024];
            using (TcpClient client = token as TcpClient)
            {
                ClientData data = new ClientData();
                data.ID = ++ClientData.MAX_ID;
                data.Name = "User" + data.ID;

                ConnectedClient connectedClient = new ConnectedClient(data, client);
                connectedClients.Add(connectedClient);

                using (NetworkStream stream = client.GetStream())
                {
                    int length;
                    // Read incoming stream into byte array. 						
                    while (stream.CanRead && (length = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        var incomingData = new byte[length];
                        Array.Copy(bytes, 0, incomingData, 0, length);
                        // Convert byte array to string message. 							
                        string clientMessage = Encoding.ASCII.GetString(incomingData);
                        //OnLog("Server received: " + clientMessage);


                    }
                }

            }
        }

        private void DisconnectClient(ConnectedClient connection)
        {
            connectedClients.Remove(connection);
        }

    }
}