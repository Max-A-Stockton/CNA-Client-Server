using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
       //Start client  &connect to server hosted on this machine
        private const string serverIP = "127.0.0.1";
        private const int serverPort = 4444;

        static void Main(string[] args)
        {
            Client client = new Client();

            if (client.Connect(serverIP, serverPort))
            {
                client.Run();
            }
        }
    }
}
