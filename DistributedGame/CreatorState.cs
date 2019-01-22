using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DistributedGame.Networking;
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
        Texture2D textbox;
        Texture2D Longtextbox;

        P2P peer;
        List<Vector2> boxBox = new List<Vector2>();
        List<string> boxValue = new List<string>();

        Font font;
        int target = 0;
        int[] maxLength = new int[3] { 5,5,39};
        private IEnumerable<Keys> lastPressedKey = new Keys[0];

        public CreatorState(Game game) : base(game)
        {
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
            Global.name = Global.r.RandomString(5);
            boxBox.Insert(0, new Vector2(110, 200));
            boxBox.Insert(1, new Vector2(410, 200));
            boxBox.Insert(2, new Vector2(335, 650));

            boxValue.Insert(0, "8888");
            boxValue.Insert(1, "8888");
            boxValue.Insert(2, "127.0.0.1");
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
            Longtextbox = Player.GenerateRectangle(width * 4, height, Color.White);
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
                P2P peer = new P2P();
                Global.host = int.Parse(boxValue[1]);
                Thread server = new Thread(() => peer.Listener(Global.host, Global.name));
                server.Start();
                Thread client = new Thread(() => peer.Client()); //Start the client, this is responcible for sending out all packets.
                client.Start();
                if (boxValue[0] != null && boxValue[0] != "")
                {
                    if (int.Parse(boxValue[0]) != 0)
                    {
                        Console.WriteLine("doing");
                        Thread connector = new Thread(() => peer.Connector(int.Parse(boxValue[0]), boxValue[2], Global.name)); //Start a new Connector connecing to the port of the first box and the IP of the third box
                        connector.Start();
                    }
                }
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
                    else if(key == Keys.OemPeriod)
                    {
                        boxValue[target] += ".";
                    }
                    else if (key == Keys.Tab)
                    {
                        target++;
                        target = target % boxBox.Count();
                    }
                    else
                    {
                        if(boxValue[target].Length < maxLength[target])
                        {
                            boxValue[target] = boxValue[target] + (string)key.ToString().Substring(key.ToString().Length - 1);
                        }
                    }
                }

            }
            Console.WriteLine(boxValue[target]);
            lastPressedKey = pressedKeys;
        }

        public override void Draw(SpriteBatch batch)
        {
            //Global.g.Clear(Color.Black);

            batch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone);
            batch.Draw(textbox, new Vector2(100, 200), null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
            batch.Draw(textbox, new Vector2(400, 200), null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
            batch.Draw(Longtextbox, new Vector2(325, 650), null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
            batch.Draw(texture, texturePosition, texture.Bounds, Color.White, 0, Vector2.Zero, 16, SpriteEffects.None, 0);
            FontRenderer.RenderFont(batch, font, "press the #a button #nto continue", new Vector2(500, 270));

            FontRenderer.RenderFont(batch, font, "press the #b button #nto reload the image", new Vector2(500, 400));
            


            FontRenderer.RenderFont(batch, font, "connect", new Vector2(100, 150), 4, Color.White);
            FontRenderer.RenderFont(batch, font, "host", new Vector2(400, 150), 4, Color.White);
            FontRenderer.RenderFont(batch, font, "ip", new Vector2(500, 600), 4, Color.White);
            FontRenderer.RenderFont(batch, font, boxValue[0], boxBox[0], 4, Color.Black);
            FontRenderer.RenderFont(batch, font, boxValue[1], boxBox[1], 4, Color.Black);
            FontRenderer.RenderFont(batch, font, boxValue[2], boxBox[2], 4, Color.Black);

            batch.End();
        }
    }
}
