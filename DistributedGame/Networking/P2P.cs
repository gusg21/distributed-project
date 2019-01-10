using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DistributedGame.Networking
{
    class P2P
    {
        public static void Main(string[] args)
        {
            TcpListener listener = new TcpListener(8888);
            Console.WriteLine("Up");
            listener.Start();
            Socket socket = listener.AcceptSocket();
            Stream networkStream = new NetworkStream(socket);
            List<String> names = new List<string>();
            names.Add("CARL");
            names.Add("Murder");
            byte[] send = System.Text.Encoding.ASCII.GetBytes(String.Join(", ", names.ToArray()));
            socket.Send(send);
            byte[] data = new byte[1024];
            socket.Receive(data);
            String rec = System.Text.Encoding.ASCII.GetString(data);
            Console.WriteLine(rec);
            Thread.Sleep(10000);
        }
        /*
         * - Manages all of the P2P relations
         * - Ideally has nice functions to call for updating your position
         */

    }
}
