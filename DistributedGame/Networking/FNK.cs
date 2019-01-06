using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedGame.Networking
{
    class FNK
    {
        // The position the FNK has for the player
        public Vector2 remotePosition;

        public Vector2 GetRenderPosition(Vector2 local)
        {
            // Return the midpoint between the position the FNK has
            // and the position the player should be at
            return (remotePosition - local) / 2;
        }
    }
}
