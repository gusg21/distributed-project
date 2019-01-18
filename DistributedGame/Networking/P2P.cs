﻿
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DistributedGame.Networking
{
    public class P2P
    {
        Peer tmpPeer;
        public void Server(int connectPort, string connectIp, int hostPort, string name)
        {
            TcpListener listener = new TcpListener(hostPort);
            Console.WriteLine("Up");
            listener.Start();
            Socket socket = listener.AcceptSocket();
            Console.WriteLine("New Connection Found");
            Stream networkStream = new NetworkStream(socket);
            byte[] send = System.Text.Encoding.ASCII.GetBytes((name));
            socket.Send(send);
            byte[] data = new byte[1024];
            socket.Receive(data);
            String recName = System.Text.Encoding.ASCII.GetString(data).ToLower();
            Console.WriteLine(recName);
            recName.Trim();
            byte[] data2 = new byte[1024];
            socket.Receive(data2);
            String rec = System.Text.Encoding.ASCII.GetString(data2);
            Console.WriteLine(rec);
            String[] recPos = rec.Split(',');
            Console.WriteLine(recPos);

            tmpPeer = new Peer();
            tmpPeer.name = recName;
            tmpPeer.position = new Vector2(float.Parse(recPos[0]), float.Parse(recPos[1]));
            Global.peers.AddChild(tmpPeer);
            Global.peerTracker.Add(recName, Global.peers.children.Count - 1);

            Thread listen = new Thread(() => Listener(8887, "localhost", socket, recName));
            listen.Start();
            Thread client = new Thread(() => Client(8887, "localhost", socket, recName));
            client.Start();
        }

        public void Connector(int connectPort, string connectIP, string name)
        {
            if(connectIP == "localhost")
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
                byte[] send = System.Text.Encoding.ASCII.GetBytes((name));
                connect.Send(send);
                byte[] data = new byte[1024];
                connect.Receive(data);
                String recName = System.Text.Encoding.ASCII.GetString(data).ToLower();
                Console.WriteLine(recName);
                recName.Trim();
                byte[] data2 = new byte[1024];
                connect.Receive(data2);
                String rec = System.Text.Encoding.ASCII.GetString(data2);
                Console.WriteLine(rec);
                String[] recPos = rec.Split(',');
                Console.WriteLine(recPos);
                tmpPeer = new Peer();
                tmpPeer.name = recName;
                tmpPeer.position = new Vector2(float.Parse(recPos[0]), float.Parse(recPos[1]));
                Global.peers.AddChild(tmpPeer);
                Global.peerTracker.Add(recName, Global.peers.children.Count - 1);

                Thread listen = new Thread(() => Listener(8887, "localhost", connect, recName));
                listen.Start();
                Thread client = new Thread(() => Client(8887, "localhost", connect, recName));
                client.Start();
            /*}
            catch
            {*/
                Console.WriteLine("It broke.");
            //}
        }

        public void Client(int connectPort, string connectIP, Socket socket, string name)
        {
            byte[] send2 = new byte[1024];
            while (true)
            {
                send2 = System.Text.Encoding.ASCII.GetBytes("xy," + (Global.p.position.X.ToString() + "," + Global.p.position.Y.ToString()) + "\n");
                socket.Send(send2);
                //Thread.Sleep(1000);
            }
        }
        public void Listener(int connectPort, string connectIP, Socket socket, string name)
        {
            Console.WriteLine("Started Listening ");
            while (2+2 == 4)
            {
                byte[] data = new byte[1024];
                socket.Receive(data);
                String rec = System.Text.Encoding.ASCII.GetString(data);
                String[] recSplit = rec.Split(',');
                switch (recSplit[0])
                {
                    case "xy":
                        foreach (KeyValuePair<string, int> key in Global.peerTracker)
                        {
                            Console.WriteLine("Key = {0}, Value = {1}", key.Key, key.Value);
                        }
                        Console.WriteLine(name);
                        Console.WriteLine(Global.peerTracker[name]);
                        ((Peer)Global.peers.children[Global.peerTracker[name]]).position = new Vector2(float.Parse(recSplit[1]), float.Parse(recSplit[2]));
                        break;
                }
            }
        }
        /*
         * - Manages all of the P2P relations
         * - Ideally has nice functions to call for updating your position
         */

    }
}
