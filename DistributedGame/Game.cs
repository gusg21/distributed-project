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

        State currentState;

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
        }

        protected override void Initialize()
        {
            currentState = new PlayState();

            // Setup sprite batch
            batch = new SpriteBatch(GraphicsDevice);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            currentState.LoadContent();

            base.LoadContent();
        }

        /// <summary>
        /// Updates all objects
        /// </summary>
        /// <param name="gameTime">it's the game time</param>
        protected override void Update(GameTime gameTime)
        {
            currentState.Update(gameTime);

            base.Update(gameTime);
        }
        /// <summary>
        /// Draw draws all objects
        /// </summary>
        /// <param name="gameTime"> it's the game time</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.ForestGreen); //Makes the background not red. (clears and makes the background green)

            currentState.Draw(batch);

            base.Draw(gameTime);
        }
    }
}
