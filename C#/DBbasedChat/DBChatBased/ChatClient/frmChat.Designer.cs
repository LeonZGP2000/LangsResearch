namespace ChatClient
{
    partial class frmChat
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmChat));
            this.pnlMain = new System.Windows.Forms.Panel();
            this.lbClearChat = new System.Windows.Forms.LinkLabel();
            this.lbLoadHistory = new System.Windows.Forms.LinkLabel();
            this.lbFind = new System.Windows.Forms.LinkLabel();
            this.tbUserCorrespond = new System.Windows.Forms.TextBox();
            this.lblInfo = new System.Windows.Forms.Label();
            this.lblExit = new System.Windows.Forms.Label();
            this.lblSaveToFile = new System.Windows.Forms.Label();
            this.lblSendMessage = new System.Windows.Forms.Label();
            this.tbUserMessage = new System.Windows.Forms.TextBox();
            this.rtfChatContent = new System.Windows.Forms.RichTextBox();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.gbAuth = new System.Windows.Forms.GroupBox();
            this.lblRegistration = new System.Windows.Forms.Label();
            this.lblLogin = new System.Windows.Forms.Label();
            this.tbAuthPass = new System.Windows.Forms.TextBox();
            this.tbAuthLogin = new System.Windows.Forms.TextBox();
            this.cgjvgjnhvj = new System.Windows.Forms.Label();
            this.sdsdsd = new System.Windows.Forms.Label();
            this.getMessageTimer = new System.Windows.Forms.Timer(this.components);
            this.gbHistory = new System.Windows.Forms.GroupBox();
            this.treeChtHistory = new System.Windows.Forms.TreeView();
            this.lbClearHistory = new System.Windows.Forms.LinkLabel();
            this.lbFurtherPlans = new System.Windows.Forms.LinkLabel();
            this.dtPicker = new System.Windows.Forms.DateTimePicker();
            this.pnlMain.SuspendLayout();
            this.gbAuth.SuspendLayout();
            this.gbHistory.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMain.Controls.Add(this.lbClearChat);
            this.pnlMain.Controls.Add(this.lbLoadHistory);
            this.pnlMain.Controls.Add(this.lbFind);
            this.pnlMain.Controls.Add(this.tbUserCorrespond);
            this.pnlMain.Controls.Add(this.lblInfo);
            this.pnlMain.Controls.Add(this.lblExit);
            this.pnlMain.Controls.Add(this.lblSaveToFile);
            this.pnlMain.Controls.Add(this.lblSendMessage);
            this.pnlMain.Controls.Add(this.tbUserMessage);
            this.pnlMain.Controls.Add(this.rtfChatContent);
            this.pnlMain.Location = new System.Drawing.Point(12, 12);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(766, 611);
            this.pnlMain.TabIndex = 0;
            this.pnlMain.Visible = false;
            this.pnlMain.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlMain_Paint);
            // 
            // lbClearChat
            // 
            this.lbClearChat.AutoSize = true;
            this.lbClearChat.Location = new System.Drawing.Point(638, 51);
            this.lbClearChat.Name = "lbClearChat";
            this.lbClearChat.Size = new System.Drawing.Size(75, 20);
            this.lbClearChat.TabIndex = 7;
            this.lbClearChat.TabStop = true;
            this.lbClearChat.Text = "Clear chat";
            this.lbClearChat.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lbClearChat_LinkClicked);
            // 
            // lbLoadHistory
            // 
            this.lbLoadHistory.AutoSize = true;
            this.lbLoadHistory.Location = new System.Drawing.Point(439, 51);
            this.lbLoadHistory.Name = "lbLoadHistory";
            this.lbLoadHistory.Size = new System.Drawing.Size(160, 20);
            this.lbLoadHistory.TabIndex = 6;
            this.lbLoadHistory.TabStop = true;
            this.lbLoadHistory.Text = "Load history from Chat";
            this.lbLoadHistory.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lbLoadHistory_LinkClicked);
            // 
            // lbFind
            // 
            this.lbFind.ActiveLinkColor = System.Drawing.Color.Blue;
            this.lbFind.AutoSize = true;
            this.lbFind.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lbFind.Location = new System.Drawing.Point(374, 51);
            this.lbFind.Name = "lbFind";
            this.lbFind.Size = new System.Drawing.Size(39, 20);
            this.lbFind.TabIndex = 5;
            this.lbFind.TabStop = true;
            this.lbFind.Text = "Find";
            this.lbFind.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lbFind_LinkClicked);
            // 
            // tbUserCorrespond
            // 
            this.tbUserCorrespond.Location = new System.Drawing.Point(24, 48);
            this.tbUserCorrespond.Name = "tbUserCorrespond";
            this.tbUserCorrespond.Size = new System.Drawing.Size(344, 27);
            this.tbUserCorrespond.TabIndex = 4;
            this.tbUserCorrespond.Text = "Enter correspond user\'s Login...";
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Font = new System.Drawing.Font("Segoe UI", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point);
            this.lblInfo.ForeColor = System.Drawing.Color.LimeGreen;
            this.lblInfo.Location = new System.Drawing.Point(24, 25);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(94, 20);
            this.lblInfo.TabIndex = 3;
            this.lblInfo.Text = "Information";
            this.lblInfo.Click += new System.EventHandler(this.lblInfo_Click);
            // 
            // lblExit
            // 
            this.lblExit.AutoSize = true;
            this.lblExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblExit.Font = new System.Drawing.Font("Script MT Bold", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblExit.Location = new System.Drawing.Point(24, 570);
            this.lblExit.Name = "lblExit";
            this.lblExit.Size = new System.Drawing.Size(76, 30);
            this.lblExit.TabIndex = 2;
            this.lblExit.Text = "EXIT";
            this.lblExit.Click += new System.EventHandler(this.lblExit_Click);
            // 
            // lblSaveToFile
            // 
            this.lblSaveToFile.AutoSize = true;
            this.lblSaveToFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblSaveToFile.Font = new System.Drawing.Font("Script MT Bold", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblSaveToFile.Location = new System.Drawing.Point(611, 17);
            this.lblSaveToFile.Name = "lblSaveToFile";
            this.lblSaveToFile.Size = new System.Drawing.Size(123, 30);
            this.lblSaveToFile.TabIndex = 2;
            this.lblSaveToFile.Text = "Save to file";
            this.lblSaveToFile.Click += new System.EventHandler(this.lblSaveToFile_Click);
            // 
            // lblSendMessage
            // 
            this.lblSendMessage.AutoSize = true;
            this.lblSendMessage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblSendMessage.Font = new System.Drawing.Font("Script MT Bold", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblSendMessage.Location = new System.Drawing.Point(579, 570);
            this.lblSendMessage.Name = "lblSendMessage";
            this.lblSendMessage.Size = new System.Drawing.Size(155, 30);
            this.lblSendMessage.TabIndex = 2;
            this.lblSendMessage.Text = "Send Message";
            this.lblSendMessage.Click += new System.EventHandler(this.lblSendMessage_Click);
            // 
            // tbUserMessage
            // 
            this.tbUserMessage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.tbUserMessage.Font = new System.Drawing.Font("Perpetua", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tbUserMessage.Location = new System.Drawing.Point(24, 518);
            this.tbUserMessage.Multiline = true;
            this.tbUserMessage.Name = "tbUserMessage";
            this.tbUserMessage.Size = new System.Drawing.Size(710, 49);
            this.tbUserMessage.TabIndex = 1;
            // 
            // rtfChatContent
            // 
            this.rtfChatContent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.rtfChatContent.Font = new System.Drawing.Font("Perpetua", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.rtfChatContent.Location = new System.Drawing.Point(24, 81);
            this.rtfChatContent.Name = "rtfChatContent";
            this.rtfChatContent.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtfChatContent.Size = new System.Drawing.Size(710, 405);
            this.rtfChatContent.TabIndex = 0;
            this.rtfChatContent.Text = "";
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.Filter = "\"Text files(*.txt)|*.txt|All files(*.*)|*.*\"";
            // 
            // gbAuth
            // 
            this.gbAuth.Controls.Add(this.lblRegistration);
            this.gbAuth.Controls.Add(this.lblLogin);
            this.gbAuth.Controls.Add(this.tbAuthPass);
            this.gbAuth.Controls.Add(this.tbAuthLogin);
            this.gbAuth.Controls.Add(this.cgjvgjnhvj);
            this.gbAuth.Controls.Add(this.sdsdsd);
            this.gbAuth.Location = new System.Drawing.Point(801, 12);
            this.gbAuth.Name = "gbAuth";
            this.gbAuth.Size = new System.Drawing.Size(306, 151);
            this.gbAuth.TabIndex = 1;
            this.gbAuth.TabStop = false;
            this.gbAuth.Text = "Authorization";
            // 
            // lblRegistration
            // 
            this.lblRegistration.AutoSize = true;
            this.lblRegistration.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblRegistration.Font = new System.Drawing.Font("Rockwell", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblRegistration.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblRegistration.Location = new System.Drawing.Point(3, 121);
            this.lblRegistration.Name = "lblRegistration";
            this.lblRegistration.Size = new System.Drawing.Size(144, 27);
            this.lblRegistration.TabIndex = 2;
            this.lblRegistration.Text = "Registration";
            this.lblRegistration.Click += new System.EventHandler(this.lblRegistration_Click);
            // 
            // lblLogin
            // 
            this.lblLogin.AutoSize = true;
            this.lblLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblLogin.Font = new System.Drawing.Font("Rockwell", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblLogin.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblLogin.Location = new System.Drawing.Point(226, 121);
            this.lblLogin.Name = "lblLogin";
            this.lblLogin.Size = new System.Drawing.Size(74, 27);
            this.lblLogin.TabIndex = 2;
            this.lblLogin.Text = "Login";
            this.lblLogin.Click += new System.EventHandler(this.lblLogin_Click);
            // 
            // tbAuthPass
            // 
            this.tbAuthPass.Location = new System.Drawing.Point(82, 71);
            this.tbAuthPass.Name = "tbAuthPass";
            this.tbAuthPass.PasswordChar = '*';
            this.tbAuthPass.Size = new System.Drawing.Size(218, 27);
            this.tbAuthPass.TabIndex = 1;
            // 
            // tbAuthLogin
            // 
            this.tbAuthLogin.Location = new System.Drawing.Point(82, 38);
            this.tbAuthLogin.Name = "tbAuthLogin";
            this.tbAuthLogin.Size = new System.Drawing.Size(218, 27);
            this.tbAuthLogin.TabIndex = 0;
            // 
            // cgjvgjnhvj
            // 
            this.cgjvgjnhvj.AutoSize = true;
            this.cgjvgjnhvj.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cgjvgjnhvj.Font = new System.Drawing.Font("Script MT Bold", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.cgjvgjnhvj.Location = new System.Drawing.Point(13, 71);
            this.cgjvgjnhvj.Name = "cgjvgjnhvj";
            this.cgjvgjnhvj.Size = new System.Drawing.Size(63, 30);
            this.cgjvgjnhvj.TabIndex = 2;
            this.cgjvgjnhvj.Text = "Pass";
            this.cgjvgjnhvj.Click += new System.EventHandler(this.lblExit_Click);
            // 
            // sdsdsd
            // 
            this.sdsdsd.AutoSize = true;
            this.sdsdsd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sdsdsd.Font = new System.Drawing.Font("Script MT Bold", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.sdsdsd.Location = new System.Drawing.Point(13, 33);
            this.sdsdsd.Name = "sdsdsd";
            this.sdsdsd.Size = new System.Drawing.Size(70, 30);
            this.sdsdsd.TabIndex = 2;
            this.sdsdsd.Text = "Login";
            this.sdsdsd.Click += new System.EventHandler(this.lblExit_Click);
            // 
            // getMessageTimer
            // 
            this.getMessageTimer.Interval = 1000;
            this.getMessageTimer.Tick += new System.EventHandler(this.getMessageTimer_Tick);
            // 
            // gbHistory
            // 
            this.gbHistory.Controls.Add(this.treeChtHistory);
            this.gbHistory.Location = new System.Drawing.Point(801, 169);
            this.gbHistory.Name = "gbHistory";
            this.gbHistory.Size = new System.Drawing.Size(306, 383);
            this.gbHistory.TabIndex = 2;
            this.gbHistory.TabStop = false;
            this.gbHistory.Text = "Chat History";
            // 
            // treeChtHistory
            // 
            this.treeChtHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeChtHistory.Location = new System.Drawing.Point(3, 23);
            this.treeChtHistory.Name = "treeChtHistory";
            this.treeChtHistory.Size = new System.Drawing.Size(300, 357);
            this.treeChtHistory.TabIndex = 0;
            // 
            // lbClearHistory
            // 
            this.lbClearHistory.AutoSize = true;
            this.lbClearHistory.Location = new System.Drawing.Point(1016, 560);
            this.lbClearHistory.Name = "lbClearHistory";
            this.lbClearHistory.Size = new System.Drawing.Size(91, 20);
            this.lbClearHistory.TabIndex = 3;
            this.lbClearHistory.TabStop = true;
            this.lbClearHistory.Text = "Clear history";
            this.lbClearHistory.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lbClearHistory_LinkClicked);
            // 
            // lbFurtherPlans
            // 
            this.lbFurtherPlans.AutoSize = true;
            this.lbFurtherPlans.Location = new System.Drawing.Point(801, 560);
            this.lbFurtherPlans.Name = "lbFurtherPlans";
            this.lbFurtherPlans.Size = new System.Drawing.Size(94, 20);
            this.lbFurtherPlans.TabIndex = 4;
            this.lbFurtherPlans.TabStop = true;
            this.lbFurtherPlans.Text = "Further plans";
            this.lbFurtherPlans.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lbFurtherPlans_LinkClicked);
            // 
            // dtPicker
            // 
            this.dtPicker.Location = new System.Drawing.Point(801, 596);
            this.dtPicker.Name = "dtPicker";
            this.dtPicker.Size = new System.Drawing.Size(303, 27);
            this.dtPicker.TabIndex = 5;
            // 
            // frmChat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1136, 635);
            this.Controls.Add(this.dtPicker);
            this.Controls.Add(this.lbFurtherPlans);
            this.Controls.Add(this.lbClearHistory);
            this.Controls.Add(this.gbHistory);
            this.Controls.Add(this.gbAuth);
            this.Controls.Add(this.pnlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmChat";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chat 1.0.0.1 DB-based";
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.gbAuth.ResumeLayout(false);
            this.gbAuth.PerformLayout();
            this.gbHistory.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.TextBox tbUserMessage;
        private System.Windows.Forms.RichTextBox rtfChatContent;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.Label lblExit;
        private System.Windows.Forms.Label lblSaveToFile;
        private System.Windows.Forms.Label lblSendMessage;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.GroupBox gbAuth;
        private System.Windows.Forms.TextBox tbAuthPass;
        private System.Windows.Forms.TextBox tbAuthLogin;
        private System.Windows.Forms.Label cgjvgjnhvj;
        private System.Windows.Forms.Label sdsdsd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblRegistration;
        private System.Windows.Forms.Label lblLogin;
        private System.Windows.Forms.TextBox tbUserCorrespond;
        private System.Windows.Forms.LinkLabel lbFind;
        private System.Windows.Forms.Timer getMessageTimer;
        private System.Windows.Forms.GroupBox gbHistory;
        private System.Windows.Forms.TreeView treeChtHistory;
        private System.Windows.Forms.LinkLabel lbClearHistory;
        private System.Windows.Forms.LinkLabel lbLoadHistory;
        private System.Windows.Forms.LinkLabel lbClearChat;
        private System.Windows.Forms.LinkLabel lbFurtherPlans;
        private System.Windows.Forms.DateTimePicker dtPicker;
    }
}

