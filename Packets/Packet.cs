using System;
using System.Net;

namespace Packets
{
    public enum packetType
        {
            chatMessage,
            privateMessage,
            clientName,
            login
        }

    [Serializable]
    public abstract class Packet
    {
        public packetType m_Packet { get; protected set; }
    }
    [Serializable]
    public class ChatMessagePacket : Packet
    {
        public string m_Messagee { get; protected set; }
        public ChatMessagePacket(string message)
        {
            m_Messagee = message;
            m_Packet = packetType.chatMessage;
        }
    }
    [Serializable]
    public class LoginPacket : Packet
    {
        public IPEndPoint m_EndPoint;
        public LoginPacket(IPEndPoint EndPoint)
        {
            m_EndPoint = EndPoint;
            m_Packet = packetType.login;
        }

    }


    
}
