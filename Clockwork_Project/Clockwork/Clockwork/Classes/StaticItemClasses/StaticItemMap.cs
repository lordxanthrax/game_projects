using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Clockwork.Classes.StaticItemClasses
{
    public class StaticItemMap
    {
        public List<StaticItem> mapItems;
        public Texture2D spriteSheet;

        public StaticItemMap(Texture2D newSpriteSheet)
        {
            mapItems = new List<StaticItem>();
            spriteSheet = newSpriteSheet;
        }

        public void AddStaticItem(StaticItem newStaticItem)
        {
            mapItems.Add(newStaticItem);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, double opacity)
        {
            foreach (StaticItem SI in mapItems)
            {
                SI.Draw(gameTime, spriteBatch, spriteSheet, opacity);
            }
        }
    }
}
