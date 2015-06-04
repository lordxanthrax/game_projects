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
using Microsoft.Xna.Framework.Input;

namespace MechWing_Defense_System.Classes.SpriteClasses
{
    public class UserControlledSprite: Sprite
    {

        // Get direction of sprite based on player input and speed
        public override Vector2 direction
        {
            get
            {
                Vector2 inputDirection = Vector2.Zero;

                // If player pressed arrow keys, move the sprite
                if (Keyboard.GetState(  ).IsKeyDown(Keys.A))
                    inputDirection.X -= 1;
                if (Keyboard.GetState(  ).IsKeyDown(Keys.D))
                    inputDirection.X += 1;
                if (Keyboard.GetState(  ).IsKeyDown(Keys.W))
                    inputDirection.Y -= 1;
                if (Keyboard.GetState(  ).IsKeyDown(Keys.S))
                    inputDirection.Y += 1;

                // If player pressed the gamepad thumbstick, move the sprite
                GamePadState gamepadState = GamePad.GetState(PlayerIndex.One);
                if(gamepadState.ThumbSticks.Left.X != 0)
                    inputDirection.X += gamepadState.ThumbSticks.Left.X;
                if(gamepadState.ThumbSticks.Left.Y != 0)
                    inputDirection.Y -= gamepadState.ThumbSticks.Left.Y;

                return inputDirection * speed;
            }
        }

        public UserControlledSprite(Texture2D textureImage, Vector2 position,
            Point frameSize, int collisionOffset, Point currentFrame, Point sheetSize,
            Vector2 speed, int newHealth, int newDamage, int newScore)
            : base(textureImage, position, frameSize, collisionOffset, currentFrame,
            sheetSize, speed, newHealth, newDamage, newScore)
        {
        }


        public UserControlledSprite(Texture2D textureImage, Vector2 position,
            Point frameSize, int collisionOffset, Point currentFrame, Point sheetSize,
            Vector2 speed, int millisecondsPerFrame, int newHealth, int newDamage, int newScore)
            : base(textureImage, position, frameSize, collisionOffset, currentFrame,
            sheetSize, speed, millisecondsPerFrame, newHealth, newDamage, newScore)
        {
        }

        public override void Update(GameTime gameTime, Rectangle clientBounds, Sprite player)
        {
            // Move the sprite based on direction
            position += direction;

            // If sprite is off the screen, move it back within the game window
            if (position.X < 0)
                position.X = 0;
            if (position.Y < 0)
                position.Y = 0;
            if (position.X > clientBounds.Width - frameSize.X)
                position.X = clientBounds.Width - frameSize.X;
            if (position.Y > clientBounds.Height - frameSize.Y)
                position.Y = clientBounds.Height - frameSize.Y;
          
            base.Update(gameTime, clientBounds, player);
        }

        //how bullet vectors are going to be calculated
        
    }
}
