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
        Dictionary<string, State> states;

        /// <summary>
        /// Game, it contains the globals and what not.
        /// </summary>
        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 768;
            graphics.ApplyChanges();

            Content.RootDirectory = "Content";

            // Literally the worst design pattern
            Global.c = Content;
            Global.g = GraphicsDevice;
            Global.game = this;

            states = new Dictionary<string, State>();
            states.Add("play", new PlayState(this));
            states.Add("creator", new CreatorState(this));
            SwitchState("creator");
        }

        protected override void Initialize()
        {
            // Setup sprite batch
            batch = new SpriteBatch(GraphicsDevice);

            IsMouseVisible = true;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            foreach (State state in states.Values)
            {
                state.LoadContent();
            }

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
            currentState.Draw(batch);

            base.Draw(gameTime);
        }

        public void SwitchState(string to)
        {
            if (currentState != null)
            {
                currentState.Leave();
            }
            currentState = states[to];
            currentState.Enter();
        }
    }
}
