namespace MillionSimple
{
    partial class Million
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Million));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnOneKeyAddFriend = new System.Windows.Forms.Button();
            this.btnAccountManage = new System.Windows.Forms.Button();
            this.txtLoginMessage = new System.Windows.Forms.TextBox();
            this.btnAllLogin = new System.Windows.Forms.Button();
            this.btnLogin = new System.Windows.Forms.Button();
            this.lbx = new System.Windows.Forms.ListBox();
            this.cbx = new System.Windows.Forms.CheckBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(697, 380);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.cbx);
            this.tabPage1.Controls.Add(this.btnOneKeyAddFriend);
            this.tabPage1.Controls.Add(this.btnAccountManage);
            this.tabPage1.Controls.Add(this.txtLoginMessage);
            this.tabPage1.Controls.Add(this.btnAllLogin);
            this.tabPage1.Controls.Add(this.btnLogin);
            this.tabPage1.Controls.Add(this.lbx);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(689, 354);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "登陆";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnOneKeyAddFriend
            // 
            this.btnOneKeyAddFriend.Location = new System.Drawing.Point(429, 323);
            this.btnOneKeyAddFriend.Name = "btnOneKeyAddFriend";
            this.btnOneKeyAddFriend.Size = new System.Drawing.Size(110, 23);
            this.btnOneKeyAddFriend.TabIndex = 5;
            this.btnOneKeyAddFriend.Text = "一键循环加好友";
            this.btnOneKeyAddFriend.UseVisualStyleBackColor = true;
            this.btnOneKeyAddFriend.Click += new System.EventHandler(this.btnOneKeyAddFriend_Click);
            // 
            // btnAccountManage
            // 
            this.btnAccountManage.Location = new System.Drawing.Point(606, 294);
            this.btnAccountManage.Name = "btnAccountManage";
            this.btnAccountManage.Size = new System.Drawing.Size(75, 23);
            this.btnAccountManage.TabIndex = 4;
            this.btnAccountManage.Text = "小号管理";
            this.btnAccountManage.UseVisualStyleBackColor = true;
            this.btnAccountManage.Click += new System.EventHandler(this.btnAccountManage_Click);
            // 
            // txtLoginMessage
            // 
            this.txtLoginMessage.Location = new System.Drawing.Point(241, 6);
            this.txtLoginMessage.Multiline = true;
            this.txtLoginMessage.Name = "txtLoginMessage";
            this.txtLoginMessage.ReadOnly = true;
            this.txtLoginMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLoginMessage.Size = new System.Drawing.Size(440, 275);
            this.txtLoginMessage.TabIndex = 3;
            // 
            // btnAllLogin
            // 
            this.btnAllLogin.Location = new System.Drawing.Point(545, 323);
            this.btnAllLogin.Name = "btnAllLogin";
            this.btnAllLogin.Size = new System.Drawing.Size(136, 23);
            this.btnAllLogin.TabIndex = 2;
            this.btnAllLogin.Text = "全部登陆并领取礼包";
            this.btnAllLogin.UseVisualStyleBackColor = true;
            this.btnAllLogin.Click += new System.EventHandler(this.btnAllLogin_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(343, 290);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(75, 23);
            this.btnLogin.TabIndex = 1;
            this.btnLogin.Text = "登陆";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // lbx
            // 
            this.lbx.FormattingEnabled = true;
            this.lbx.ItemHeight = 12;
            this.lbx.Location = new System.Drawing.Point(8, 6);
            this.lbx.Name = "lbx";
            this.lbx.Size = new System.Drawing.Size(206, 340);
            this.lbx.TabIndex = 0;
            // 
            // cbx
            // 
            this.cbx.AutoSize = true;
            this.cbx.Location = new System.Drawing.Point(241, 294);
            this.cbx.Name = "cbx";
            this.cbx.Size = new System.Drawing.Size(96, 16);
            this.cbx.TabIndex = 6;
            this.cbx.Text = "大号加密模式";
            this.cbx.UseVisualStyleBackColor = true;
            // 
            // Million
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(697, 380);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Million";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Million_FormClosing);
            this.Load += new System.EventHandler(this.Million_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox txtLoginMessage;
        private System.Windows.Forms.Button btnAllLogin;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.ListBox lbx;
        private System.Windows.Forms.Button btnAccountManage;
        private System.Windows.Forms.Button btnOneKeyAddFriend;
        private System.Windows.Forms.CheckBox cbx;
    }
}