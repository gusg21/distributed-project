using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Humper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DistributedGame
{
    class Peer : ZObject
    {
        Texture2D texture;
        Texture2D lanceTex;
        public Vector2 position { get; set; } = Vector2.Zero;
        public Socket socket { get; set; }
        Vector2 velocity;
        public int port { get; set; }

        public Rectangle bbox { get
            {
                Rectangle bounds = texture.Bounds;
                bounds.Offset(
                    Player.Vector2ToPoint(
                        position
                        )
                     - new Point(8, 8));
                return bounds;
            } } // gets peer's bounding box
        Vector2 bboxOffset;
        //IBox bbox;
        public string name { get; set; } = "JOE";
        public float rotation;

        float moveSpeed = 3F;

        public Peer()
        {
            LoadContent();
        }

        public override void Leave()
        {
        }

        public override void LoadContent()
        {
            position = new Vector2(20, 20);

            texture = Player.GenerateRectangle(16, 16, Color.Cyan);
            lanceTex = Player.GenerateRectangle(24, 2, Color.Tomato);
        }
        public void SetupCreatedTexture()
        {
            texture = Global.playerTexture;

            depthYOffset = texture.Height;

            bboxOffset = new Vector2(3, texture.Height - 6);
            //bbox = Global.w.Create(position.X + bboxOffset.X, position.Y + bboxOffset.Y, texture.Width - 6, 6);
        }
        public override void Update(GameTime gameTime)
        {
            // Example vibration
            //GamePad.SetVibration(0, 1, 1);

            //position += velocity;

            YZ(this, position);
            position = new Vector2(position.X, position.Y);
            /*if (bbox != null)
            {
                IMovement result = bbox.Move(position.X + bboxOffset.X, position.Y + bboxOffset.Y, (collision) => CollisionResponses.Slide);
                
            }*/
        }

        public override void Draw(SpriteBatch batch)
        {
            if (texture != null)
            {
                batch.Draw(texture, position, texture.Bounds, Color.White,rotation,new Vector2(texture.Bounds.Center.X, texture.Bounds.Center.Y), 1, SpriteEffects.None, 0);
                batch.Draw(lanceTex, position, lanceTex.Bounds, Color.White, rotation, new Vector2(0, 0), 1, SpriteEffects.None, 0);
            }
            Vector2 textPosition = position;
            textPosition.Y -= 32;
            FontRenderer.RenderFont(batch, Global.font, name, textPosition, 2);
        }

        public override void Enter()
        {
            Console.WriteLine("Enter");
            SetupCreatedTexture();
        }
    }
}
