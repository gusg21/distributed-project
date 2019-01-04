using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DistributedGame
{
    class ZObjectGroup : GameObject
    {
        public List<ZObject> children;

        /// <summary>
        /// Add a ZObject to this group.
        /// </summary>
        /// <param name="child"></param>
        public void AddChild(ZObject child)
        {
            children.Add(child);
        }

        private static int InternalSorter(ZObject a, ZObject b)
        {
            if (a.z > b.z)
                return 1;
            else if (a.z < b.z)
                return -1;
            else
                return 0;
        }

        /// <summary>
        /// Sort the children by their Z.
        /// </summary>
        public void Sort()
        {
            children.Sort(InternalSorter);
        }

        /// <summary>
        /// Call LoadContent() on the children
        /// </summary>
        public override void LoadContent()
        {
            foreach (ZObject child in children)
            {
                child.LoadContent();
            }
        }

        /// <summary>
        /// Call Update() on the children
        /// </summary>
        public override void Update(GameTime gameTime)
        {
            foreach (ZObject child in children)
            {
                child.Update(gameTime);
            }
        }

        /// <summary>
        /// Call Draw() on the children
        /// </summary>
        public override void Draw(SpriteBatch batch)
        {
            foreach (ZObject child in children)
            {
                child.Draw(batch);
            }
        }
    }
}
