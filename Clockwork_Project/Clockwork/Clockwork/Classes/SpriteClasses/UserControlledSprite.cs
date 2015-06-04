
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Clockwork.Classes.SpriteClasses
{
    public class UserControlledSprite: Sprite
    {
        // Get direction of sprite based on player input and speed
        public override Vector2 direction
        {
            get
            {
                return Vector2.Zero;           
            }
        }

        public UserControlledSprite(Texture2D textureImage, Vector2 position,
            Point frameSize, int collisionOffset, Point currentFrame, Point sheetSize,
            Vector2 speed)
            : base(textureImage, position, frameSize, collisionOffset, currentFrame,
            sheetSize, speed)
        {

        }


        public UserControlledSprite(Texture2D textureImage, Vector2 position,
            Point frameSize, int collisionOffset, Point currentFrame, Point sheetSize,
            Vector2 speed, int millisecondsPerFrame)
            : base(textureImage, position, frameSize, collisionOffset, currentFrame,
            sheetSize, speed, millisecondsPerFrame)
        {
          
        }

        public void Update(GameTime gameTime, Rectangle clientBounds, bool isMoving, bool isShooting, int facing, Vector2 inputDirection)
        {
            switch (facing)
            {
                case 0:
                    currentFrame.Y = 0;
                    break;

                case 1:
                    currentFrame.Y = 1;
                    break;

                case 2:
                    currentFrame.Y = 2;
                    break;

                case 3:
                    currentFrame.Y = 3;
                    break;
            }
            // Move the sprite based on direction
            position = position + (inputDirection * speed);

            // If sprite is off the screen, move it back within the game window
            if (position.X < 0)
                position.X = 0;
            if (position.Y < 0)
                position.Y = 0;
            if (position.X > clientBounds.Width - frameSize.X)
                position.X = clientBounds.Width - frameSize.X;
            if (position.Y > clientBounds.Height - frameSize.Y)
                position.Y = clientBounds.Height - frameSize.Y;

            base.Update(gameTime, clientBounds, isMoving);
        }

        //how bullet vectors are going to be calculated
        
    }
}
