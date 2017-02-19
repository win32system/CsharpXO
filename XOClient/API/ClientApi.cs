using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace XOClient.API
{
    public class ClientApi
    {
        public ClientApi(EventHandler FreePlayersHandler, EventHandler InviteHandler, 
            EventHandler SuccessHandler, Action<bool> Auth, Action<bool> Reg)
        {
            Client = new TcpClient("localhost", Port);

            FreePlayresListChanged += FreePlayersHandler;
            InviteOccur += InviteHandler;
            SuccessOccur += SuccessHandler;
            Authentification += Auth;
            Registration += Reg;

            threadListener = new Thread(new ThreadStart(ListenLoop));
            threadListener.Start();
        }


        private const int Port = 8888;
        private Thread threadListener;
        private NetworkStream ns;
        public EventHandler FreePlayresListChanged;
        public EventHandler InviteOccur;
        public EventHandler SuccessOccur;
        private Action<bool> Authentification;
        private Action<bool> Registration;
        public TcpClient Client { get; private set; }


        private void ListenLoop()
        {
            ns = Client.GetStream();
            StreamReader sr = new StreamReader(ns);

            while (true)
            {
                if (ns.DataAvailable)
                {
                    string message = sr.ReadLine();
                    var modifier = message.Remove(message.IndexOf(":"));
                    var args = message.Replace(modifier + ":", "").Split(',');

                    switch (modifier)
                    {
                        case "broadcast":
                            {
                                FreePlayresListChanged(args, null);
                                break;
                            }
                        case "invite":
                            {
                                InviteOccur(args[0], null);
                                break;
                            }
                        case "success":
                            {
                                SuccessOccur(args, null);
                                break;
                            }
                        case "auth":
                            {
                                if (args[0].Equals("yes"))
                                {
                                    Authentification(true);
                                }
                                else
                                {
                                    Authentification(false);
                                }
                                break;
                            }
                        case "reg":
                            {
                                if (args[0].Equals("yes"))
                                {
                                    Registration(true);
                                }

                                else
                                {
                                    Registration(false);
                                }
                                break;
                            }
                    }
                }
                //Thread.Sleep(20);
            }
        }


        public void SendLogout(string message)
        {
            SendInvite(message);
        }

        public void SendInviteResponse(string response)
        {
            SendInvite(response);
        }

        public void SendInviteRequest(string response)
        {
            SendInvite(response);
        }

        public void SendAuth(string name, string password)
        {
            SendInvite("auth:" + name + "," + password);
        }

        public void SendReg(string name, string password)
        {
            SendInvite("reg:" + name + "," + password);
        }

        private void SendInvite(string response)
        {
            while (ns == null) { }

            StreamWriter sw = new StreamWriter(ns);
            sw.WriteLine(response);
            sw.Flush();
        }

    }
}
