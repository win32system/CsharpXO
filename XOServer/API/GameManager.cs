using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XOServer.Authentification;
using XOServer.RealJope;
using XOServer.Sessions;
using System.IO;
using System.Net.Sockets;
using Core;

namespace XOServer.API
{
    public class GameManager
    {
        public GameManager(Action broadcast)
        {
            gameRooms = new List<GameRoom>();
            Broadcast += broadcast;
        }

        private List<GameRoom> gameRooms;
        private event Action Broadcast;

        public GameRoom GetByNames(string key)
        {
            return gameRooms.First(gRoom =>
            {
                return gRoom.Key.Equals(key);
            });
        }

        public void Play(string key, string packetString)
        {
            var room = GetByNames(key);
            var packet = XmlPacketDecoder.Decode(packetString);

            PacketUsage(packet, room);
        }

        private void PacketUsage(TTTPacket packet, GameRoom room)
        {
            bool isGameOver;
            if (room.game.IsWrongStep(packet.ButtonNumber - 1))
            {
                return;
            }
            room.Turn += 1;

            room.game.Turn(packet.ButtonNumber - 1, packet.Unit);
            isGameOver = room.game.IsGameOver();
            if (isGameOver)
            {
                packet.GameResult = room.game.Result;
            }
            packet.Matrix = room.game.GetMatrix();

            NetworkStream ns1, ns2;
            if (room.Turn == 1)
            {
                ns1 = room.player1.User.GetStream();
                ns2 = room.player2.User.GetStream();
            }
            else
            {
                ns1 = room.player2.User.GetStream();
                ns2 = room.player1.User.GetStream();
            }

            StreamWriter sw = new StreamWriter(ns2);
            sw.WriteLine(XmlPacketDecoder.Encode(packet));
            sw.Flush();

            packet.PlayerTurn = ~packet.PlayerTurn;

            sw = new StreamWriter(ns1);
            sw.WriteLine(XmlPacketDecoder.Encode(packet));
            sw.Flush();
            if (isGameOver)
            {
                RemoveFromGame(room.player1, room.player2);
                gameRooms.Remove(room);
                Broadcast();
            }
        }

        public void SendInitPackage(Client wanter, Client wanted)
        {
            AddToGame(wanter, wanted);
            TTTPacket packet = new TTTPacket(PlayerTurn.Turn, "X", 0, null, null);
            StreamWriter sw = new StreamWriter(wanter.User.GetStream());
            sw.WriteLine(XmlPacketDecoder.Encode(packet));
            sw.Flush();

            packet = new TTTPacket(PlayerTurn.Wait, "O", 0, null, null);
            sw = new StreamWriter(wanted.User.GetStream());
            sw.WriteLine(XmlPacketDecoder.Encode(packet));
            sw.Flush();

            gameRooms.Add(new GameRoom(wanter, wanted, GameType.XO));
        }

        private void AddToGame(Client wanter, Client wanted)
        {
            wanted.IsInGame = true;
            wanter.IsInGame = true;
        }

        private void RemoveFromGame(Client wanter, Client wanted)
        {
            wanted.IsInGame = false;
            wanter.IsInGame = false;
        }
    }
}
