using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedGame
{
    class Camera : GameObject
    {
        // Centered Position of the Camera.
        public Vector2 Position { get; private set; }
        public float Zoom { get; set; }
        public float Rotation { get; private set; }

        // height and width of the viewport window which should adjust when the player resizes the game window.
        public int ViewportWidth { get; set; }
        public int ViewportHeight { get; set; }

        // Center of the Viewport does not account for scale
        public Vector2 ViewportCenter
        {
            get
            {
                return new Vector2(ViewportWidth * 0.5f, ViewportHeight * 0.5f);
            }
        }
        
        public Matrix TranslationMatrix
        {
            get
            {
                return Matrix.CreateTranslation(-(int)Position.X, -(int)Position.Y, 0) *
                   Matrix.CreateRotationZ(Rotation) *
                   Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
                   Matrix.CreateTranslation(new Vector3(ViewportCenter, 0));
            }
        }

        public Camera(int viewportWidth, int viewportHeight, Vector2 cameraPosition)
        {
            ViewportWidth = viewportWidth;
            ViewportHeight = viewportHeight;
            Position = cameraPosition;
            Zoom = 1.0f;
        }

        public override void LoadContent()
        {

        }

        public override void Update(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch batch)
        {

        }

        public void CenterOn(Vector2 position)
        {
            Position = position;
        }

        //public void Follow(GameObject whom)
        //{
        //    Position = Vector2.Lerp(Position, whom.position, 0.05F);
        //}

        public Vector2 WorldToScreen(Vector2 worldPosition)
        {
            return Vector2.Transform(worldPosition, TranslationMatrix);
        }

        public Vector2 ScreenToWorld(Vector2 screenPosition)
        {
            return Vector2.Transform(screenPosition, Matrix.Invert(TranslationMatrix));
        }

        public override void Enter()
        {
        }

        public override void Leave()
        {
        }
    }
}
