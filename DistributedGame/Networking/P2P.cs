
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
        Packet positionPacket = new Packet("pos");
        List<Socket> sockets = new List<Socket>();
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

                Thread server = new Thread(() => Listener(socket));
                server.Start(); ; //Thread this
            }
        }

        public void Connector(int connectPort, string connectIP, string name, bool known = false)
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
            byte[] send = Encoding.ASCII.GetBytes((name) + "," + Global.host.ToString() + "," + (known ? 1 : 0).ToString());
            connect.Send(send);
            Console.WriteLine("sent");
            byte[] data = new byte[1024];
            connect.Receive(data);
            string recName = "";
            string[] process = new string[2];
            string rec = Encoding.ASCII.GetString(data).ToLower();
            process = rec.Split(',').ToArray<string>();
            recName = process[0].Trim(' ', '\n', '\0');
            Console.WriteLine(recName + " is the other's name");
            tmpPeer = new Peer();
            tmpPeer.name = recName;
            tmpPeer.position = new Vector2(0, 0);
            tmpPeer.port = int.Parse(process[1].Trim(' ', '\n', '\0'));

            Global.peers.AddChild(tmpPeer);

            Global.peerTracker.Add(recName, Global.peers.children.Count - 1);
            Thread listener = new Thread(() => Receiver(connect, recName));
            Thread client = new Thread(() => Client(8887, "localhost", connect, recName));

            listener.Start();
            client.Start();
            sockets.Add(connect);
            /*}
            catch
            {*/
            //Console.WriteLine("It broke.");
            //}
        }

        public void Client(int connectPort, string connectIP, Socket socket, string name)
        {
            while (true)
            {
                foreach (Packet packet in Global.packets)
                {
                    byte[] send2 = new byte[1024];
                    while (true)
                    {
                        Send(socket, packet);
                    }
                }
                Send(socket, positionPacket);
                Thread.Sleep(1);

            }
        }
        private void Send(Socket socket, Packet packet)
        {
            byte[] send2 = new byte[1024];
            string processString = "";
            switch (packet.send)
            {
                case "pos":
                case "position":
                case "xy":
                    processString = "xy," + (Global.p.position.X.ToString() + "," + Global.p.position.Y.ToString() + "," + Global.p.rotation + "|");
                    //Console.WriteLine(Encoding.ASCII.GetString(send2));
                    break;
                case "hit":
                    processString = "hit";
                    break;
            }
            if (packet.end)
            {
                processString = processString + ",end";
            }
            send2 = Encoding.ASCII.GetBytes(processString);
            socket.Send(send2);
        }
        private void Receive(Socket socket, string name)
        {
            while (true)
            {
                byte[] data = new byte[1024];
                socket.Receive(data);
                string rec = Encoding.ASCII.GetString(data);
                Console.WriteLine(rec);
                rec = rec.Trim(' ', '\n', '\0');
                List<string> commnands = rec.Split('|').ToList<string>();
                foreach (string command in commnands)
                {
                    try
                    {
                        List<string> recSplit = command.Split(',').ToList<string>();
                        //Console.WriteLine(recSplit[0] + "RECSPLIT");
                        switch (recSplit[0])
                        {
                            case "xy":
                                /*Console.WriteLine("Rec.");
                                foreach (KeyValuePair<string, int> key in Global.peerTracker)
                                {
                                    Console.WriteLine("Key = {0}, Value = {1}", key.Key, key.Value);
                                }
                                Console.WriteLine(name);
                                Console.WriteLine(Global.peerTracker[name]);
                                Console.WriteLine(rec + " REC");*/
                                ((Peer)Global.peers.children[Global.peerTracker[name]]).position = new Vector2(float.Parse(recSplit[1]), float.Parse(recSplit[2]));
                                ((Peer)Global.peers.children[Global.peerTracker[name]]).rotation = float.Parse(recSplit[3]);

                                break;
                            case "hit":
                                //death code
                                break;
                            case "intro":
                                Console.WriteLine(recSplit[1] + " REC1");
                                Console.WriteLine(recSplit[2] + " REC2");

                                Thread client = new Thread(() => Connector(int.Parse(recSplit[2]), recSplit[1].Trim(), Global.name, true));
                                client.Start();
                                break;
                        }
                    }
                    catch
                    {
                        break;
                    }
                }
            }
        }
        private void Receiver(Socket socket, string name)
        {
            while (true)
            {
                Receive(socket, name);
            }
        }
        public void Listener(Socket socket)
        {
            byte[] data = new byte[1024];
            socket.Receive(data);
            byte[] send = Encoding.ASCII.GetBytes((Global.name) + "," + Global.host.ToString());
            Console.WriteLine("Got a name");
            string recName = Encoding.ASCII.GetString(data).ToLower();
            recName = recName.Trim(' ', '\n', '\0');
            string[] process = new string[2];
            process = recName.Split(',').ToArray<string>();
            socket.Send(send);
            Console.WriteLine("sent");

            tmpPeer = new Peer();
            tmpPeer.name = process[0].Trim();
            tmpPeer.port = int.Parse(process[1].Trim());
            tmpPeer.position = new Vector2(0, 0);
            Global.peers.AddChild(tmpPeer);
            Console.WriteLine(Global.peers.children.Count.ToString());
            Global.peerTracker.Add(recName, Global.peers.children.Count - 1);
            if (int.Parse(process[2].Trim()) != 1)
            {
                foreach (Socket s in sockets)
                {
                    s.Send(Encoding.ASCII.GetBytes("intro," + ((IPEndPoint)socket.RemoteEndPoint).Address.ToString() + "," + tmpPeer.port + "|"));
                }
            }
            Console.WriteLine("intro," + ((IPEndPoint)socket.RemoteEndPoint).Address.ToString() + "," + tmpPeer.port.ToString());
            //Receive(socket, recName);
            Thread client = new Thread(() => Client(8887, "localhost", socket, recName));
            Thread listener = new Thread(() => Receiver(socket, recName));
            listener.Start();
            client.Start();

            sockets.Add(socket);
        }

        /*
         * - Manages all of the P2P relations
         * - Ideally has nice functions to call for updating your position
         */

    }
}
