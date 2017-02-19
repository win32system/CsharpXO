using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace XOServer.Authentification
{
    public class Client : IDisposable
    {
        public string Name { get; set; }
        public TcpClient User { get; set; }
        public bool IsInGame { set; get; }

        public Client(string name, TcpClient user)
        {
            Name = name;
            User = user;
        }

        public void Dispose()
        {
            User.Close();
        }
    }
}
