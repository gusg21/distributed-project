using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DistributedGame
{
    class Game : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch batch;

        List<GameObject> objects;
        Player player;

        public Game()
        {
            graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";

            // Literally the worst design pattern
            Global.c = Content;
            Global.g = GraphicsDevice;
            Global.p = player;
        }

        protected override void Initialize()
        {
            batch = new SpriteBatch(GraphicsDevice);

            objects = new List<GameObject>();

            player = new Player();
            objects.Add(player);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            foreach (GameObject @object in objects)
            {
                @object.Update(gameTime);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.ForestGreen);

            batch.Begin();
            foreach (GameObject @object in objects)
            {
                @object.Draw(batch);
            }
            batch.End();

            base.Draw(gameTime);
        }
    }
}
