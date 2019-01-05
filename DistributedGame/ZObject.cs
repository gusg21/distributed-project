using Microsoft.Xna.Framework;
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
        public float depthYOffset = 0F;

        public static void YZ(ZObject target, Vector2 position) => target.z = position.Y + target.depthYOffset;
    }
}
