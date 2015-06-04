using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Clockwork.Classes.SpriteClasses;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Clockwork.Classes.LogicalClasses
{
    public class Bullet
    {
        public BulletSprite bulletSprite;
        public int damage;
        

        public Bullet(int gunDamage, BulletSprite newBulletSprite)
        {
            bulletSprite = newBulletSprite;
            damage = gunDamage;
        }

        public void Update(GameTime gameTime, Rectangle clientBounds)
        {
            bulletSprite.Update(gameTime, clientBounds);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            bulletSprite.Draw(gameTime, spriteBatch);
        }
    }
}
