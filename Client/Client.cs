using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Net.Configuration;
using System.Runtime.Serialization.Formatters.Binary;
using Packets;

namespace Client
{
    public class Client
    {
        private TcpClient tcpClient;
        private NetworkStream stream;
        private BinaryWriter writer;
        private BinaryReader reader;
        private BinaryFormatter formatter;
        private ClientForm clientForm;
        private UdpClient udpClient;

        public String nickname;

        //Constructor
        public Client()
        {
            tcpClient = new TcpClient();
            formatter = new BinaryFormatter();
            udpClient = new UdpClient();
        }

        public void Login()
        {
            IPEndPoint m_IpEndPoint = (IPEndPoint)udpClient.Client.LocalEndPoint;
        }

        //Connect to the server
        public bool Connect(String ipAddress, int port)
        {
            try
            {
                tcpClient.Connect(ipAddress, port);
                udpClient.Connect(ipAddress, port);
                stream = tcpClient.GetStream();
                reader = new BinaryReader(stream, Encoding.UTF8);
                writer = new BinaryWriter(stream, Encoding.UTF8);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(" Exception: " + e.Message);
                return false;
            }
        }

        //Run the Process Server Response on thread 1
        public void Run()
        {

            clientForm = new ClientForm(this);

            Thread thread1 = new Thread(() => { ProcessServerResponse(); });
            thread1.Start();
            Thread thread2 = new Thread(() => { UdpProcessServerResponse(); });
            thread2.Start();

            Login();

            clientForm.ShowDialog();

            tcpClient.Close();
            udpClient.Close();
        }
        public void UdpSendMessage(Packet packet)
        {
            MemoryStream memoryStream = new MemoryStream();
            formatter.Serialize(memoryStream, packet);

            //Store the byte array from the memry stream in a new variable
            byte[] buffer = memoryStream.GetBuffer();

            //Write length of array to writer
            udpClient.Send(buffer, buffer.Length);

            //Write Byte array to the writer
            writer.Write(buffer);

            writer.Flush();
        }

        public void UdpProcessServerResponse()
        {
            try
            {
                IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Any, 0);
                while (true)
                {
                    byte[] bytes = udpClient.Receive(ref ipEndPoint);
                    MemoryStream memSream = new MemoryStream(bytes);
                    Packet receivedPacket = formatter.Deserialize(memSream) as Packet;

                    switch (receivedPacket.m_Packet)
                    {
                        case packetType.chatMessage:
                            ChatMessagePacket chatPacket = (ChatMessagePacket)receivedPacket;
                            clientForm.UpdateChatWindow(chatPacket.m_Message);
                            break;
                        case packetType.privateMessage:
                            break;
                        case packetType.nickname:
                            break;
                    }
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("Client UDP Read Method exception: " + e.Message);
            }

        }

        //Process server response
        private void ProcessServerResponse()
        {
            while (reader != null)
            {
                int numberofBytes = 0;
                if ((numberofBytes = reader.ReadInt32()) != -1)
                {
                    byte[] buffer = reader.ReadBytes(numberofBytes);
                    MemoryStream memSream = new MemoryStream(buffer);
                    Packet receivedPacket =  formatter.Deserialize(memSream) as Packet;
                    
                    switch (receivedPacket.m_Packet)
                    {
                        case packetType.chatMessage:
                            ChatMessagePacket chatPacket = (ChatMessagePacket)receivedPacket;
                            clientForm.UpdateChatWindow(chatPacket.m_Message);
                            break;
                        case packetType.privateMessage:
                            break;
                        case packetType.nickname:
                            break;
                    }
                }


                /*Console.WriteLine("Server says: ");
                Console.WriteLine(reader.ReadLine());
                Console.WriteLine();

                MemoryStream memoryStream;

                while ((receivedMessage = reader.ReadInt32()) != null)
                {
                    /*if (receivedMessage.ToLower() == "bye")
                    {
                        clients[index].Send("Goodbye.");
                        break;
                    }
                    else
                    {
                        clients[index].Send(GetReturnMessage(receivedMessage));
                    }
                    
                }*/
            }
        }

        public void SendMessage(Packet message)
        {
                MemoryStream memoryStream = new MemoryStream();
                formatter.Serialize(memoryStream, message);

            //Store the byte array from the memry stream in a new variable
            byte[] buffer = memoryStream.GetBuffer();

                //Write length of array to writer
                writer.Write(buffer.Length);

                //Write Byte array to the writer
                writer.Write(buffer);

                writer.Flush();

            /*writer.WriteLine(message);
            writer.Flush();*/
        }
        public void SendNickname(Packet nickname)
        {
            MemoryStream memoryStream = new MemoryStream();
            formatter.Serialize(memoryStream, nickname);

            //Store the byte array from the memry stream in a new variable
            byte[] buffer = memoryStream.GetBuffer();

            //Write length of array to writer
            writer.Write(buffer.Length);

            //Write Byte array to the writer
            writer.Write(buffer);

            writer.Flush();

            /*writer.WriteLine(message);
            writer.Flush();*/
        }
    }
}