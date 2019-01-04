using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedGame
{
    /// <summary>
    /// A GameObject with a z or depth value
    /// </summary>
    abstract class ZObject : GameObject
    {
        public float z = 0F;
    }
}
