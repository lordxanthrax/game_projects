using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace Clockwork.Classes.LogicalClasses
{
    public class Weapon : Item
    {
        public Texture2D weaponSpriteSheet;
        public int weaponDamage;
        public int cooldownTime;

        public int widthOffset = 0;
        public int heightOffset = 0;

        Point currentFrame;
        Point frameSize;
        Point sheetSize;

        int timeSinceLastFrame = 0;
        int millisecondsPerFrame = 32;

        protected bool animationPlaying;

        SoundEffect weaponSoundEffect;


        public Weapon(): base()
        {
            weaponSpriteSheet = null;
            weaponDamage = 0;

            currentFrame = new Point(1, 1);
            sheetSize = new Point(1, 1);

            millisecondsPerFrame = 0;
        }

        public Weapon(String newItemName, int newSlotsTaken, Texture2D newInventoryImage, Texture2D newWeaponSpriteSheet, int newWeaponDamage,
            int newCooldownTime, Point newCurrentFrame, Point newFrameSize, Point newSheetSize)
            : base(newItemName, newSlotsTaken, newInventoryImage)
        {
            weaponSpriteSheet = newWeaponSpriteSheet;
            weaponDamage = newWeaponDamage;
            cooldownTime = newCooldownTime;

            widthOffset = newFrameSize.X /2;
            heightOffset = newFrameSize.Y / 2;

            currentFrame = newCurrentFrame;
            frameSize = newFrameSize;
            sheetSize = newSheetSize;
        }

        public void Update(GameTime gameTime, Rectangle clientBounds, int facing)
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

            if (animationPlaying)
            {
                //select the right row of the sprite sheet based on which way the character is facing
                //only update it if the animationTimer is under a certain amount
                timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
                if (timeSinceLastFrame > (millisecondsPerFrame))
                {
                    // Increment to next frame
                    timeSinceLastFrame = 0;
                    ++currentFrame.X;
                    if (currentFrame.X >= sheetSize.X)
                    {
                        currentFrame.X = 0;
                        animationPlaying = false;
                    }
                }
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 position)
        {
            // Draw the sprite
            spriteBatch.Draw(weaponSpriteSheet, position,
                new Rectangle(currentFrame.X * frameSize.X,
                    currentFrame.Y * frameSize.Y,
                    frameSize.X, frameSize.Y),
                Color.White, 0, Vector2.Zero,
                1f, SpriteEffects.None, 0);
        }

        //trigger the animationTimer so the gun animates a shot
        public virtual bool Shoot()
        {
            animationPlaying = true;
            //weaponSoundEffect.Play();
            //play a BANG BANG BOOM! sound.  lol.
            return true;
        }
    }
}
