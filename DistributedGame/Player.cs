using Humper;
using Humper.Responses;
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
    class Player : ZObject
    {
        Texture2D texture;
        public Vector2 position { get; private set; } = Vector2.Zero;
        Vector2 velocity;
        Vector2 bboxOffset;
        IBox bbox; 

        float moveSpeed = 3F;

        /// <summary>
        /// The player plays the game, game object it keeps track of our textures and whatnot
        /// </summary>
        public Player()
        {
            
        }

        /// <summary>
        /// Load the texture and stuff. We make our
        /// own texture (see constructor)
        /// </summary>
        public override void LoadContent()
        {
            position = new Vector2(20, 20);

            texture = new Texture2D(Global.g, 16, 16);
            byte[] colors = new byte[16 * 16 * 4];
            for (int i = 0; i < 16 * 16; i++)
            {
                colors[i * 4] = 0x34;
                colors[i * 4 + 1] = 0xED;
                colors[i * 4 + 2] = 0x01;
                colors[i * 4 + 3] = 0xFF;
            }
            texture.SetData(colors);
        }

        /// <summary>
        /// Call to set the player up with the texture created in the
        /// creation state.
        /// </summary>
        public void SetupCreatedTexture()
        {
            texture = Global.playerTexture;

            depthYOffset = texture.Height;

            bboxOffset = new Vector2(3, texture.Height - 6);
            bbox = Global.w.Create(position.X + bboxOffset.X, position.Y + bboxOffset.Y, texture.Width - 6, 6);
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

            // Example vibration
            //GamePad.SetVibration(0, 1, 1);

            position += velocity;

            YZ(this, position);

            if (bbox != null)
            {
                IMovement result = bbox.Move(position.X + bboxOffset.X, position.Y + bboxOffset.Y, (collision) => CollisionResponses.Slide);
                position = new Vector2(result.Destination.X - bboxOffset.X, result.Destination.Y - bboxOffset.Y);
            }
        }

        public override void Draw(SpriteBatch batch)
        {
            if (texture != null)
            {
                batch.Draw(texture, position, Color.White);
            }
        }

        public override void Enter()
        {
            Console.WriteLine("Enter");
            SetupCreatedTexture();
        }

        public override void Leave()
        {
        }
    }
}
