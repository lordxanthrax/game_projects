using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MechWing_Defense_System.Classes
{
    public class HealthBar
    {
        Texture2D healthBar;
        int currentHealth;
        int currentShield;

        public HealthBar(Texture2D healthBarTexture)
        {
            healthBar = healthBarTexture;
            currentHealth = 100;
            currentShield = 100;
        }

        public HealthBar(Texture2D healthBarTexture, int startingHealth)
        {
            healthBar = healthBarTexture;
            currentHealth = startingHealth;
            currentShield = 0;
        }


        public void Update(GameTime gameTime, int playerHealth, int playerShield)
        {
            currentHealth = playerHealth;
            currentShield = playerShield;
        }

        public void Update(GameTime gameTime, int unitHealth)
        {
            currentHealth = unitHealth;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            
            //Draw negative space
            spriteBatch.Draw(healthBar, new Rectangle(5, 5, healthBar.Width, 44),
                new Rectangle(0, 45, healthBar.Width, 44), Color.Black);

            spriteBatch.Draw(healthBar, new Rectangle(5, 50, healthBar.Width, 44),
                new Rectangle(0, 45, healthBar.Width, 44), Color.Black);

            //Draw the current health level based on the current Health
            spriteBatch.Draw(healthBar, new Rectangle(5,
            5, (int)(healthBar.Width * ((double)currentHealth / 100)), 44),
            new Rectangle(0, 45, healthBar.Width, 44), Color.Red);

            //Draw the shield level
            spriteBatch.Draw(healthBar, new Rectangle(5,
            50, (int)(healthBar.Width * ((double)currentShield / 100)), 44),
            new Rectangle(0, 45, healthBar.Width, 44), Color.CornflowerBlue);

            //Draw the box around the health bar
            spriteBatch.Draw(healthBar, new Rectangle(5, 5,
            healthBar.Width, 44), new Rectangle(0, 0, healthBar.Width, 44), Color.White);
            
            //Draw the box around the shield bar
            spriteBatch.Draw(healthBar, new Rectangle(5, 50,
            healthBar.Width, 44), new Rectangle(0, 0, healthBar.Width, 44), Color.White);

        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, int boss)
        {

            //Draw negative space
            spriteBatch.Draw(healthBar, new Rectangle(500, 5, healthBar.Width, 44),
                new Rectangle(0, 45, healthBar.Width, 44), Color.Black);

           

            //Draw the current health level based on the current Health
            spriteBatch.Draw(healthBar, new Rectangle(500,
            5, (int)(healthBar.Width * ((double)currentHealth / 4000)), 44),
            new Rectangle(0, 45, healthBar.Width, 44), Color.Yellow);

            

            //Draw the box around the health bar
            spriteBatch.Draw(healthBar, new Rectangle(500, 5,
            healthBar.Width, 44), new Rectangle(0, 0, healthBar.Width, 44), Color.White);
            
        }
    }
}
