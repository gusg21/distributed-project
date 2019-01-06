using DistributedGame.Math;
using DistributedGame.Networking;
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
        /// <summary>
        /// The global ContentManager
        /// USE ONLY IN LoadContent()!
        /// </summary>
        public static ContentManager c;
        /// <summary>
        /// An interface with some convenient networking methods
        /// for talking to the Fun Network King
        /// </summary>
        public static FNK fnk;
        /// <summary>
        /// Global Graphics device, easily acessible
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
    }
}
