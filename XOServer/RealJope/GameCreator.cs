using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XOServer.Sessions;


namespace XOServer.RealJope
{
    public static class GameCreator
    {
        public static IGame CreateInstance(GameType gameType)
        {
            IGame game = null;

            switch (gameType)
            {
                case GameType.XO:
                    game = new XOGame();
                    break;
                default:
                    break;
            }

            return game;
        }
    }
}
