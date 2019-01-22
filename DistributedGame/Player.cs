using DistributedGame.Networking;
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
        Texture2D lanceTexture;
        Vector2 lanceOffset = new Vector2(8, 7);
        public Vector2 position { get; private set; } = Vector2.Zero;
        Vector2 velocity;
        Vector2 bboxOffset;
        public float rotation;
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

            texture = GenerateRectangle(16, 16, Color.Aquamarine);
            lanceTexture = GenerateRectangle(24, 2, Color.Tomato);
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
            if (!Global.isTyping)
            {
                if (GamePad.GetState(0).IsConnected) // Controller input
                {
                    velocity = GamePad.GetState(0).ThumbSticks.Left * new Vector2(1, -1) * moveSpeed;
                }
                else // Keyboard input
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
            }
            if (bbox != null)
            {
                IMovement result = bbox.Move(position.X + bboxOffset.X, position.Y + bboxOffset.Y, (collision) => CollisionResponses.Slide);
                position = new Vector2(result.Destination.X - bboxOffset.X, result.Destination.Y - bboxOffset.Y);
            }

            rotation = (float) Math.Atan2(Mouse.GetState().Y / Global.cam.Zoom - position.Y, Mouse.GetState().X / Global.cam.Zoom - position.X);
            Global.packets.Add(new Packet("pos", new List<string> { Math.Round(position.X, 2).ToString(), Math.Round(position.Y, 2).ToString(), Math.Round(rotation, 2).ToString() }));
            foreach (Peer peer in Global.peers.children)
            {
                if (peer.bbox.Intersects(LanceRect()))
                {
                    Global.packets.Add(new Packet("hit", new List<string>() { peer.name })); // ok just tell me dont edit
                }
            }
        }

        public Vector2 GetCenter()
        {
            return position + new Vector2(texture.Bounds.Center.X, texture.Bounds.Center.Y);
        }

        public Rectangle LanceRect()
        {
            Rectangle lanceBounds = lanceTexture.Bounds;
            lanceBounds.Offset(Vector2ToPoint(position));
            return lanceBounds;
        }

        public static Point Vector2ToPoint(Vector2 vec)
        {
            return new Point(
                   (int) vec.X, (int) vec.Y
                );
        }

        float VectorToAngle(Vector2 vector)
        {
            return (float)Math.Atan2(vector.Y, vector.X);
        }

        public override void Draw(SpriteBatch batch)
        {
            if (texture != null && lanceTexture != null)
            {
                batch.Draw(texture, position, texture.Bounds, Color.White, rotation, new Vector2(8, 8), 1, SpriteEffects.None, 0);
                batch.Draw(lanceTexture, position, lanceTexture.Bounds, Color.White, rotation, new Vector2(0, 1), 1, SpriteEffects.None, 0);
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

        public static Texture2D GenerateRectangle(int width, int height, Color color)
        {
            Texture2D texture = new Texture2D(Global.g, width, height);
            byte[] colors = new byte[width * height * 4];
            for (int i = 0; i < width * height; i++)
            {
                colors[i * 4] = color.R;
                colors[i * 4 + 1] = color.G;
                colors[i * 4 + 2] = color.B;
                colors[i * 4 + 3] = color.A;
            }
            texture.SetData(colors);

            return texture;
        }
    }
}
