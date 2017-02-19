using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XOServer.Authentification;
using XOServer.Sessions;

namespace XOServer.RealJope
{
    public class GameRoom
    {
        public GameRoom(Client cl1, Client cl2, GameType type)
        {
            player1 = cl1;
            player2 = cl2;
            game = GameCreator.CreateInstance(type);
        }

        public Client player1;
        public Client player2;

        private int turn;
        public int Turn
        {
            get
            {
                return turn % 2;
            }
            set
            {
                turn = value;
            }
        }

        public string Key {
            get
            {
                return player1.Name + player2.Name;
            }
        }

        public IGame game;

    }
}
