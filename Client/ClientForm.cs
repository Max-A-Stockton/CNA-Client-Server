using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Packets;

namespace Client
{
    public partial class ClientForm : Form
    {
        Client _client;
        String nickname;
        String message;
        //Initialise Client
        public ClientForm(Client client)
        {
            InitializeComponent();

           _client = client;
        }
        //Submit button
        private void SubmitButton_Click(object sender, EventArgs e)
        {

            message = InputField.Text;
            String _nickname = nickname;
            _nickname = _nickname + ": ";
            message = message.Insert(0, _nickname);
            if (_nickname == ": ")
            {
                message = ("A user without a set nickname tried to send a message.");
            }
            ChatMessagePacket chatPacket = new ChatMessagePacket(message);
            _client.SendMessage(chatPacket);
        }
        //Change nickname button
        private void ChangeNicknameButton_Click_1(object sender, EventArgs e)
        {
            nickname = NicknameInputField.Text;
            NicknamePacket nicknamePacket = new NicknamePacket(nickname);
            _client.SendNickname(nicknamePacket);
        }

        //Update the chat window
        public void UpdateChatWindow(String message)
        {
            if (MessageWindow.InvokeRequired)
            {
                Invoke(new Action(() =>
                {
                    UpdateChatWindow(message);
                }));
            }
            else
            {
                MessageWindow.Text += message + Environment.NewLine;
                MessageWindow.SelectionStart = MessageWindow.Text.Length;
                MessageWindow.ScrollToCaret();
            }

        }

        public void UpdateNicknameWindow(String nickname)
        {
            if (MessageWindow.InvokeRequired)
            {
                Invoke(new Action(() =>
                {
                    UpdateNicknameWindow(nickname);
                }));
            }
            else
            {
                MessageWindow.Text += message + Environment.NewLine;
                MessageWindow.SelectionStart = MessageWindow.Text.Length;
                MessageWindow.ScrollToCaret();
            }

        }

    }
}
