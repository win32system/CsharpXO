namespace XOClient.UI
{
    partial class NameDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonAuth = new System.Windows.Forms.Button();
            this.L_EnterNickname = new System.Windows.Forms.Label();
            this.labelPassword = new System.Windows.Forms.Label();
            this.buttonReg = new System.Windows.Forms.Button();
            this.TB_Nickname = new System.Windows.Forms.TextBox();
            this.TB_Password = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // buttonAuth
            // 
            this.buttonAuth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAuth.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold);
            this.buttonAuth.Location = new System.Drawing.Point(174, 86);
            this.buttonAuth.Name = "buttonAuth";
            this.buttonAuth.Size = new System.Drawing.Size(136, 31);
            this.buttonAuth.TabIndex = 3;
            this.buttonAuth.Text = "&Authorization";
            this.buttonAuth.UseVisualStyleBackColor = true;
            this.buttonAuth.Click += new System.EventHandler(this.OnAuth_Click);
            // 
            // L_EnterNickname
            // 
            this.L_EnterNickname.Dock = System.Windows.Forms.DockStyle.Top;
            this.L_EnterNickname.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F);
            this.L_EnterNickname.Location = new System.Drawing.Point(0, 0);
            this.L_EnterNickname.Margin = new System.Windows.Forms.Padding(3);
            this.L_EnterNickname.Name = "L_EnterNickname";
            this.L_EnterNickname.Padding = new System.Windows.Forms.Padding(10, 5, 0, 0);
            this.L_EnterNickname.Size = new System.Drawing.Size(328, 34);
            this.L_EnterNickname.TabIndex = 1;
            this.L_EnterNickname.Text = "Name";
            // 
            // labelPassword
            // 
            this.labelPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F);
            this.labelPassword.Location = new System.Drawing.Point(0, 40);
            this.labelPassword.Margin = new System.Windows.Forms.Padding(3);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Padding = new System.Windows.Forms.Padding(10, 5, 0, 0);
            this.labelPassword.Size = new System.Drawing.Size(328, 30);
            this.labelPassword.TabIndex = 3;
            this.labelPassword.Text = "Password";
            // 
            // buttonReg
            // 
            this.buttonReg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonReg.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold);
            this.buttonReg.Location = new System.Drawing.Point(12, 86);
            this.buttonReg.Name = "buttonReg";
            this.buttonReg.Size = new System.Drawing.Size(133, 31);
            this.buttonReg.TabIndex = 4;
            this.buttonReg.Text = "&Registration";
            this.buttonReg.UseVisualStyleBackColor = true;
            this.buttonReg.Click += new System.EventHandler(this.OnReg_Click);
            // 
            // TB_Nickname
            // 
            this.TB_Nickname.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TB_Nickname.Location = new System.Drawing.Point(122, 3);
            this.TB_Nickname.Name = "TB_Nickname";
            this.TB_Nickname.Size = new System.Drawing.Size(188, 31);
            this.TB_Nickname.TabIndex = 1;
            this.TB_Nickname.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TB_Nickname_KeyPress);
            // 
            // TB_Password
            // 
            this.TB_Password.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TB_Password.Location = new System.Drawing.Point(122, 40);
            this.TB_Password.Name = "TB_Password";
            this.TB_Password.PasswordChar = '*';
            this.TB_Password.Size = new System.Drawing.Size(188, 31);
            this.TB_Password.TabIndex = 2;
            this.TB_Password.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TB_Nickname_KeyPress);
            // 
            // NameDialog
            // 
            this.AcceptButton = this.buttonAuth;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(328, 129);
            this.Controls.Add(this.TB_Password);
            this.Controls.Add(this.TB_Nickname);
            this.Controls.Add(this.buttonReg);
            this.Controls.Add(this.labelPassword);
            this.Controls.Add(this.L_EnterNickname);
            this.Controls.Add(this.buttonAuth);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NameDialog";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Login";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonAuth;
        private System.Windows.Forms.Label L_EnterNickname;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.Button buttonReg;
        private System.Windows.Forms.TextBox TB_Nickname;
        private System.Windows.Forms.TextBox TB_Password;
    }
}