using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MechWing_Defense_System.Classes
{
    public class Player
    {
        public int playerHealth;
        public int playerShield;
        public int playerDamage;
        public int playerSpeed;

        public HealthBar healthBar;

        public double timeSinceShot;

        public Player()
        {
        }

        public Player(int health, int shield, int damage, int speed, Texture2D healthBarTexture)
        {
            playerHealth = health;
            playerShield = shield;
            playerDamage = damage;
            playerSpeed = speed;
            timeSinceShot = 3000;
            healthBar = new HealthBar(healthBarTexture);
        }

        public void DealDamage(int damage)
        {
            if (playerShield > 0)
            {
                int difference = damage - playerShield;

                if (difference <= 0)
                {
                    playerShield -= damage;
                }
                else
                {
                    playerShield = 0;
                    playerHealth -= difference;
                }
            }
            else
            {
                playerHealth -= damage;
            }

            timeSinceShot = 0;
        }

        public void Heal(int healAmount)
        {
            playerHealth += healAmount;

            if (playerHealth > 100)
            {
                playerHealth = 100;
            }
        }

        public void Update(GameTime gameTime)
        {
            if (playerShield < 0)
            {
                playerShield = 0;
            }

            if (playerShield < 100 && timeSinceShot > 1500)
            {
                playerShield += 5;
            }

            timeSinceShot += gameTime.ElapsedGameTime.Milliseconds;

            healthBar.Update(gameTime, playerHealth, playerShield);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            healthBar.Draw(gameTime, spriteBatch);
        }
    }
}
