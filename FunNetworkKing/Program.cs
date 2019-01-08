using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace FunNetworkKing
{
    class Program
    {
        Socket socket;

        static void Main(string[] args)
        {
            socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
            
        }
    }
}
