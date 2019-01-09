using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public PlayState(Game game) : base(game)
        {
            physicsWorld = new World(640, 480);
            Global.w = physicsWorld;

            // Create objects
            objects = new GameObjectGroup();
            zObjects = new ZObjectGroup();
            objects.AddChild(zObjects);

            player = new Player();
            zObjects.AddChild(player);
            Global.p = player;

            zObjects.AddChild(new Castle());
        }

        public override void Enter()
        {
            
        }

        public override void Leave()
        {
            
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

        public override void Draw(SpriteBatch batch)
        {
            Global.g.Clear(Color.ForestGreen); //Makes the background not red. (clears and makes the background green)

            batch.Begin();
            objects.Draw(batch);
            batch.End();
        }
    }
}
