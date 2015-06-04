using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Clockwork.Classes.MapClasses
{
    class MapBlock
    {
        MapBlockSprite blockSprite;
        bool impassible;
        bool lightUp;

        public MapBlock()
        {
        }

        public MapBlock(MapBlockSprite newBlockSprite, bool newImpassible)
        {
            blockSprite = newBlockSprite;
            impassible = newImpassible;
            lightUp = false;
        }

        public void Update(GameTime gameTime)
        {
            lightUp = false;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            blockSprite.Draw(gameTime, spriteBatch, lightUp);
        }

        public void LightUp()
        {
            lightUp = true;
        }
    }
}
