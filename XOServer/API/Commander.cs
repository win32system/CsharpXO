using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XOServer.Authentification;
using XOServer.Sessions;

namespace XOServer.API
{
    public class Commander
    {
        public Commander(ConnectionList connList)
        {
            this.connList = connList;
            inviteManager = new InviteManager();
            gameManager = new GameManager(connList.RefreshBroadcast);
        }

        private ConnectionList connList;
        private InviteManager inviteManager;
        private GameManager gameManager;

        public void Dispatch(string command, Client wanter)
        {
            string modifier = command.Remove(command.IndexOf(':'));
            command = command.Replace(modifier + ":", "");
            string[] args = command.Split(',');

            switch (modifier)
            {
                case "invite":
                    {
                        inviteManager.Invite(wanter, connList.GetByName(args[0]));
                        break;
                    }
                case "inviteYes":
                    {
                        inviteManager.InviteYes(connList.GetByName(args[0]), connList.GetByName(args[1]));
                        gameManager.SendInitPackage(connList.GetByName(args[0]), connList.GetByName(args[1]));
                        connList.RefreshBroadcast();
                        break;
                    }
                case "inviteNo":
                    {
                        inviteManager.InviteNo(connList.GetByName(args[0]));
                        break;
                    }
                case "game":
                    {
                        gameManager.Play(args[0], args[1]);
                        break;
                    }
                case "logout":
                    {
                        connList.RemoveUser(wanter);
                        wanter.Dispose();
                        break;
                    }

                default:
                    break;
            }
        }
    }
}
