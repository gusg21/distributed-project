using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
<<<<<<< HEAD
using System.Threading.Tasks;
=======
using System.Threading;
using System.Threading.Tasks;
using DistributedGame.Networking;
>>>>>>> 900f7c42b917b21d346c07afc14a3672aaac9c87
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DistributedGame
{
    class CreatorState : State
    {
        Texture2D texture;
        Vector2 texturePosition;
<<<<<<< HEAD

        Font font;

        public CreatorState(Game game) : base(game)
        {

=======
        Texture2D textbox;

        P2P peer;
        List<Vector2> boxBox = new List<Vector2>();
        List<string> boxValue = new List<string>();

        Font font;
        int target = 0;
        int maxLength = 5;
        private IEnumerable<Keys> lastPressedKey = new Keys[0];

        public CreatorState(Game game) : base(game)
        {
>>>>>>> 900f7c42b917b21d346c07afc14a3672aaac9c87
        }

        public void LoadDefaultImage()
        {
            texture = Global.c.Load<Texture2D>("defaultplayer.png");
        }

        public void ReloadImage()
        {
            try
            {
                texture = Global.c.Load<Texture2D>("player.png");
            }
            catch (ContentLoadException)
            {
                LoadDefaultImage();
            }

            if (texture.Width != 16 || texture.Height != 16)
            {
                LoadDefaultImage();
            }
        }

        public override void LoadContent()
        {
            ReloadImage();

            texturePosition = new Vector2(100, (Global.g.PresentationParameters.BackBufferHeight - texture.Height) / 3);

            font = new Font(Global.c.Load<Texture2D>("fonts/font1.png"), "abcdefghijklmnopqrstuvwxyz1234567890:!' ", 4, 8);
            Global.font = font;
<<<<<<< HEAD
=======


            boxBox.Insert(0, new Vector2(100, 200));
            boxBox.Insert(1, new Vector2(400, 200));
            boxValue.Insert(0, "8888");
            boxValue.Insert(1, "8888");
            int width = 100;
            int height = 50;
            byte[] colors = new byte[width * height * 4];
            for (int i = 0; i < width * height; i++)
            {
                colors[i * 4] = Color.White.R;
                colors[i * 4 + 1] = Color.White.G;
                colors[i * 4 + 2] = Color.White.B;
                colors[i * 4 + 3] = Color.White.A;
            }

            textbox = new Texture2D(Global.g, width, height);
            textbox.SetData(colors);
>>>>>>> 900f7c42b917b21d346c07afc14a3672aaac9c87
        }

        public override void Enter()
        {
               
        }

        public override void Leave()
        {
            
        }

        public void SubmitTexture()
        {
            // submit the player's texture
            Global.playerTexture = texture;
            SwitchState("play");
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Z))
            {
<<<<<<< HEAD
=======
                P2P peer = new P2P();
                Thread server = new Thread(() => peer.Server(0000, "localhost", int.Parse(boxValue[1]), "gusg21"));
                server.Start();
                if(int.Parse(boxValue[0]) != 0)
                {
                    Console.WriteLine("doing");
                    Thread client = new Thread(() => peer.Connector(int.Parse(boxValue[0]), "localhost", "gusg21"));
                    client.Start();
                }
>>>>>>> 900f7c42b917b21d346c07afc14a3672aaac9c87
                SubmitTexture();
            }
            else if (GamePad.GetState(PlayerIndex.One).IsConnected && GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.A))
            {
                SubmitTexture();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.X))
            {
                ReloadImage();
            }
<<<<<<< HEAD
=======
            KeyboardState kbState = Keyboard.GetState();
            Keys[] pressedKeys = kbState.GetPressedKeys();

            foreach (Keys key in lastPressedKey)
            {
                if (!pressedKeys.Contains(key))
                {
                    if (key == Keys.Back || key == Keys.Delete)
                    {
                        if (boxValue[target].Length > 0)
                        {
                            boxValue[target] = boxValue[target].Substring(0, boxValue[target].Length - 1);
                        }
                    }
                    else if (key == Keys.Tab)
                    {
                        target++;
                        target = target % boxBox.Count();
                    }
                    else
                    {
                        if(boxValue[target].Length < maxLength)
                        {
                            boxValue[target] = boxValue[target] + (string)key.ToString().Substring(key.ToString().Length - 1);
                        }
                    }
                }

            }
            Console.WriteLine(boxValue[target]);
            lastPressedKey = pressedKeys;
>>>>>>> 900f7c42b917b21d346c07afc14a3672aaac9c87
        }

        public override void Draw(SpriteBatch batch)
        {
<<<<<<< HEAD
            
            batch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone);
            batch.Draw(texture, texturePosition, texture.Bounds, Color.White, 0, Vector2.Zero, 16, SpriteEffects.None, 0);
            FontRenderer.RenderFont(batch, font, "press the #a button #nto continue", new Vector2(500, 270));
            FontRenderer.RenderFont(batch, font, "press the #b button #nto reload the image", new Vector2(500, 400));
=======
            Global.g.Clear(Color.Black);

            batch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone);
            batch.Draw(texture, texturePosition, texture.Bounds, Color.White, 0, Vector2.Zero, 16, SpriteEffects.None, 0);
            FontRenderer.RenderFont(batch, font, "press the #a button #nto continue", new Vector2(500, 270));

            FontRenderer.RenderFont(batch, font, "press the #b button #nto reload the image", new Vector2(500, 400));
            batch.Draw(textbox, new Vector2(100, 200), Color.White);
            batch.Draw(textbox, new Vector2(400, 200), Color.White);
            FontRenderer.RenderFont(batch, font, boxValue[0], boxBox[0], 4, Color.Black);
            FontRenderer.RenderFont(batch, font, boxValue[1], boxBox[1], 4, Color.Black);
            FontRenderer.RenderFont(batch, font, "connect", new Vector2(100, 150), 4, Color.White);
            FontRenderer.RenderFont(batch, font, "host", new Vector2(400, 150), 4, Color.White);

>>>>>>> 900f7c42b917b21d346c07afc14a3672aaac9c87
            batch.End();
        }
    }
}
