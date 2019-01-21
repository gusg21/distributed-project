
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
    /// <summary>
    /// Handles all of the inter-peer networking
    /// </summary>
    public class P2P
    {
        Peer tmpPeer;
        //Packet positionPacket = new Packet("pos"); //A simple packet to update position
        List<Socket> sockets = new List<Socket>();


        /// <summary>
        /// Listener listens on the host port for any incoming connections and creates the socket before passing it off to the Handshaker.
        /// </summary>
        /// <param name="hostPort"></param>
        /// <param name="name"></param>
        public void Listener(int hostPort, string name)
        {
            TcpListener listener = new TcpListener(hostPort);
            while (3 == 3)
            {
                Console.WriteLine("Up");
                try
                {
                    listener.Start();

                    Socket socket = listener.AcceptSocket();
                    Console.WriteLine("New Connection Found");
                    Stream networkStream = new NetworkStream(socket);
                    Console.WriteLine("netstream");

                    Thread server = new Thread(() => Handshaker(socket));
                    server.Start(); ; //Thread this
                }
                catch
                {
                    hostPort++;
                    Console.WriteLine("port taken trying " + hostPort.ToString());

                    listener = new TcpListener(hostPort);
                }

            }
        }
        /// <summary>
        /// The connector handles connecting to a new peer as well as the inital handshake between them.
        /// </summary>
        /// <param name="connectPort">The port to connect on</param>
        /// <param name="connectIP">The IP to connect to</param>
        /// <param name="name">The name to introduce ones self as</param>
        /// <param name="known">Have we been refered to this peer by an existing freind?</param>
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
            tmpPeer.socket = connect;

            Global.peers.AddChild(tmpPeer);

            Global.peerTracker.Add(recName, Global.peers.children.Count - 1);
            Thread listener = new Thread(() => Receiver(connect, recName));
            //Thread client = new Thread(() => Client());

            listener.Start();
           // client.Start();
            //sockets.Add(connect);
            /*}
            catch
            {*/
            //Console.WriteLine("It broke.");
            //}
        }
        /// <summary>
        /// The Client sends all packets waiting to be sent.
        /// </summary>
        /// 
        public void Client()
        {
            while (true)
            {
                while(Global.packets.Count > 0)
                {
                    Packet packet = Global.packets[0];
                    ///byte[] send2 = new byte[1024];
                    for(int i = 0; i < Global.peers.children.Count(); i++)
                    {
                        Socket sock = ((Peer)Global.peers.children[i]).socket;
                        Send((Peer)(Global.peers.children[i]), packet);
                    }
                    Global.packets.RemoveAt(0);
                }
                //Send(socket, positionPacket);
                Thread.Sleep(1);

            }
        }
        /// <summary>
        /// Send does the parsing of the socket, determines what kind of packet it is and processes it before sending.
        /// </summary>
        /// <param name="socket">The socket to send on</param>
        /// <param name="packet">The Packet to be sent</param>
        private void Send(Peer peer, Packet packet)
        {
            byte[] send2 = new byte[1024];
            string processString = "";
            Socket socket = peer.socket;
            switch (packet.send)
            {
                case "pos":
                case "position":
                case "xy":
                    processString = "xy," + (packet.value[0] + "," + packet.value[1] + "," + packet.value[2]);
                    //Console.WriteLine(Encoding.ASCII.GetString(send2));
                    break;
                case "hit":
                    processString = "hit";
                    break;
                case "intro":
                    processString = "intro," + packet.value[0].ToString().Trim('(',')')+ "," + packet.value[1].ToString().Trim('(', ')') + "," + packet.value[2];
                    break;
            }
            if (packet.end)
            {
                processString = processString + ",end";
            }
            processString = processString + "|";
            send2 = Encoding.ASCII.GetBytes(processString);
            socket.Send(send2);
        }
        /// <summary>
        /// Receive on socket and process data for a named peer.
        /// </summary>
        /// <param name="socket">The socket to receive on</param>
        /// <param name="name">The name of the Peer you are processing for</param>
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
                                if (recSplit[3] != Global.name)
                                {
                                    Thread client = new Thread(() => Connector(int.Parse(recSplit[2]), recSplit[1].Trim(), Global.name, true));
                                    client.Start();
                                }
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
        /// <summary>
        /// A loop for the Receive function.
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="name"></param>
        private void Receiver(Socket socket, string name)
        {
            while (true)
            {
                Receive(socket, name);
            }
        }
        /// <summary>
        /// Preforms the inital steps for establishing a connection.
        /// </summary>
        /// <param name="socket"></param>
        public void Handshaker(Socket socket)
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
            tmpPeer.socket = socket;
            Console.WriteLine(Global.peers.children.Count.ToString());
            Global.peerTracker.Add(recName, Global.peers.children.Count - 1);
            if (int.Parse(process[2].Trim()) != 1) //Checking if the connecting peer "knows" the listener (was directed to connect by someone else, rather than a new connection)
            {
                Global.packets.Add(new Packet("intro", new List<string> { ((IPEndPoint)socket.RemoteEndPoint).Address.ToString(), tmpPeer.port.ToString(), tmpPeer.name }));
                    //"intro," + ((IPEndPoint)socket.RemoteEndPoint).Address.ToString() + "," + tmpPeer.port + "|"
            }
            Console.WriteLine("intro," + ((IPEndPoint)socket.RemoteEndPoint).Address.ToString() + "," + tmpPeer.port.ToString() + "," + process[2].ToString().Trim());
            //Receive(socket, recName);
            Thread listener = new Thread(() => Receiver(socket, recName));
            listener.Start();

            //sockets.Add(socket);
        }

        /*
         * - Manages all of the P2P relations
         * - Ideally has nice functions to call for updating your position
         */

    }
}
