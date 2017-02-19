using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using XOServer.Authentification;
using XOServer.Sessions;

namespace XOServer.API
{
    public class RequestLoop
    {
        private Thread threadListener;
        private object locker;
        private ConnectionList connList;
        private Commander commander;


        public RequestLoop(ConnectionList connList, object locker)
        {
            this.locker = locker;
            this.connList = connList;
            commander = new Commander(connList);
            threadListener = new Thread(new ThreadStart(ListenLoop));
            threadListener.Start();
        }

        private void ListenLoop()
        {
            while (true)
            {
                if (connList.IsEmpty())
                    continue;

                lock (locker)
                {
                    GetRequest();
                }
                Thread.Sleep(20);
            }
        }

        private void GetRequest()
        {
            for (int i = 0; i < connList.Count; i++)
            {
                if (connList[i].User.GetStream().DataAvailable)
                {
                    RequestSelection(connList[i]);                        
                }
            }
        }

        private void RequestSelection(Client wanter)
        {
            StreamReader sr = new StreamReader(wanter.User.GetStream());
            string message = sr.ReadLine();

            if (message.Contains(':'))
            {
                commander.Dispatch(message, wanter);
            }
        }

    }
}
