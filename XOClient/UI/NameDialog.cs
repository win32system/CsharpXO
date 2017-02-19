using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XOClient.UI
{
    public partial class NameDialog : Form
    {
        public NameDialog()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        public string Nickname { get; private set; }
        public string Password { get; private set; }

        private void OnReg_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Nickname = TB_Nickname.Text;
            Password = TB_Password.Text;
        }

        private void OnAuth_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Yes;
            Nickname = TB_Nickname.Text;
            Password = TB_Password.Text;
        }

        private void TB_Nickname_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Back)
            {
                return;
            }
            if (Char.IsWhiteSpace(e.KeyChar) || Char.IsSymbol(e.KeyChar) || Char.IsSeparator(e.KeyChar) || Char.IsPunctuation(e.KeyChar) || (sender as TextBox).Text.Length >= 40)
            {
                e.Handled = true;
                return;
            }
        }
    }
}
