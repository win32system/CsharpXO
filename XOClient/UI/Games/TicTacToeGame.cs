using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XOClient.API;
using XOClient.Sessions;
using Core;

namespace XOClient.UI.Games
{
    public partial class TicTacToeGame : Form, IDisposable
    {
        public TicTacToeGame(string key, TcpClient client)
        {
            InitializeComponent();
            this.key = key;
            buttons = this.Controls[0].Controls.OfType<Button>().ToArray();
            buttons = buttons.Reverse().ToArray();
            unit = String.Empty;
            session = new Session(client, new EventHandler(InitHandler), new EventHandler(TurnHandler));
        }


        private string key;
        private PlayerTurn playerTurn;
        private string unit;
        private Session session;
        private Button[] buttons;

        private void OnTileClick(object sender, EventArgs e)
        {
            if (unit != String.Empty)
            {
                if (PlayerTurn.Turn == playerTurn)
                {
                    Button btn = sender as Button;
                    int number = Convert.ToInt32(btn.Name.Replace("B_", ""));
                    session.Send(key, new TTTPacket(playerTurn, unit, number, null, null));
                }
            }
        }

        private void InitHandler(object sender, EventArgs e)
        {
            TTTPacket packet = sender as TTTPacket;
            playerTurn = packet.PlayerTurn;
            unit = packet.Unit;

            StatusBar_ShapeType.Text = unit;
            StatusBar_Turn.Text = playerTurn.ToString();
        }

        private void TurnHandler(object sender, EventArgs e)
        {
            TTTPacket packet = sender as TTTPacket;
            if (packet.GameResult != null)
            {
                Refresh(packet);
                MessageBox.Show(packet.GameResult, "Game over", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(() =>
                    {
                        this.Close();
                        this.session.IsConnection = false;
                    }));
                }
            }
            else
            {
                Refresh(packet);
            }
        }

        private void Refresh(TTTPacket packet)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    for (int i = 0; i < packet.Matrix.Length; i++)
                    {
                        buttons[i].Text = packet.Matrix[i];
                    }
                }));
            }
            playerTurn = packet.PlayerTurn;
            StatusBar_Turn.Text = playerTurn.ToString();
        }

        public new void Dispose()
        {
            base.Dispose();
            session = null;
        }
        
    }
}
