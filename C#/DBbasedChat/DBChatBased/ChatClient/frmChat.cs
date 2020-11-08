using BL;
using BL.Models;
using System;
using System.Drawing;
using System.Windows.Forms;
using Unity;

namespace ChatClient
{
    public partial class frmChat : Form
    {
        private User User = default;
        private User UserCorrespond = default;
        private Chat Chat = default;

        private Authorization auth { get; set; }
        private ChatOperations ch { get; set; }

        public frmChat()
        {
            UContainer.Registration();

            InitializeComponent();

            auth = UContainer.services.Resolve<Authorization>();
            ch = UContainer.services.Resolve<ChatOperations>();
        }

        /// <summary>
        /// Exit
        /// </summary>
        private void lblExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you really want to leave now ?", ":(((", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes )
            {
                Application.Exit();
            }
        }

        /// <summary>
        /// Login
        /// </summary>
        private void lblLogin_Click(object sender, EventArgs e)
        {
            var login = tbAuthLogin.Text;
            var pass = tbAuthPass.Text;

            var result = auth.MakeLogin(login, pass);

            if (result.Succeed)
            {
                User = result.User;

                PrintMessage($"User '{User.Login}' logged in ({DateTime.Now})", Color.Green);

                PrepareChat();
            }
            else
                ShowErrorMessage($"Login failed: {result?.Ex.Message}", "Authorization failed");
        }

        /// <summary>
        /// Registration
        /// </summary>
        private void lblRegistration_Click(object sender, EventArgs e)
        {
            var login = tbAuthLogin.Text;
            var pass = tbAuthPass.Text;

            try
            {
                User = auth.CreateUser(login, pass);

                PrintMessage($"User '{User.Login}' registered in ({DateTime.Now})", Color.Blue);                

                PrepareChat();
            }
            catch(Exception ex)
            {
                ShowErrorMessage($"Registration failed: {ex.Message}", "Authorization failed");
            }

        }

        /// <summary>
        /// Send message
        /// </summary>
        private void lblSendMessage_Click(object sender, EventArgs e)
        {
            var msg = tbUserMessage.Text.Trim();

            if (String.IsNullOrEmpty(msg))
                return;

            BL.Models.Message message = default;

            // Чат должен быть создан при поиске юзера
            if (Chat == null)
            {
                PrintMessage($"Chat was nor created or loaded. Use 'Find' link.", Color.BurlyWood);
                return;
            }

            try
            {
                message = ch.SendMessage(User, Chat, msg);
                
                PrintMessage($">> message sent -> {Chat.id} | {Chat.ChatTableName}.", Color.Green);

                if (message == null || message.id == default)
                    PrintMessage($"[x] {DateTime.Now}. Send message failed (NULL)", Color.DarkRed);
                else
                    tbUserMessage.Text = "";
            }
            catch (Exception ex)
            {
                PrintMessage($"[x] {DateTime.Now}. Send message error: {ex.Message}. Message not sent!", Color.DarkRed);
                return;
            }

            //list of messages will be on Timer
            getMessageTimer.Enabled = true;
        }

        /// <summary>
        /// Find correspond user by Login
        /// </summary>
        private void lbFind_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var corUser = tbUserCorrespond.Text.Trim();

            if (corUser == User.Login)
            {
                MessageBox.Show("You can't chat with yourself!");
                return;
            }

            try
            {
                UserCorrespond = auth.GetUserByLogin(corUser);

                PrintMessage($">> User with login '{UserCorrespond.Login}' found ({DateTime.Now})", Color.DarkRed);

                //Load chat
                if (Chat == null)
                {
                    Chat = ch.CreateChat(User, UserCorrespond);

                    PrintMessage("Loading history...", Color.Gray);

                    //load chat history
                    RetrieveChatMessages();

                    PrintMessage("Chat history loaded.", Color.Gray);
                }
                else
                    PrintMessage($"Chat already exists: {Chat.id}|{Chat.ChatTableName}", Color.Chocolate);

                //add to history
                treeChtHistory.Nodes.Add(UserCorrespond.Login + " [" + Chat.CreatedDate.ToString("yyyy.MM.dd") + "]");
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Can't find user: {ex.Message}", "Search failed");
            }
        }

        /// <summary>
        /// Get chat messages (timer)
        /// </summary>
        private void getMessageTimer_Tick(object sender, EventArgs e)
        {
            RetrieveChatMessages(getFullHistory: false);
        }

        //MISC...
        void PrintMessage(string msg, Color color)
        {
            if (String.IsNullOrEmpty(rtfChatContent.Text))
            {
                var length = rtfChatContent.Text.Length;
                var msgLength = msg.Length;

                rtfChatContent.Text = msg;

                rtfChatContent.Select(length, msgLength);
                rtfChatContent.SelectionColor = color;
            }
            else
            {
                var length = rtfChatContent.Text.Length;
                var msgLength = msg.Length;

                rtfChatContent.AppendText("\r\n" + msg);

                rtfChatContent.Select(length, msgLength);
                rtfChatContent.SelectionColor = color;
            }

            rtfChatContent.ScrollToCaret();
        }

        void ShowErrorMessage(string msg, string title)
        {
            MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        void PrepareChat()
        {
            pnlMain.Visible = true;
            gbAuth.Enabled = false;

            lblInfo.Text = $"Wellcome, {User.Login} !!!";
        }

        void pnlMain_Paint(object sender, PaintEventArgs e)
        {

        }

        void RetrieveChatMessages(bool getFullHistory = true)
        {
            var chatContent = ch.GetChatContent(Chat, getFullHistory);
            ConvertChatContentToView(chatContent);
        }

        /// <summary>
        /// Cleare history (tree)
        /// </summary>
        private void lbClearHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            treeChtHistory.Nodes.Clear();
        }

        private void lbLoadHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (UserCorrespond == null) return;

            var chatContent = ch.GetChatContent(Chat, getFullHistory : true);
            ConvertChatContentToView(chatContent);

            PrintMessage(">>> Loading history... ", Color.Gray);
            RetrieveChatMessages();
        }

        void ConvertChatContentToView (ChatView chatContent)
        {
            if (chatContent != null || chatContent.Content != null)
            {
                if (chatContent.Content.Count > 0)
                {
                    foreach (var message in chatContent.Content)
                    {
                        var msgView = $"[{message.Date}] from {message.UserFrom} => {message.Text}";
                        PrintMessage(msgView, Color.Black);
                    }
                }
            }
        }

        /// <summary>
        /// Save chat content to file
        /// </summary>
        private void lblSaveToFile_Click(object sender, EventArgs e)
        {
            if (UserCorrespond != null)
            {
                if (saveFileDialog.ShowDialog() == DialogResult.Cancel) return;
                var filename = $"{User.Login}->{UserCorrespond.Login}_chat.txt";
                System.IO.File.WriteAllText(filename, rtfChatContent.Text);
                MessageBox.Show($"Chat {Chat.id} saved.", "Chat saved", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        /// <summary>
        /// Clear Chat window
        /// </summary>
        private void lbClearChat_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (UserCorrespond != null)
            {
                if (MessageBox.Show("Do you want to clear Chat Window?\nThis action can not be canceled.", "Chat window", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    rtfChatContent.Text = "";
                }
            }
        }

        /// <summary>
        /// Plans
        /// </summary>
        private void lbFurtherPlans_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("-Store CS in eccrypted binary file\n-Show chat content as HTML\n-Remove dbo.Messages and move this logic to custom table.\n-Use Azure DB","Plans",MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void lblInfo_Click(object sender, EventArgs e)
        {

        }
    }
}
