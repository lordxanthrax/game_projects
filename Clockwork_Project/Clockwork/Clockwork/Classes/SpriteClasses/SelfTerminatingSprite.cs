using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Clockwork.Classes.SpriteClasses
{
    class SelfTerminatingSprite : Sprite
    {
        public bool animationFinished;

        public override Vector2 direction
        {
            get { return speed; }
        }

        public SelfTerminatingSprite(Texture2D textureImage, Vector2 position, Point frameSize,
            int collisionOffset, Point currentFrame, Point sheetSize, Vector2 speed)
            : base(textureImage, position, frameSize, collisionOffset, currentFrame,
            sheetSize, speed)
        {
            animationFinished = false;
        }

        public SelfTerminatingSprite(Texture2D textureImage, Vector2 position, Point frameSize,
            int collisionOffset, Point currentFrame, Point sheetSize, Vector2 speed,
            int millisecondsPerFrame)
            : base(textureImage, position, frameSize, collisionOffset, currentFrame,
            sheetSize, speed, millisecondsPerFrame)
        {
            animationFinished = false;
        }

        public override void Update(GameTime gameTime, Rectangle clientBounds)
        {

            // Update frame if time to do so based on framerate
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (timeSinceLastFrame > (millisecondsPerFrame))
            {
                // Increment to next frame
                timeSinceLastFrame = 0;
                ++currentFrame.X;
                if (currentFrame.X >= sheetSize.X)
                {
                    animationFinished = true;
                }
            }

            base.Update(gameTime, clientBounds);
        }
    }
}
