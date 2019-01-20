using DistributedGame.Maths;
using DistributedGame.Networking;
using Humper;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedGame
{
    static class Global
    {
        public static Camera cam;
        /// <summary>
        /// The global ContentManager
        /// USE ONLY IN LoadContent()!
        /// </summary>
        public static ContentManager c;
        /// <summary>
        /// The main pixel font
        /// </summary>
        public static Font font;
        /// <summary>
        /// An interface with some convenient networking methods
        /// for talking to the Fun Network King
        /// </summary>
        public static FNK fnk;
        /// <summary>
        /// Global Graphics device, easily accessible
        /// </summary>
        public static GraphicsDevice g;
        /// <summary>
        /// The local player
        /// </summary>
        public static Player p;
        /// <summary>
        /// The main RNG
        /// </summary>
        public static SuperRandom r = new SuperRandom();
        /// <summary>
        /// The texture of the player
        /// </summary>
        public static Texture2D playerTexture;
        /// <summary>
        /// Physics World
        /// </summary>
        public static World w;
        public static string name;
        /// <summary>
        /// peers
        /// </summary>
        public static ZObjectGroup peers;
        public static Dictionary<string, int> peerTracker = new Dictionary<string, int>();
        public static List<Packet> packets = new List<Packet>();
        public static int host;
    }
}
