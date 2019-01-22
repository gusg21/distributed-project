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
        /// A group of ZObjects
        /// </summary>
        public ZObjectGroup()
        {
            children = new List<ZObject>();
        }

        /// <summary>
        /// Add a ZObject to this group.
        /// </summary>
        /// <param name="child"></param>
        public void AddChild(ZObject child)
        {
            children.Add(child);
        }
        public void RemoveChild(ZObject child)
        {
            children.Remove(child);
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
            for (int i = 0; i < children.Count(); i++)
            {
                ZObject child = children[i];
                child.LoadContent();
            }
        }

        /// <summary>
        /// Call Update() on the children
        /// </summary>
        public override void Update(GameTime gameTime)
        {
            for (int i = 0; i < children.Count(); i++)
            {
                ZObject child = children[i];
                child.Update(gameTime);
            }
        }


        /// <summary>
        /// Call Draw() on the children
        /// </summary>
        public override void Draw(SpriteBatch batch)
        {
            for (int i = 0; i < children.Count(); i++)
            {
                ZObject child = children[i];
                child.Draw(batch);
            }
        }

        public override void Enter()
        {
            for (int i = 0; i < children.Count(); i++)
            {
                ZObject child = children[i];
                child.Enter();
            }
        }

        public override void Leave()
        {
            for (int i = 0; i < children.Count(); i++)
            {
                ZObject child = children[i];
                child.Leave();
            }
        }
    }
}
