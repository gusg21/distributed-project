﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedGame
{
    abstract class GameObject
    {
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch batch);
    }
}
