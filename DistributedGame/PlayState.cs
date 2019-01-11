using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DistributedGame.Networking;
using Humper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
            P2P peer = new P2P();
            Thread server = new Thread(() => peer.Server(8887,"localhost", 8888, "gusg21"));
            server.Start();

            camera = new Camera(Global.g.PresentationParameters.BackBufferWidth, Global.g.PresentationParameters.BackBufferHeight, Vector2.Zero)
            {
                Zoom = 2F
            };
            camera.CenterOn(new Vector2(1024 / 4, 768 / 4));
            objects.AddChild(camera);

            zObjects.AddChild(new Castle("gusg21"));
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
            objects.Update(gameTime);
            zObjects.Sort();
        }
        public void addPeer(Peer p)
        {
            zObjects.AddChild(p);
        }
        public override void Draw(SpriteBatch batch)
        {
            Global.g.Clear(Color.ForestGreen); //Makes the background not red. (clears and makes the background green)

            batch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone, null, camera.TranslationMatrix);
            objects.Draw(batch);
            batch.End();
        }
    }
}
