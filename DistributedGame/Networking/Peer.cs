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
        public Vector2 position { get; set; } = Vector2.Zero;
        Vector2 velocity;
        Vector2 bboxOffset;
        //IBox bbox;
        public String name { get; set; } = "JOE";

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
                batch.Draw(texture, position, Color.White);
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
