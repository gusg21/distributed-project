using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedGame
{
    class Player : GameObject
    {
        Texture2D texture;
        Vector2 position;
        Vector2 velocity;

        float moveSpeed = 3F;
        /// <summary>
        /// The player plays the game, game object it keeps track of our textures and whatnot
        /// </summary>
        public Player()
        {
            texture = new Texture2D(Global.g, 32, 32);

            byte[] colors = new byte[32 * 32 * 4];
            for (int i = 0; i < 32 * 32; i++)
            {
                colors[i * 4] = 0xFF;
                colors[i * 4 + 1] = 0x30;
                colors[i * 4 + 2] = 0x85;
                colors[i * 4 + 3] = 0xFF;
            }
            texture.SetData<byte>(colors);
        }
        /// <summary>
        /// turns a bool into an int, fun!
        /// </summary>
        /// <param name="x">the bool in question</param>
        /// <returns></returns>
        private int BoolToInt(bool x)
        {
            if (x)
                return 1;
            else
                return 0;
        }
        /// <summary>
        /// It updates the player.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(0).IsConnected) // Controller input
            {
                velocity = GamePad.GetState(0).ThumbSticks.Left * new Vector2(1, -1) * moveSpeed;
            } else // Keyboard input
            {
                velocity.X = BoolToInt(Keyboard.GetState().IsKeyDown(Keys.D)) - BoolToInt(Keyboard.GetState().IsKeyDown(Keys.A));
                velocity.Y = BoolToInt(Keyboard.GetState().IsKeyDown(Keys.S)) - BoolToInt(Keyboard.GetState().IsKeyDown(Keys.W));
                if (velocity != Vector2.Zero)
                    velocity.Normalize(); //Normalize the velocity so that nobody goes faster on diagonal
                velocity *= moveSpeed;
            }

            GamePad.SetVibration(0, 1, 1);

            position += velocity;
        }

        public override void Draw(SpriteBatch batch)
        {
            batch.Draw(texture, position, Color.White);
        }
    }
}
