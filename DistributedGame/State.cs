using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedGame
{
    abstract class State : GameObject
    {
        public abstract void Enter();
        public abstract void Leave();
    }
}
