using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedGame
{
    class Program
    {
        /// <summary>
        /// The Launcher for our game. Just makes a new Game object
        /// and Run()s it.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Game game = new Game();
            game.Run();
        }
    }
}
