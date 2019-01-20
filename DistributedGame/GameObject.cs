using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedGame
{
    /// <summary>
    /// The basic object of the game
    /// </summary>
    abstract class GameObject
    {
        public abstract void LoadContent();
        public abstract void Enter();
        public abstract void Leave();
        /// <summary>
        /// Is called while running to update everything
        /// </summary>
        /// <param name="gameTime"></param>
        public abstract void Update(GameTime gameTime);
        /// <summary>
        /// Is called at the end to draw.
        /// </summary>
        /// <param name="batch"></param>
        public abstract void Draw(SpriteBatch batch);
    }
}
