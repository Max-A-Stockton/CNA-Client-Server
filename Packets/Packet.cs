using System;
using System.Net;

namespace Packets
{
    public enum packetType
        {
            chatMessage,
            privateMessage,
            nickname,
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
        public string m_Message { get; protected set; }
        public ChatMessagePacket(string message)
        {
            m_Message = message;
            m_Packet = packetType.chatMessage;
        }
    }
    [Serializable]
    public class NicknamePacket : Packet
    {
        public string m_Nickname { get; protected set; }
        public NicknamePacket(string nickname)
        {
            m_Nickname = nickname;
            m_Packet = packetType.nickname;
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
