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
        //Initialise Client
        public ClientForm(Client client)
        {
            InitializeComponent();

           _client = client;
        }
        //Submit button
        private void SubmitButton_Click(object sender, EventArgs e)
        {
            ChatMessagePacket chatPacket = new ChatMessagePacket(InputField.Text);
            String _nickname = nickname;
            _nickname = _nickname + ": ";
            NicknamePacket nicknamePacket = new NicknamePacket(_nickname);
            _client.SendMessage(nicknamePacket, chatPacket);
        }
        
        //Change nickname button
        private void ChangeNicknameButton_Click(object sender, EventArgs e)
        {
            nickname = NicknameInputField.Text;
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
    }
}
