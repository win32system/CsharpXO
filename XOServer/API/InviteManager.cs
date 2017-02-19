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
    public class InviteManager
    {
        public InviteManager()
        {
            requestPool = new RequestPool();
        }

        private RequestPool requestPool;


        public void Invite(Client wanter, Client wanted)
        {
            if (wanted != null)
            {
                bool exists = false;

                if (requestPool.Item.Count != 0)
                {
                    exists = requestPool.Item.Any(r =>
                    {
                        return r.Wanted.Name.Equals(wanted.Name);
                    });
                }

                if (!exists)
                {
                    requestPool.Add(new Request(wanted, wanter));
                }
            }
        }

        public void InviteYes(Client wanter, Client wanted)
        {
            SessionResult(wanter.Name, "yes," + wanter.Name + wanted.Name);
            Thread.Sleep(1000);
        }

        public void InviteNo(Client wanter)
        {
            SessionResult(wanter.Name, "no");
        }

        private void SessionResult(string wantedName, string modifier)
        {
            Request req = requestPool.GetByWantedName(wantedName);
            requestPool.SendResponse(req, modifier);
            requestPool.Item.Remove(req);
        }
    }
}
