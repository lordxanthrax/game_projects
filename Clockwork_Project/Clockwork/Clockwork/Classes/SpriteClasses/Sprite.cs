﻿/*
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

namespace Clockwork.Classes.SpriteClasses
{
    public abstract class Sprite
    {
        // Stuff needed to draw the sprite
        public Texture2D textureImage;
        public Point frameSize;
        protected Point currentFrame;
        protected Point sheetSize;
        
        // Collision data
        int collisionOffset;
        
        // Framerate stuff
        protected int timeSinceLastFrame = 0;
        public int millisecondsPerFrame;
        const int defaultMillisecondsPerFrame = 100;

        // Movement data
        public Vector2 speed;
        public Vector2 position;

        //direction the sprite is facing
        public int facing;

        // Abstract definition of direction property
        public abstract Vector2 direction
        {
            get;
        }

        public Vector2 GetPosition
        {
            get { return position; }
        }

        public Sprite(Texture2D textureImage, Vector2 position, Point frameSize,
            int collisionOffset, Point currentFrame, Point sheetSize, Vector2 speed)
            : this(textureImage, position, frameSize, collisionOffset, currentFrame,
            sheetSize, speed, defaultMillisecondsPerFrame)
        {
        }

        public Sprite(Texture2D textureImage, Vector2 position, Point frameSize,
            int collisionOffset, Point currentFrame, Point sheetSize, Vector2 speed,
            int millisecondsPerFrame)
        {
            this.textureImage = textureImage;
            this.position = position;
            this.frameSize = frameSize;
            this.collisionOffset = collisionOffset;
            this.currentFrame = currentFrame;
            this.sheetSize = sheetSize;
            this.speed = speed;
            this.millisecondsPerFrame = millisecondsPerFrame;
        }

        public virtual void Update(GameTime gameTime, Rectangle clientBounds)
        {
            //select the right row of the sprite sheet based on which way the character is facing
            
            // Update frame if time to do so based on framerate
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;

            if (timeSinceLastFrame > (millisecondsPerFrame))
            {
                // Increment to next frame
                timeSinceLastFrame = 0;
                ++currentFrame.X;
                if (currentFrame.X >= sheetSize.X)
                {
                    currentFrame.X = 0;
                }
            }
        }

        public void Update(GameTime gameTime, Rectangle clientBounds, bool isMoving)
        {
            //select the right row of the sprite sheet based on which way the character is facing

            // Update frame if time to do so based on framerate

            if (isMoving)
            {
                timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
                if (timeSinceLastFrame > (millisecondsPerFrame))
                {
                    // Increment to next frame
                    timeSinceLastFrame = 0;
                    ++currentFrame.X;
                    if (currentFrame.X >= sheetSize.X)
                    {
                        currentFrame.X = 0;
                    }
                }
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // Draw the sprite
            spriteBatch.Draw(textureImage,
                position,
                new Rectangle(currentFrame.X * frameSize.X,
                    currentFrame.Y * frameSize.Y,
                    frameSize.X, frameSize.Y),
                Color.White, 0, Vector2.Zero,
                1f, SpriteEffects.None, 0);
        }

        // Gets the collision rect based on position, framesize and collision offset
        public Rectangle collisionRect
        {
            get
            {
                return new Rectangle(
                    (int)position.X + collisionOffset,
                    (int)position.Y + collisionOffset,
                    frameSize.X - (collisionOffset * 2),
                    frameSize.Y - (collisionOffset * 2));
            }
        }

        public bool IsOutOfBounds(Rectangle clientRect)
        {
            if (position.X < -frameSize.X ||
                position.X > clientRect.Width ||
                position.Y < -frameSize.Y ||
                position.Y > clientRect.Height)
            {
                return true;
            }

            return false;

        }       
    }
}
