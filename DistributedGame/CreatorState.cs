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

        public void ReloadImage()
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
        }

        public override void LoadContent()
        {
            ReloadImage();

            texturePosition = new Vector2(100, (Global.g.PresentationParameters.BackBufferHeight - texture.Height) / 3);

            font = new Font(Global.c.Load<Texture2D>("fonts/font1.png"), "abcdefghijklmnopqrstuvwxyz1234567890:!' ", 4, 8);
            Global.font = font;
        }

        public override void Enter()
        {
               
        }

        public override void Leave()
        {
            
        }

        public void SubmitTexture()
        {
            // submit the player's texture
            Global.playerTexture = texture;
            SwitchState("play");
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Z))
            {
                SubmitTexture();
            }
            else if (GamePad.GetState(PlayerIndex.One).IsConnected && GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.A))
            {
                SubmitTexture();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.X))
            {
                ReloadImage();
            }
        }

        public override void Draw(SpriteBatch batch)
        {
            
            batch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone);
            batch.Draw(texture, texturePosition, texture.Bounds, Color.White, 0, Vector2.Zero, 16, SpriteEffects.None, 0);
            FontRenderer.RenderFont(batch, font, "press the #a button #nto continue", new Vector2(500, 270));
            FontRenderer.RenderFont(batch, font, "press the #b button #nto reload the image", new Vector2(500, 400));
            batch.End();
        }
    }
}
