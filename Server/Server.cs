using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Collections.Concurrent;
using System.Net.Configuration;
using System.Runtime.Serialization.Formatters.Binary;
using Packets;

namespace Server
{
    class Server
    {
        private TcpListener tcpListener;
        private ConcurrentDictionary<int, Client> clients;
        private UdpClient udpListener;
        private BinaryFormatter formatter;

        public Server(string ipAddress, int port)
        {
           IPAddress ip = IPAddress.Parse(ipAddress);
            tcpListener = new TcpListener(ip, port);
            udpListener = new UdpClient(port);

            Thread thread1 = new Thread(() => { UdpListen(); });
            thread1.Start();
        }
        //Start the server
        public void Start()
        {
            clients = new ConcurrentDictionary<int, Client>();
            tcpListener.Start();

            int clientindex = 0;

            Console.WriteLine("Listening...");

            //Start a thread for every new client
            while (true)
            {
                int index = clientindex;
                clientindex++;
                
                Socket socket = tcpListener.AcceptSocket();

                Client client = new Client(socket);
                clients.TryAdd(index, client);

                Thread thread = new Thread(() => { ClientMethod(index); });
                thread.Start();
            }
        }
        //Stop the server
        public void Stop()
        {
            tcpListener.Stop();
        }

        //Client Method
        private void ClientMethod(int index)
        {
            Packet receivedMessage;

            while ((receivedMessage = clients[index].Read()) != null)
            {
                switch (receivedMessage.m_Packet)
                {
                    //Chat packet
                    case packetType.login:
                        LoginPacket loginPacket = (LoginPacket)receivedMessage;
                        break;
                    case packetType.chatMessage:
                        ChatMessagePacket chatPacket = (ChatMessagePacket)receivedMessage;
                        clients[index].Send(chatPacket);
                        break;
                    //Private message packet
                    case packetType.privateMessage:
                        break;
                    //Client name packet
                    case packetType.clientName:
                        break;
                }
            }
            
            clients[index].Close();
            Client c;
            clients.TryRemove(index, out c);

        }
        private void UdpListen()
        {
            try
            {
                IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Any, 0);
                while (true)
                {
                    byte[] bytes = udpListener.Receive(ref ipEndPoint);
                    MemoryStream memSream = new MemoryStream(bytes);
                    Packet receivedPacket = formatter.Deserialize(memSream) as Packet;

                    switch (receivedPacket.m_Packet)
                    {
                        case packetType.chatMessage:
                            ChatMessagePacket chatPacket = (ChatMessagePacket)receivedPacket;
                            clientForm.UpdateChatWindow(chatPacket.m_Messagee);
                            break;
                        case packetType.privateMessage:
                            break;
                        case packetType.clientName:
                            break;
                    }
                }
            }

            foreach (Client c in clients)
            {

            }
        }

        //Return message
        private string GetReturnMessage(string code)
        {
            return code;
        }
    }
}
