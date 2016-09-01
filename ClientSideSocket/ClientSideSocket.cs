using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;

namespace ClientSideSocket
{
    class ClientSideSocket
    {
        private string serverName;
        private int port;

        public ClientSideSocket(string serverName, int port)
        {
            this.serverName = serverName;
            this.port = port;
        }

        internal void Run()
        {
            Console.WriteLine(GetConnectionInfo());
            TcpClient server = new TcpClient(serverName, port);

            NetworkStream stream = server.GetStream();
            StreamWriter writer = new StreamWriter(stream);
            StreamReader reader = new StreamReader(stream);

            // waiting for a ready-message from the server
            string serverText;
            serverText = reader.ReadLine();
            Console.WriteLine(serverText);

            string message;
            do
            {
                Console.Write("Write a message to the server: ");
                message = Console.ReadLine();
                try
                {
                    writer.WriteLine(message);
                    writer.Flush();
                    serverText = reader.ReadLine();
                    Console.WriteLine(serverText);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error message: {0}", e.Message);
                    Console.WriteLine("Press enter to continue...");
                    Console.ReadLine();
                }

            } while (message.ToLower() != "exit");

            Console.WriteLine("Shutting down connection...");
            reader.Close();
            writer.Close();
            stream.Close();
            server.Close();

            Console.WriteLine("Press enter to continue...");
            Console.ReadLine();
        }

        private string GetConnectionInfo()
        {
            string connectionInfo = "This client is connected to " + serverName + " at port " + port + ".";
            return connectionInfo;
        }
    }
}
