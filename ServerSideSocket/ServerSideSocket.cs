using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;
using System.Net;

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

            Console.WriteLine("Server is ready for a client to connect.");

            Socket clientSocket = listener.AcceptSocket();

            Console.WriteLine("A connection has been made.");

            NetworkStream stream = new NetworkStream(clientSocket);
            StreamWriter writer = new StreamWriter(stream);
            StreamReader reader = new StreamReader(stream);

            writer.WriteLine("The server is ready");
            writer.Flush();

            string clientText;
            do
            {
                clientText = reader.ReadLine();
                Console.WriteLine("Client says: " + clientText);

                if (clientText.ToLower() == "time?")
                    writer.WriteLine(DateTime.Now.ToLongTimeString());
                else if (clientText.ToLower() == "date?")
                    writer.WriteLine(DateTime.Now.ToLongDateString());
                else if (clientText.ToLower().Substring(0, 3) == "add")
                    try
                    {
                        writer.WriteLine("Sum: " + Add(clientText));
                    }
                    catch (Exception)
                    {
                        writer.WriteLine("\"Add\" syntax invalid.");
                    }
                else if (clientText.ToLower().Substring(0, 3) == "sub")
                    try
                    {
                        writer.WriteLine("Differens: " + Substract(clientText));
                    }
                    catch (Exception)
                    {
                        writer.WriteLine("\"Sub\" syntax invalid.");
                    }
                else
                    writer.WriteLine("Unknown command.");
                writer.Flush();
            } while (clientText.ToLower() != "exit");

            Console.WriteLine("Shutting down connection...");
            writer.Close();
            reader.Close();
            stream.Close();
            clientSocket.Close();

            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }

        private int Add(string clientText)
        {
            int firstNumber = int.Parse(clientText.Split(' ')[1]);
            int secondNumber = int.Parse(clientText.Split(' ')[2]);
            int sum = firstNumber + secondNumber;
            return sum;
        }

        private int Substract(string clientText)
        {
            int firstNumber = int.Parse(clientText.Split(' ')[1]);
            int secondNumber = int.Parse(clientText.Split(' ')[2]);
            int differens = firstNumber - secondNumber;
            return differens;
        }
    }
}
