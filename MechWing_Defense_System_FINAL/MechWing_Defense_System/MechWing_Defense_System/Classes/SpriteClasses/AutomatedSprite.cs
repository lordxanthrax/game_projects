/*
 * Just For Fun
 * Andersen, Corey; Gan, Jie; Olson, Darwin; Zhang, Shujian
 * CSCI 313   Section #1
 * 3/1/2014
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MechWing_Defense_System.Classes.SpriteClasses
{
    public class AutomatedSprite: Sprite
    {
        int nextShootTime;
        Random rng;

        // Sprite is automated. Direction is same as speed
        public override Vector2 direction
        {
            get { return speed; }
        }

        public AutomatedSprite(Texture2D textureImage, Vector2 position, Point frameSize,
            int collisionOffset, Point currentFrame, Point sheetSize, Vector2 speed, int newHealth, int newDamage, int newScore)
            : base(textureImage, position, frameSize, collisionOffset, currentFrame,
            sheetSize, speed, newHealth, newDamage, newScore)
        {
            rng = new Random();
        }

        public AutomatedSprite(Texture2D textureImage, Vector2 position, Point frameSize,
            int collisionOffset, Point currentFrame, Point sheetSize, Vector2 speed,
            int millisecondsPerFrame, int newHealth, int newDamage, int newScore)
            : base(textureImage, position, frameSize, collisionOffset, currentFrame,
            sheetSize, speed, millisecondsPerFrame, newHealth, newDamage, newScore)
        {
            rng = new Random();
        }

        public override void Update(GameTime gameTime, Rectangle clientBounds, Sprite player)
        {
            // Move sprite based on direction
            position += direction;

            nextShootTime -= gameTime.ElapsedGameTime.Milliseconds;

            base.Update(gameTime, clientBounds, player);
        }

        private void ResetShootTime()
        {
            nextShootTime = rng.Next(250, 3000);
        }

        public override bool IsTimeToShoot()
        {

            if (nextShootTime < 0)
            {
                this.ResetShootTime();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
