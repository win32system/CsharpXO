using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using XOClient.API;

namespace XOClient.Sessions
{
    public class Session
    {
        public Session(TcpClient client, EventHandler InitHandler, EventHandler TurnHandler)
        {
            IsConnection = true;
            this.client = client;
            this.InitHandle += InitHandler;
            this.TurnOccured += TurnHandler;
            threadListener = new Thread(new ThreadStart(ListenLoop));
            threadListener.Start();
        }
        

        private TcpClient client;
        private StreamWriter sw;
        private Thread threadListener;
        public EventHandler InitHandle;
        public EventHandler TurnOccured;

        public bool IsConnection { set; get; }


        private void ListenLoop()
        {
            NetworkStream ns = client.GetStream();
            StreamReader sr = new StreamReader(ns);
            sw = new StreamWriter(ns);
            string xmlStr = String.Empty;

            while (true)
            {
                if (ns.DataAvailable)
                {
                    xmlStr = sr.ReadLine();
                    break;
                }
                Thread.Sleep(20);
            }

            InitHandle(XmlPacketDecoder.Decode(xmlStr), null);
            xmlStr = String.Empty;

            while (IsConnection)
            {
                if (ns.DataAvailable)
                {
                    xmlStr = sr.ReadLine();
                    TurnOccured(XmlPacketDecoder.Decode(xmlStr), null);
                }
            }
        }

        public void Send(string key, TTTPacket packet)
        {
            string xmlStr = XmlPacketDecoder.Encode(packet);
            sw.WriteLine("game:" + key + "," + xmlStr);
            sw.Flush();
        }
    }
}
