
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace DistributedGame.Networking
{
    public class P2P
    {
        Peer tmpPeer;
        public void Server(int hostPort, string name)
        {
            TcpListener listener = new TcpListener(hostPort);
            while (3 == 3)
            {
                Console.WriteLine("Up");
                listener.Start();
                Socket socket = listener.AcceptSocket();
                Console.WriteLine("New Connection Found");
                Stream networkStream = new NetworkStream(socket);
                Console.WriteLine("netstream");
                Listener(socket);
            }
        }

        public void Connector(int connectPort, string connectIP, string name)
        {
            if (connectIP == "localhost")
            {
                connectIP = "127.0.0.1";
            }
            IPAddress ipAdress = IPAddress.Parse(connectIP);
            Console.WriteLine(ipAdress);
            IPEndPoint ipEndpoint = new IPEndPoint(ipAdress, connectPort);
            Console.WriteLine(ipEndpoint.ToString());
            Socket connect = new Socket(ipAdress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            //try
            //{
            connect.Connect(ipEndpoint);
            Console.WriteLine("Client connected to {0}", connect.RemoteEndPoint.ToString());
            byte[] send = Encoding.ASCII.GetBytes((name));
            connect.Send(send);
            Console.WriteLine("sent");
            byte[] data = new byte[1024];
            connect.Receive(data); ;
            string recName = Encoding.ASCII.GetString(data).ToLower();
            recName = recName.Trim(' ', '\n', '\0');
            Console.WriteLine(recName + " is the other's name");
            tmpPeer = new Peer();
            tmpPeer.name = recName;
            tmpPeer.position = new Vector2(0, 0);
            Global.peers.AddChild(tmpPeer);

            Global.peerTracker.Add(recName, Global.peers.children.Count - 1);

            Thread client = new Thread(() => Client(8887, "localhost", connect, recName));
            client.Start();
            /*}
            catch
            {*/
            //Console.WriteLine("It broke.");
            //}
        }

        public void Client(int connectPort, string connectIP, Socket socket, string name)
        {
            byte[] send2 = new byte[1024];
            while (true)
            {
                Send("pos", socket);
                Receive(socket, name);
            }
        }
        private void Send(string send, Socket socket)
        {
            byte[] send2 = new byte[1024];
            switch (send)
            {
                case "pos":
                case "position":
                case "xy":
                    send2 = Encoding.ASCII.GetBytes("xy," + (Global.p.position.X.ToString() + "," + Global.p.position.Y.ToString()));
                    socket.Send(send2);
                    //Console.WriteLine(Encoding.ASCII.GetString(send2));
                    break;
            }
        }
        private void Receive(Socket socket, string name)
        {
            byte[] data = new byte[1024];
            socket.Receive(data);
            string rec = Encoding.ASCII.GetString(data);
            Console.WriteLine(rec);
            rec = rec.Trim(' ', '\n', '\0');
            List<string> recSplit = rec.Split(',').ToList<string>();
            //Console.WriteLine(recSplit[0] + "RECSPLIT");
            switch (recSplit[0])
            {
                case "xy":
                    Console.WriteLine("Rec.");
                    foreach (KeyValuePair<string, int> key in Global.peerTracker)
                    {
                        Console.WriteLine("Key = {0}, Value = {1}", key.Key, key.Value);
                    }
                    Console.WriteLine(name);
                    Console.WriteLine(Global.peerTracker[name]);
                    Console.WriteLine(rec + " REC");
                    ((Peer)Global.peers.children[Global.peerTracker[name]]).position = new Vector2(float.Parse(recSplit[1]), float.Parse(recSplit[2]));
                    break;
            }
        }
        public void Listener(Socket socket)
        {
            byte[] data = new byte[1024];
            socket.Receive(data);
            byte[] send = Encoding.ASCII.GetBytes((Global.name));
            Console.WriteLine("Got a name");
            string recName = Encoding.ASCII.GetString(data).ToLower();
            recName = recName.Trim(' ', '\n', '\0');
            socket.Send(send);
            Console.WriteLine("sent");

            tmpPeer = new Peer();
            tmpPeer.name = recName.Trim();
            tmpPeer.position = new Vector2(0, 0);
            Global.peers.AddChild(tmpPeer);
            Global.peerTracker.Add(recName, Global.peers.children.Count - 1);

            Receive(socket, recName);
            Thread client = new Thread(() => Client(8887, "localhost", socket, recName));
            client.Start();
        }
        
        /*
         * - Manages all of the P2P relations
         * - Ideally has nice functions to call for updating your position
         */

    }
}
