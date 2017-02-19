using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XOServer.DAO.Model;

namespace XOServer.DAO
{
    public class PlayerDAO_XML
    {
        private IOManager io;

        public PlayerDAO_XML()
        {
            io = new IOManager();
        }

        public void Create(Player player)
        {
            List<Player> players = io.Deserialize().ToList();
            players.Add(player);
            io.Serialize(players);
        }

        public IEnumerable<Player> Read()
        {
            return io.Deserialize().ToList();
        }

        public void Update(Player player)
        {
            List<Player> players = io.Deserialize().ToList();
            players.First((p) => p.ToString().Equals(player.ToString())).Copy(player);
            io.Serialize(players);
        }

        public void Delete(Player player)
        {
            List<Player> players = io.Deserialize().ToList();
            players.Remove(players.First((p) => p.ToString().Equals(player.ToString())));
            io.Serialize(players);
        }
    }
}
