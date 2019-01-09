using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DistributedGame
{
    class CreatorState : State
    {
        Texture2D texture;
        Vector2 texturePosition;

        Font font;

        public CreatorState(Game game) : base(game)
        {
           
        }

        public void LoadDefaultImage()
        {
            texture = Global.c.Load<Texture2D>("defaultplayer.png");
        }

        public override void LoadContent()
        {
            try
            {
                texture = Global.c.Load<Texture2D>("player.png");
            }
            catch (ContentLoadException)
            {
                LoadDefaultImage();
            }

            if (texture.Width != 16 || texture.Height != 16)
            {
                LoadDefaultImage();
            }

            texturePosition = new Vector2(32, (Global.g.PresentationParameters.BackBufferHeight - texture.Height) / 4);

            font = new Font(Global.c.Load<Texture2D>("fonts/font1.png"), "abcdefghijklmnopqrstuvwxyz1234567890:! ", 4, 8);
        }

        public override void Enter()
        {
               
        }

        public override void Leave()
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Z))
            {
                SwitchState("play");
            }
        }

        public override void Draw(SpriteBatch batch)
        {
            
            batch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone);
            batch.Draw(texture, texturePosition, texture.Bounds, Color.White, 0, Vector2.Zero, 16, SpriteEffects.None, 0);
            FontRenderer.RenderFont(batch, font, "press the #a button #nto continue", new Vector2(400, 200));
            batch.End();
        }
    }
}
