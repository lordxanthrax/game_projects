/*
 * Just For Fun
 * Andersen, Corey; Gan, Jie; Olson, Darwin; Zhang, Shujian
 * CSCI 313   Section #1
 * 3/1/2014
*/

/*
 * This entire class is new.
 * 
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MechWing_Defense_System.Classes.SpriteClasses
{
    // Start of class BouncingSprite, which inherits from class AutomatedSprite.
    class BouncingSprite : AutomatedSprite
    {
        // Sprite is automated. Direction is same as speed.
        public override Vector2 direction
        {
            get { return speed; }
        }

        // The two constructors for BouncingSprite.
        public BouncingSprite(Texture2D textureImage, Vector2 position, Point frameSize,
            int collisionOffset, Point currentFrame, Point sheetSize, Vector2 speed, int newHealth, int newDamage, int newScore)
            : base(textureImage, position, frameSize, collisionOffset, currentFrame,
            sheetSize, speed, newHealth, newDamage, newScore)
        {
        }

        public BouncingSprite(Texture2D textureImage, Vector2 position, Point frameSize,
            int collisionOffset, Point currentFrame, Point sheetSize, Vector2 speed, int newHealth, int newDamage, int newScore,
            int millisecondsPerFrame)
            : base(textureImage, position, frameSize, collisionOffset, currentFrame,
            sheetSize, speed, newHealth, newDamage, newScore, millisecondsPerFrame)
        {
        }

        // Overridden Update method that will move sprite based on direction and reverse 
        // its course, if it hits a side.
        public override void Update(GameTime gameTime, Rectangle clientBounds)
        {
            // Move sprite based on direction.
            position += direction;

            //Reverse the sprite's direction if it hits a side.
            if (position.X > clientBounds.Width - frameSize.X ||
                position.X < 0)
                speed.X *= -1;
            if (position.Y > clientBounds.Height - frameSize.Y ||
                position.Y < 0)
                speed.Y *= -1;

            base.Update(gameTime, clientBounds);
        }

        public override Boolean IsTimeToShoot()
        {
            return false;
        }
    }
}
