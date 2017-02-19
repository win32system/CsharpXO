using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using XOServer.Authentification;
using XOServer.Sessions;


namespace XOServer.API
{
    public class ServerApi
    {
        private const int Port = 8888;
        private TcpListener serverListener;
        private Thread threadConnection;
        private ConnectionList connList;
        private RequestLoop requestLoop;

        public ServerApi()
        {
            try {
                connList = new ConnectionList(this);
                requestLoop = new RequestLoop(connList, this);
                serverListener = new TcpListener(IPAddress.Parse("127.0.0.1"), Port);
                threadConnection = new Thread(new ThreadStart(ConnectionLoop));
                threadConnection.Start();
            }
            catch (Exception ex)
            {
                Disconect();
            }
        }
        public void Disconect()
        { 
            serverListener.Stop();
        }
        private void ConnectionLoop()
        {
            serverListener.Start();

            while (true)
            {
                var connectedClient = serverListener.AcceptTcpClient();
                var client = GetClient(connectedClient);
                if (client != null) connList.AddUser(client);
                Thread.Sleep(20); //+++
            }
        }  

        private Client GetClient(TcpClient clientSocket)
        {
            StreamReader sr = new StreamReader(clientSocket.GetStream());
            string name = String.Empty;
            Client result = null;

            while (true)
            {
                if (clientSocket.GetStream().DataAvailable)
                {
                    name = AuthManager.ConnectionProcessing(clientSocket, sr.ReadLine(), connList);
                    break;
                }
                Thread.Sleep(20);
            }

            if (name != null)
            {
                result = new Client(name, clientSocket);
            }

            return result;
        }

    }
}
