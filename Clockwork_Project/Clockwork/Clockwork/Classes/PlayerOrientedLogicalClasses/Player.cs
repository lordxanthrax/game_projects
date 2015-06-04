using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Clockwork.Classes.LogicalClasses;
using Clockwork.Classes.SpriteClasses;
using Microsoft.Xna.Framework.Input;


namespace Clockwork.Classes.LogicalClasses
{
    public class Player
    {
        public int playerHealth;
        public int playerHealthCurrent;
        public int playerSpeed;
        public int playerStamina;
        public int playerStaminaCurrent;

        public int timeSinceHit;
        public int timeSinceFired;
        public int timeSinceSprinted;

        bool isShooting;
        bool isMoving;

        int facing;
        Vector2 inputDirection;

        GamePadState currentGPS;
        PlayerIndex playerIndex;
        GamePadDeadZone GPDZ;

        public UserControlledSprite playerSprite;

        public Vector2 oldPosition;

        public Inventory playerInventory;

        public List<Bullet> playerBullets;

        Texture2D defaultBulletTexture;

        public Player()
        {
        }

        public Player(int health, int stamina, int speed, UserControlledSprite sprite, Texture2D bulletTex)
        {
            playerHealth = health;
            playerHealthCurrent = health;

            playerStamina = stamina;
            playerStaminaCurrent = stamina;

            playerSpeed = speed;

            timeSinceHit = 1000;
            timeSinceFired = 10000;
            timeSinceSprinted = 1000;

            isShooting = false;
            isMoving = false;
            facing = 0;
            inputDirection = Vector2.Zero;

            playerSprite = sprite;
            playerInventory = new Inventory();

            playerBullets = new List<Bullet>();

            defaultBulletTexture = bulletTex;

            playerIndex = new PlayerIndex();
            currentGPS = new GamePadState();
            GPDZ = new GamePadDeadZone();            
        }

        public void DealDamage(int damage)
        {     
            if(timeSinceHit >= 1000)
            {
                playerHealthCurrent -= damage;
            }
        }

        public void Heal(int healAmount)
        {
            playerHealthCurrent += healAmount;

            if (playerHealthCurrent > playerHealth)
            {
                playerHealthCurrent = playerHealth;
            }
        }

        public void RecoverStamina(int recoverAmount)
        {
            playerStaminaCurrent += recoverAmount;

            if (playerStaminaCurrent > playerStamina)
            {
                playerStaminaCurrent = playerStamina;
            }
        }

        public void Update(GameTime gameTime, Rectangle clientBounds)
        {
            //lock this time so it doesn't escalate out of control on idle
            if (timeSinceHit < 1000)
            {
                timeSinceHit += gameTime.ElapsedGameTime.Milliseconds;
            }

            if (timeSinceFired < 10000)
            {
                timeSinceFired += gameTime.ElapsedGameTime.Milliseconds;
            }

            if (timeSinceSprinted < 10000)
            {
                timeSinceSprinted += gameTime.ElapsedGameTime.Milliseconds;
            }
            else
            {
                RecoverStamina(1);
            }

            foreach (Bullet b in playerBullets)
            {
                b.Update(gameTime, clientBounds);
            }

            GetPlayerInput();

            oldPosition = playerSprite.position;

            playerSprite.Update(gameTime, clientBounds, isMoving, isShooting, facing, inputDirection);
            playerInventory.Update(gameTime, clientBounds, facing);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {                 
           
            if (isShooting)
            {
                Vector2 gunPosition = playerSprite.position;
                int playerWidthMiddle = playerSprite.textureImage.Width / 8;
                int playerHeightMiddle = playerSprite.textureImage.Height / 8;

                gunPosition.X += playerWidthMiddle;
                gunPosition.Y += playerHeightMiddle;
                gunPosition.X -= playerInventory.currentWeapon.widthOffset;
                gunPosition.Y -= playerInventory.currentWeapon.heightOffset;

                switch (facing)
                {
                    case 0:
                        gunPosition.X -= 10;
                        gunPosition.Y += 15;
                        playerSprite.Draw(gameTime, spriteBatch);
                        playerInventory.currentWeapon.Draw(gameTime, spriteBatch, gunPosition);
                        DrawBullets(gameTime, spriteBatch);
                        break;

                    case 1:
                        gunPosition.X -= 25;
                        gunPosition.Y -= 5;
                        playerSprite.Draw(gameTime, spriteBatch);
                        playerInventory.currentWeapon.Draw(gameTime, spriteBatch, gunPosition);
                        DrawBullets(gameTime, spriteBatch);
                        break;

                    case 2:
                        gunPosition.X += 25;
                        gunPosition.Y -= 5;
                        playerSprite.Draw(gameTime, spriteBatch);
                        playerInventory.currentWeapon.Draw(gameTime, spriteBatch, gunPosition);
                        DrawBullets(gameTime, spriteBatch);
                        break;

                    case 3:
                        gunPosition.X += 10;
                        gunPosition.Y -= 40;
                        playerInventory.currentWeapon.Draw(gameTime, spriteBatch, gunPosition);
                        DrawBullets(gameTime, spriteBatch);
                        playerSprite.Draw(gameTime, spriteBatch);
                        break;
                }
            }
            else
            {
                playerSprite.Draw(gameTime, spriteBatch);
                DrawBullets(gameTime, spriteBatch);
            }
        }

        public void GetPlayerInput()
        {   
            //currentGPS = new GamePadState(GamePad.GetState());

            //if the shoot stance is on, don't bother getting the other keys
            if (!Keyboard.GetState().IsKeyDown(Keys.LeftShift))
            {
                isShooting = false;
                playerSprite.millisecondsPerFrame = 170;

                //check the 4 directions
                /*
                 * 0 - down
                 * 1- left
                 * 2- right
                 * 3 - up
                */
                if (Keyboard.GetState().IsKeyDown(Keys.W))
                {
                    inputDirection.Y = -1;
                    facing = 3;
                    isMoving = true;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.S))
                {
                    inputDirection.Y = 1;
                    facing = 0;
                    isMoving = true;
                }
                else
                {
                    isMoving = false;
                    inputDirection.Y = 0;
                }

                if (Keyboard.GetState().IsKeyDown(Keys.D))
                {
                    inputDirection.X = 1;
                    facing = 2;
                    isMoving = true;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.A))
                {
                    inputDirection.X = -1;
                    facing = 1;
                    isMoving = true;
                }
                else
                {
                    inputDirection.X = 0;
                }

                //normalize the vector
                if ((inputDirection.X > 0 || inputDirection.X < 0) && (inputDirection.Y > 0 || inputDirection.Y < 0))
                {
                    inputDirection.Normalize();                              
                }

                //check for sprinting, and SPRINT!
                if (Keyboard.GetState().IsKeyDown(Keys.N) && playerStaminaCurrent > 0)
                {
                    inputDirection = Vector2.Multiply(inputDirection, 1.8f);
                    playerStaminaCurrent -= 1;
                    timeSinceSprinted = 0;
                    playerSprite.millisecondsPerFrame = 70;
                }
            }

            //in this case, we're in the shooting stance, so freeze our character
            else
            {
                inputDirection.X = 0;
                inputDirection.Y = 0;
                isShooting = true;
                isMoving = false;

                //check for a shot
                if (Keyboard.GetState().IsKeyDown(Keys.J))
                {
                    //make sure we fire at our gun's fire rate, and that we have a gun
                    if (timeSinceFired >= playerInventory.currentWeapon.cooldownTime && playerInventory.currentWeapon != null)
                    {
                        if (playerInventory.currentWeapon.Shoot())
                        {
                            //reset the timer
                            timeSinceFired = 0;

                            if (playerInventory.currentWeapon.GetType() == typeof(AmmoBasedWeapon))
                            {
                                //Texture2D tempBulletTexture = playerInventory.currentWeapon.weaponBulletTexture;
                                int tempBulletSpeed = 10;

                                float playerX = playerSprite.position.X + (playerSprite.textureImage.Width / 8);
                                float playerY = playerSprite.position.Y + (playerSprite.textureImage.Height / 8);
                                Vector2 tempBulletPosition = Vector2.Zero;
                                Vector2 tempBulletDirection = Vector2.Zero;
                                Point tempCurrentFrame = new Point(1, 1);

                                /* 0 - down
                                 * 1 - left
                                 * 2 - right
                                 * 3 - up
                                */
                                switch (facing)
                                {
                                    case 0:
                                        {
                                            tempBulletPosition = new Vector2(playerX - 13, (playerY - 20));
                                            tempBulletDirection = new Vector2(0, 1);
                                            tempCurrentFrame = new Point(1, 0);
                                            break;
                                        }

                                    case 1:
                                        {
                                            tempBulletPosition = new Vector2((playerX - 30), (playerY - 5));
                                            tempBulletDirection = new Vector2(-1, 0);
                                            tempCurrentFrame = new Point(1, 1);
                                            break;
                                        }

                                    case 2:
                                        {
                                            tempBulletPosition = new Vector2((playerX + 30), (playerY - 5));
                                            tempBulletDirection = new Vector2(1, 0);
                                            tempCurrentFrame = new Point(1, 2);
                                            break;
                                        }

                                    case 3:
                                        {
                                            tempBulletPosition = new Vector2(playerX + 10, (playerY - 30));
                                            tempBulletDirection = new Vector2(0, -1);
                                            tempCurrentFrame = new Point(1, 3);
                                            break;
                                        }
                                }

                                tempBulletDirection = tempBulletDirection * tempBulletSpeed;

                                //add a bullet to our player's bullets list
                                BulletSprite tempBulletSprite = new BulletSprite(defaultBulletTexture, tempBulletPosition, new Point(5, 5), 0, tempCurrentFrame,
                                    new Point(1, 4), tempBulletDirection);
                                Bullet tempBullet = new Bullet(playerInventory.currentWeapon.weaponDamage, tempBulletSprite);
                                playerBullets.Add(tempBullet);
                            }
                        }
                    }
                }

            }

            // If player pressed the gamepad thumbstick, move the sprite
            GamePadState gamepadState = GamePad.GetState(PlayerIndex.One);
            if (gamepadState.ThumbSticks.Left.X != 0)
                inputDirection.X += gamepadState.ThumbSticks.Left.X;
            if (gamepadState.ThumbSticks.Left.Y != 0)
                inputDirection.Y -= gamepadState.ThumbSticks.Left.Y;
        }

        //just a clutter saver
        public void DrawBullets(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (Bullet b in playerBullets)
            {
                b.Draw(gameTime, spriteBatch);
            }
        }
    }

    

}
