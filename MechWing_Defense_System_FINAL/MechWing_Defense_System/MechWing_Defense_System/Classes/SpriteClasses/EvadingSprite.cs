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
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MechWing_Defense_System.Classes.SpriteClasses
{
    class EvadingSprite : Sprite
    {
        // Save reference to the sprite manager to
        // use to get the player position
        SpriteManager spriteManager;

        int nextShootTime;
        Random rng;

        public EvadingSprite(Texture2D textureImage, Vector2 position,
            Point frameSize, int collisionOffset, Point currentFrame,
            Point sheetSize, Vector2 speed, int newHealth, int newDamage, int newScore, /*string collisionCueName,*/
            SpriteManager spriteManager)
            : base(textureImage, position, frameSize, collisionOffset, currentFrame, sheetSize, speed, newHealth, newDamage, newScore)//, collisionCueName)
        {
            this.spriteManager = spriteManager;
            rng = new Random();
        }

        public EvadingSprite(Texture2D textureImage, Vector2 position,
            Point frameSize, int collisionOffset, Point currentFrame,
            Point sheetSize, Vector2 speed, int millisecondsPerFrame, int newHealth, int newDamage, int newScore,
            /*string collisionCueName,*/ SpriteManager spriteManager)
            : base(textureImage, position, frameSize, collisionOffset, currentFrame, sheetSize, speed, millisecondsPerFrame, newHealth, newDamage, newScore)//, collisionCueName)
        {
            this.spriteManager = spriteManager;
            rng = new Random();
        }

        public override Vector2 direction
        {
            get { return speed; }
        }

        public override void Update(GameTime gameTime, Rectangle clientBounds, Sprite playerSprite)
        {
            // First, move the sprite along its direction vector
            position += speed;

            // Use the player position to move the sprite closer in
            // the X and/or Y directions
            Vector2 player = playerSprite.position;

            // Move away from the player horizontally
                if (player.X < position.X)
                    position.X += Math.Abs(speed.Y);
                else if (player.X > position.X)
                    position.X -= Math.Abs(speed.Y);

            // Move away from the player vertically
                if (player.Y < position.Y)
                    position.Y += Math.Abs(speed.X);
                else if (player.Y > position.Y)
                    position.Y -= Math.Abs(speed.X);

             nextShootTime -= gameTime.ElapsedGameTime.Milliseconds;

             
            
            base.Update(gameTime, clientBounds, playerSprite);
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
