using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Clockwork.Classes.StaticItemClasses
{
    public class StaticItemMapList
    {
        public List<StaticItemMap> staticItemMapList;

        public StaticItemMapList()
        {
            staticItemMapList = new List<StaticItemMap>();
        }

        public void AddToList(StaticItemMap newStaticItemMap)
        {
            staticItemMapList.Add(newStaticItemMap);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, double opacity)
        {
            foreach (StaticItemMap SIM in staticItemMapList)
            {
                SIM.Draw(gameTime, spriteBatch, opacity);
            }
        }
    }
}
