using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedGame
{
    abstract class State : GameObject
    {
        private Game myGame;

        public State(Game game)
        {
            myGame = game;
        }
        /// <summary>
        /// Switches the gamestate
        /// </summary>
        /// <param name="to">new state</param>
        public void SwitchState(string to)
        {
            myGame.SwitchState(to);
        }

        public abstract override void Enter();
        public abstract override void Leave();
    }
}
