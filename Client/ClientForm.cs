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
            _client.SendMessage(chatPacket);
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
