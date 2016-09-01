using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;
using System.Net;
using System.Threading;


namespace ServerSideSocket
{
    class ServerSideSocket
    {
        private IPAddress IP = IPAddress.Parse("127.0.0.1");
        private int port;
        private volatile bool stop;

        public ServerSideSocket(int port)
        {
            this.port = port;
        }

        internal void Run()
        {
            TcpListener listener = new TcpListener(IP, port);
            listener.Start();

            int clientNumber = 0;

            stop = false;

            while (!stop)
            {
                Console.WriteLine("Server is ready for a new client to connect.");

                Socket clientSocket = listener.AcceptSocket();
                clientNumber++;
                Console.WriteLine("Client {0}, connected.", clientNumber);

                Thread ClientHandlerThread = new Thread(new ClientHandler(clientSocket, clientNumber).Start);
                ClientHandlerThread.Start();
            }

            Console.WriteLine("Shutting down connection...");
            listener.Stop();
            Thread.Sleep(3000);
        }
    }
}
