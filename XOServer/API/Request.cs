using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XOServer.Authentification;

namespace XOServer.API
{
    public class Request
    {
        public Client Wanted { get; set; }
        public Client Wanter { get; set; }

        public Request(Client wanted, Client wanter)
        {
            Wanted = wanted;
            Wanter = wanter;
        }
    }
}
