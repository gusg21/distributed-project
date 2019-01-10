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
        string username;

        /// <summary>
        /// Represents a player's castle in-game. This should have no networking, if possible.
        /// Simply display logic.
        /// </summary>
        public Castle(string username)
        {
            this.username = username;
        }

        public string FilterString(string input, string filter)
        {
            string @return = "";
            foreach (char letter in input)
            {
                @return += filter.Contains(letter) ? letter : '!';
            }
            return @return;
        }

        public override void LoadContent()
        {
            texture = Global.c.Load<Texture2D>("castle.png");

            depthYOffset = texture.Height;

            position.X = 1024 / 4 - texture.Width / 2;
            position.Y = 768 / 4 - texture.Height / 2;

            bbox = Global.w.Create(position.X + 9, position.Y + texture.Height - 20, texture.Width - 18, 20);
        }

        public override void Update(GameTime gameTime)
        {
            YZ(this, position);
        }

        public override void Draw(SpriteBatch batch)
        {
            batch.Draw(texture, position, Color.White);
            Vector2 textPosition = position;
            textPosition.Y -= 32;
            FontRenderer.RenderFont(batch, Global.font, username + "'s castle", textPosition, 2);
        }

        public override void Enter()
        {
            username = username.ToLower();
            username = FilterString(username, Global.font.glyphs);
        }

        public override void Leave()
        {
        }
    }
}