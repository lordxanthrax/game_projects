using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Clockwork.Classes.StaticItemClasses
{
    public class StaticItem
    {
        public Vector2 position;
        public Rectangle itemFrame;
        public String interactionResponse;
        public bool impassible;

        public StaticItem(Vector2 newPosition, Rectangle newItemFrame, String newResponse, bool newImpassible)
        {
            position = newPosition;
            itemFrame = newItemFrame;
            interactionResponse = newResponse;
            impassible = newImpassible;
        }

        public Rectangle collisionRect
        {
            get
            {
                return new Rectangle(
                    (int)position.X,
                    (int)position.Y,
                    itemFrame.Width,
                    itemFrame.Height);
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Texture2D spriteSheet, double opacity)
        {
            spriteBatch.Draw(spriteSheet, position, itemFrame, Color.White * (float)opacity, 0.0f, Vector2.Zero, 1.3f, SpriteEffects.None, 0.9f);
            
        }
    }
}
