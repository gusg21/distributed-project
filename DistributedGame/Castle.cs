using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DistributedGame
{
    class Castle : GameObject
    {
        Texture2D texture;
        Vector2 position = new Vector2();

        public Castle()
        {
            position.X = Global.r.random.Next(0, 100);
            position.Y = Global.r.random.Next(0, 100);
        }

        public override void LoadContent()
        {
            texture = Global.c.Load<Texture2D>("castle.png");
        }

        public override void Update(GameTime gameTime)
        {
            
        }

        public override void Draw(SpriteBatch batch)
        {
            batch.Draw(texture, position, Color.White);    
        }
    }
}