
using Microsoft.Xna.Framework;
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
    public class P2P
    {
        public void Server(int connectPort, string connectIp, int hostPort, string name)
        {
            TcpListener listener = new TcpListener(hostPort);
            Console.WriteLine("Up");
            listener.Start();
            Socket socket = listener.AcceptSocket();
            Stream networkStream = new NetworkStream(socket);
            List<String> names = new List<string>();
            names.Add("CARL");
            names.Add("Murder");
            byte[] send = System.Text.Encoding.ASCII.GetBytes((name));
            socket.Send(send);
            byte[] data = new byte[1024];
            socket.Receive(data);
            String recName = System.Text.Encoding.ASCII.GetString(data);
            Console.WriteLine(recName);
            byte[] data2 = new byte[1024];
            socket.Receive(data2);
            String rec = System.Text.Encoding.ASCII.GetString(data2);
            Console.WriteLine(rec);
            String[] recPos = rec.Split(',');
            Console.WriteLine(recPos);
            Peer tmpPeer = new Peer();
            tmpPeer.name = recName;
            Vector2 pos = new Vector2(float.Parse(recPos[0]), float.Parse(recPos[1]));
            tmpPeer.position = pos;
            Global.friends.Add(tmpPeer);
        }
        public void Client(int connectPort, int connectIP, Socket socket)
        {
            byte[] send2 = new byte[1024];
            while (true)
            {
                send2 = System.Text.Encoding.ASCII.GetBytes((Global.p.position.X.ToString() + "," + Global.p.position.Y.ToString()));
                socket.Send(send2);
                socket.Send(System.Text.Encoding.ASCII.GetBytes("\n"));
                Thread.Sleep(1000);
            }
        }
        /*
         * - Manages all of the P2P relations
         * - Ideally has nice functions to call for updating your position
         */

    }
}
