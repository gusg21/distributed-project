using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedGame
{
    class GameObjectGroup : GameObject
    {
        public List<GameObject> children;

        /// <summary>
        /// A simple group of GameObjects
        /// </summary>
        public GameObjectGroup()
        {
            children = new List<GameObject>();
        }

        /// <summary>
        /// Add a GameObject to the group.
        /// </summary>
        /// <param name="child"></param>
        public void AddChild(GameObject child)
        {
            children.Add(child);
        }

        /// <summary>
        /// Call LoadContent() on the children
        /// </summary>
        public override void LoadContent()
        {
            foreach (GameObject child in children)
            {
                child.LoadContent();
            }
        }

        /// <summary>
        /// Call Update() on the children
        /// </summary>
        public override void Update(GameTime gameTime)
        {
            foreach (GameObject child in children)
            {
                child.Update(gameTime);
            }
        }

        /// <summary>
        /// Call Draw() on the children
        /// </summary>
        public override void Draw(SpriteBatch batch)
        {
            foreach (GameObject child in children)
            {
                child.Draw(batch);
            }
        }

        public override void Enter()
        {
            foreach (GameObject child in children)
            {
                child.Enter();
            }
        }

        public override void Leave()
        {
            foreach (GameObject child in children)
            {
                child.Leave();
            }
        }
    }
}
