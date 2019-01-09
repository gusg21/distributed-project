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

        public void SwitchState(string to)
        {
            myGame.SwitchState(to);
        }

        public abstract void Enter();
        public abstract void Leave();
    }
}
