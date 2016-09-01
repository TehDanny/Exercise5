using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerSideSocket
{
    static class CommandHandler
    {
        internal static int Add(string clientText)
        {
            int firstNumber = int.Parse(clientText.Split(' ')[1]);
            int secondNumber = int.Parse(clientText.Split(' ')[2]);
            int sum = firstNumber + secondNumber;
            return sum;
        }

        internal static int Substract(string clientText)
        {
            int firstNumber = int.Parse(clientText.Split(' ')[1]);
            int secondNumber = int.Parse(clientText.Split(' ')[2]);
            int differens = firstNumber - secondNumber;
            return differens;
        }
    }
}
