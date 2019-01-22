using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using DistributedGame.Maths;
using DistributedGame.Networking;
using Humper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DistributedGame
{
    class PlayState : State
    {
        GameObjectGroup objects;
        ZObjectGroup zObjects;
        Player player;
        World physicsWorld;
        Camera camera;
        P2P peer;
        bool lastTil = false;
        bool hasDied = false;
        double timeOfDeath;
        string toChat = "";
        SuperRandom random = new SuperRandom();
        private IEnumerable<Keys> lastPressedKey = new Keys[0];
        public PlayState(Game game) : base(game)
        {
            physicsWorld = new World(Global.g.PresentationParameters.BackBufferWidth, Global.g.PresentationParameters.BackBufferHeight);
            Global.w = physicsWorld;

            // Create objects
            objects = new GameObjectGroup();
            zObjects = new ZObjectGroup();
            objects.AddChild(zObjects);

            player = new Player();
            zObjects.AddChild(player);
            Global.p = player;


            camera = new Camera(Global.g.PresentationParameters.BackBufferWidth, Global.g.PresentationParameters.BackBufferHeight, Vector2.Zero)
            {
                Zoom = 2F
            };
            camera.CenterOn(new Vector2(1024 / 4, 768 / 4));
            objects.AddChild(camera);
            Global.cam = camera;

            zObjects.AddChild(new Castle("gusg21"));

            Global.peers = new ZObjectGroup();
            objects.AddChild(Global.peers);
        }

        public override void Enter()
        {
            objects.Enter();
        }

        public override void Leave()
        {
            objects.Leave();
        }

        public override void LoadContent()
        {
            objects.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState(0).IsKeyDown(Keys.OemTilde))
            {
                if (lastTil == false)
                {
                    Global.isTyping = !Global.isTyping;
                    lastTil = true;
                }
            }
            else
            {
                lastTil = false;
            }
            if (Global.isDead)
            {
                if (!hasDied)
                {
                    timeOfDeath = gameTime.TotalGameTime.TotalSeconds;
                    hasDied = true;
                    Global.p.position = new Vector2(-100, -100);
                }
                if(gameTime.TotalGameTime.TotalSeconds - timeOfDeath > 5)
                {
                    hasDied = false;
                    Global.isDead = false;
                    Global.p.position = new Vector2(random.RandomRange(0,500), random.RandomRange(0, 380));
                }
            }
            if (Global.isTyping)
            {
                KeyboardState kbState = Keyboard.GetState();
                Keys[] pressedKeys = kbState.GetPressedKeys();

                foreach (Keys key in lastPressedKey)
                {
                    if (!pressedKeys.Contains(key))
                    {
                        if (key == Keys.Back || key == Keys.Delete)
                        {
                            if (toChat.Length > 0)
                            {
                                toChat = toChat.Substring(0, toChat.Length - 1);
                            }
                        }
                        else if (key == Keys.Space)
                        {
                            toChat += " ";
                        }
                        else if (key == Keys.Enter)
                        {
                            Global.chat.Add(new Message("You say: " + toChat));
                            Global.packets.Add(new Packet("chat", new List<string>() { toChat }));

                            toChat = "";
                            Global.isTyping = false;
                        }
                        else if (key != Keys.OemTilde)
                        {
                            toChat = toChat + (string)key.ToString().ToLower().Substring(key.ToString().Length - 1);
                        }
                    }

                }
                Console.WriteLine(toChat);
                lastPressedKey = pressedKeys;
            }
            objects.Update(gameTime);
            zObjects.Sort();
        }
        public void addPeer(Peer p)
        {
            zObjects.AddChild(p);
        }
        public override void Draw(SpriteBatch batch)
        {
            int chatOffset = 55;
            Global.g.Clear(Color.ForestGreen); //Makes the background not red. (clears and makes the background green)

            batch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone, null, camera.TranslationMatrix);
            objects.Draw(batch);
            while (Global.chat.Count() > 4)
            {
                Global.chat.RemoveAt(0);
            }
            for (int i = Global.chat.Count(); i >  Global.chat.Count(); i--)
            {
                Message msg = Global.chat[i];
                FontRenderer.RenderFont(batch, Global.font, msg.text.ToLower(), new Vector2(0, 65 - chatOffset), 1, msg.GetColor());
                chatOffset -= 10;

            }
            FontRenderer.RenderFont(batch, Global.font, toChat, new Vector2(0, 0), 1, Color.White);
            batch.End();
        }
    }
}
