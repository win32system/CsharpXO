using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using XOServer.DAO;
using XOServer.DAO.Model;

namespace XOServer.Authentification
{
    public static class AuthManager
    {
        public static string ConnectionProcessing(TcpClient socket, string command, ConnectionList connList)
        {
            var modifier = command.Remove(command.IndexOf(":"));
            var args = command.Replace(modifier + ":", "").Split(',');
            PlayerDAO_XML dao = new PlayerDAO_XML();
            string result = null;
            switch (modifier)
            {
                case "reg":
                    {
                        bool exist = dao.Read().Any(p => p.Name.Equals(args[0]));
                        StreamWriter writer = new StreamWriter(socket.GetStream());
                        if (!exist)
                        {
                            dao.Create(new Player() { Name = args[0], Password = args[1], WinRate = 0.0f });
                            result = args[0];
                            writer.WriteLine("reg:yes");
                            writer.Flush();
                        }
                        else
                        {
                            writer.WriteLine("reg:no");
                            writer.Flush();
                        }
                        
                        break;
                    }
                case "auth":
                    {
                        bool exist = dao.Read().Any(p => p.Name.Equals(args[0])) ;
                        if (exist) exist = !connList.Any(p => p.Name.Equals(args[0]));
                        StreamWriter writer = new StreamWriter(socket.GetStream());
                        if (exist)
                        {
                            result = args[0];
                            writer.WriteLine("auth:yes");
                            writer.Flush();
                        }
                        else
                        {
                            writer.WriteLine("auth:no");
                            writer.Flush();
                        }
                        break;
                    }
            }
            return result;
        }

    }
}
