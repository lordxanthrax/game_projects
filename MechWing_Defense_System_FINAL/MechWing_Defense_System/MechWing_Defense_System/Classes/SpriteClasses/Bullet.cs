/*
 * Just For Fun
 * Andersen, Corey; Gan, Jie; Olson, Darwin; Zhang, Shujian
 * CSCI 313   Section #1
 * 5/9/2014
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MechWing_Defense_System.Classes.SpriteClasses
{
    public class Bullet : AutomatedSprite
    {
        

        public Bullet(Texture2D textureImage, Vector2 position, Point frameSize,
            int collisionOffset, Point currentFrame, Point sheetSize, Vector2 speed, int newHealth, int newDamage, int newScore)
            : base(textureImage, position, frameSize, collisionOffset, currentFrame,
            sheetSize, speed, newHealth, newDamage, newScore)
        {
            
        }

        public Bullet(Texture2D textureImage, Vector2 position, Point frameSize,
            int collisionOffset, Point currentFrame, Point sheetSize, Vector2 speed,
            int millisecondsPerFrame, int newHealth, int newDamage, int newScore)
            : base(textureImage, position, frameSize, collisionOffset, currentFrame,
            sheetSize, speed, newHealth, newDamage, newScore, millisecondsPerFrame)
        {
        }

        public override void Update(GameTime gameTime, Rectangle clientBounds, Sprite player)
        {
            // Move sprite based on direction
            position += speed * 10;

            base.Update(gameTime, clientBounds);
        }

        public override bool IsTimeToShoot()
        {
            return false;
        }
    }
}
