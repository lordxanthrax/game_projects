using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Clockwork.Classes.LogicalClasses;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Clockwork.Classes.PlayerOrientedLogicalClasses
{
    public class Battery : Item
    {
        int charge;

        public Battery()
            : base()
        {
        }

        public int GetCharge()
        {
            return charge;
        }

        public void Drain()
        {
            charge -= 1;
        }

        public void Update(GameTime gameTime)
        {
            base.Update();
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }
    }
}
