using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Clockwork.Classes.MapClasses
{
    class MapBlockSprite
    {
        Rectangle mapBlockSpriteBox;
        Point mapBlockSpriteBoxLocation;
        Texture2D mapBlockSpriteTexture;

        public MapBlockSprite()
        {
        }

        public MapBlockSprite(Rectangle newMapBlockSpriteBox, Point newMapBlockSpriteBoxLocation, Texture2D newMapBlockSpriteTexture)
        {
            mapBlockSpriteBox = newMapBlockSpriteBox;
            mapBlockSpriteBoxLocation = newMapBlockSpriteBoxLocation;
            mapBlockSpriteTexture = newMapBlockSpriteTexture;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, bool lightUp)
        {
            float textureAlpha;

            if (lightUp)
            {
                textureAlpha = 1.0f;
            }
            else
            {
                textureAlpha = 0.1f;
            }
            //draw the sprite using that alpha.  SHOULD make things light up
        }

    }
}
