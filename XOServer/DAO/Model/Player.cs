using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XOServer.DAO.Model
{
    public class Player
    {
        public string Name { set; get; }
        public string Password { set; get; }
        public float WinRate { set; get; }

        public void Copy(Player p)
        {
            this.Name = p.Name;
            this.Password = p.Password;
            this.WinRate = p.WinRate;
        }

        public override string ToString()
        {
            return String.Format("name : {0} password : {1} win rate : {2}", Name, Password, WinRate);
        }
    }
}
