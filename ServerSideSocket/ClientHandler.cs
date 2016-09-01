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
    class ClientHandler
    {
        internal void Start(Socket clientSocket, int clientNumber)
        {
            NetworkStream stream = new NetworkStream(clientSocket);
            StreamWriter writer = new StreamWriter(stream);
            StreamReader reader = new StreamReader(stream);

            writer.WriteLine("The server is ready. Close connection with the \"EXIT\" command.");
            writer.Flush();

            string clientText;
            do
            {
                clientText = reader.ReadLine();
                Console.WriteLine("Client {0} says: {1}", clientNumber, clientText);

                if (clientText.ToLower() == "time?")
                    writer.WriteLine(DateTime.Now.ToLongTimeString());
                else if (clientText.ToLower() == "date?")
                    writer.WriteLine(DateTime.Now.ToLongDateString());
                else if (clientText.ToLower().Substring(0, 3) == "add")
                    try
                    {
                        writer.WriteLine("Sum: {0}", CommandHandler.Add(clientText));
                    }
                    catch (Exception)
                    {
                        writer.WriteLine("\"Add\" syntax invalid.");
                    }
                else if (clientText.ToLower().Substring(0, 3) == "sub")
                    try
                    {
                        writer.WriteLine("Differens: {0}", CommandHandler.Substract(clientText));
                    }
                    catch (Exception)
                    {
                        writer.WriteLine("\"Sub\" syntax invalid.");
                    }
                else if (clientText.ToLower() != "exit")
                    writer.WriteLine("Unknown command.");
                writer.Flush();
            } while (clientText.ToLower() != "exit");

            reader.Close();
            writer.Close();
            stream.Close();
            clientSocket.Close();
        }
    }
}
