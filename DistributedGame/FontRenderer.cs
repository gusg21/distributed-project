using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedGame
{
    class FontRenderer
    {
        public static readonly Texture2D A_BUTTON = Global.c.Load<Texture2D>("buttons/a.png");

        public static void RenderFont(SpriteBatch batch, Font font, string text, Vector2 position)
        {
            RenderFont(batch, font, text, position, 4, Color.White);
        }

        public static void RenderFont(SpriteBatch batch, Font font, string text, Vector2 position, int scale)
        {
            RenderFont(batch, font, text, position, scale, Color.White);
        }

        public static void RenderFont(SpriteBatch batch, Font font, string text, Vector2 position, int scale, Color tint)
        {
            int xOffset = 0, yOffset = 0, index = 0;
            char nextLetter = ' ';
            bool skip = false;
            foreach (char letter in text)
            {
                if (skip)
                {
                    index += 2;
                    skip = false;
                    continue;
                }

                try
                {
                    nextLetter = text[index + 1];
                }
                catch (IndexOutOfRangeException)
                {
                    nextLetter = ' ';
                }

                Vector2 localPosition = new Vector2(position.X + xOffset, position.Y + yOffset);

                if (letter == '#') // special
                {
                    if (nextLetter == 'n') // new line
                    {
                        yOffset += font.glyphHeight * scale + (int) (scale * 0.2);
                        xOffset = 0;
                        skip = true;
                        continue;
                    }

                    if (nextLetter == 'a') // a button
                    {
                        localPosition.X += scale;
                        batch.Draw(A_BUTTON, localPosition, A_BUTTON.Bounds, tint, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
                        xOffset += (int) (scale * A_BUTTON.Width * 1.25);
                        skip = true;
                        continue;
                    }
                }

                if (!letter.Equals(' '))
                {
                    Rectangle glyphRect = font.GetGlyph(letter);

                    batch.Draw(font.Texture, localPosition, glyphRect, tint, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
                }
                
                xOffset += font.glyphWidth * scale;
                index++;
            }
        }
    }

    class Font
    {
        public Texture2D Texture { get; private set; }
        string glyphs;
        public int glyphWidth { get; private set; }
        public int glyphHeight { get; private set; }

        public Font(Texture2D texture, string glyphs, int glyphWidth, int glyphHeight)
        {
            this.Texture = texture ?? throw new ArgumentNullException(nameof(texture));
            this.glyphs = glyphs;
            this.glyphWidth = glyphWidth;
            this.glyphHeight = glyphHeight;
        }

        public Rectangle GetGlyph(char letter)
        {
            int glyphIndex = glyphs.IndexOf(letter);
            return new Rectangle(
                glyphWidth * glyphIndex, 0, glyphWidth, glyphHeight
            );
        }
    }
}
