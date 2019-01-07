using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Humper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DistributedGame
{
    class Castle : ZObject
    {
        Texture2D texture;
        Vector2 position = new Vector2();
        IBox bbox;

        /// <summary>
        /// Represents a player's castle in-game. This should have no networking, if possible.
        /// Simply display logic.
        /// </summary>
        public Castle()
        {
            // Random position. temporary
            position.X = Global.r.random.Next(0, 100);
            position.Y = Global.r.random.Next(0, 100);
        }

        public override void LoadContent()
        {
            texture = Global.c.Load<Texture2D>("castle.png");

            bbox = Global.w.Create(position.X + 3, position.Y + texture.Height - 8, texture.Width - 6, 8);

            depthYOffset = texture.Height;
        }

        public override void Update(GameTime gameTime)
        {
            YZ(this, position);
        }

        public override void Draw(SpriteBatch batch)
        {
            batch.Draw(texture, position, Color.White);    
        }
    }
}