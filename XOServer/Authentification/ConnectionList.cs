using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XOServer.Authentification
{
    public class ConnectionList
    {
     
        public Client this[int index]
        {
            set
            {
                this.userList[index] = value;
            }
            get
            {
                return userList[index];
            }
        }
     

        private List<Client> userList;
        private object locker;
        public int Count { get { return userList.Count; } }

        public ConnectionList(object locker)
        {
            userList = new List<Client>();
            this.locker = locker;
        }

        public bool IsEmpty()
        {
            return userList.Count == 0;
        }

        public Client GetByName(string name)
        {
            IEnumerable<Client> client = null;
            client = from cl in userList
                     where cl.Name.Equals(name)
                     select cl;

            return client.First();
        }

        public void AddUser(Client user)
        {
            lock (locker)
            {
                userList.Add(user);
            }
            BroadcastSend();
        }

        public void RemoveUser(Client user)
        {
            lock (locker)
            {
                userList.Remove(user);
            }
            BroadcastSend();
        }

        public void RemoveUsers(IEnumerable<Client> users)
        {
            lock (locker)
            {
                foreach (var item in users)
                {
                    userList.Remove(item);
                }
            }
            BroadcastSend();
        }

        private void BroadcastSend()
        {
            string freeUsers = "broadcast:";
            foreach (var item in userList)
            {
                if (!item.IsInGame) freeUsers += item.Name + ",";
            }
            freeUsers = freeUsers.Remove(freeUsers.Length - 1);

            foreach (var item in userList)
            {
                if (!item.IsInGame)
                {
                    StreamWriter sw = new StreamWriter(item.User.GetStream());
                    sw.WriteLine(freeUsers);
                    sw.Flush();
                }
            }
        }

        public void RefreshBroadcast()
        {
            BroadcastSend();
        }

        public bool Any(Func<Client, bool> predicate)
        {
            return this.userList.Any(predicate);
        }

    }
}
