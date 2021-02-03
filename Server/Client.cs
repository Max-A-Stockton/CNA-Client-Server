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

namespace Server
{
    class Client
    {
        private Socket socket;
        private NetworkStream stream;
        private BinaryReader reader;
        private BinaryWriter writer;
        private object readLock;
        private object writeLock;
        private BinaryFormatter formatter;

        public IPEndPoint ipEndPoint;

        public Client(Socket newSocket)
        {
            readLock = new object();
            writeLock = new object();
            socket = newSocket;
            stream = new NetworkStream(newSocket);
            reader = new BinaryReader(stream, Encoding.UTF8);
            writer = new BinaryWriter(stream, Encoding.UTF8);
            formatter = new BinaryFormatter();

        }
        //Close function
        public void Close()
        {
            stream.Close();
            reader.Close();
            writer.Close();
            socket.Close();
        }
        //read message sent from the client
        public Packet Read()
        {
            lock (readLock)
            {
                //Read bytes sent to server
                int numberofBytes = 0;
                if ((numberofBytes = reader.ReadInt32()) != -1)
                {
                    byte[] buffer = reader.ReadBytes(numberofBytes);
                    MemoryStream memSream = new MemoryStream(buffer);
                    return formatter.Deserialize(memSream) as Packet;
                }
                return null;
            }
        }
        //Send the read message back to the client
        public void Send(Packet message)
        {
            lock (writeLock)
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
            }
        }
    }
}
