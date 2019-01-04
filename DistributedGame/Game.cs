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

        GameObjectGroup objects;
        ZObjectGroup zObjects;
        Player player;

        /// <summary>
        /// Game, it contains the globals and what not.
        /// </summary>
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
            // Setup sprite batch
            batch = new SpriteBatch(GraphicsDevice);

            // Create objects
            objects = new GameObjectGroup();
            zObjects = new ZObjectGroup();
            objects.AddChild(zObjects);

            player = new Player();
            zObjects.AddChild(player);

            zObjects.AddChild(new Castle());

            base.Initialize();
        }

        protected override void LoadContent()
        {
            objects.LoadContent();

            base.LoadContent();
        }

        /// <summary>
        /// Updates all objects
        /// </summary>
        /// <param name="gameTime">it's the game time</param>
        protected override void Update(GameTime gameTime)
        {
            objects.Update(gameTime);
            zObjects.Sort();

            base.Update(gameTime);
        }
        /// <summary>
        /// Draw draws all objects
        /// </summary>
        /// <param name="gameTime"> it's the game time</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.ForestGreen); //Makes the background not red. (clears and makes the background green)

            batch.Begin();
            objects.Draw(batch);
            batch.End();

            base.Draw(gameTime);
        }
    }
}
