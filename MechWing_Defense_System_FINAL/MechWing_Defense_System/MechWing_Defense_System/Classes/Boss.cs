using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MechWing_Defense_System.Classes;
using MechWing_Defense_System.Classes.SpriteClasses;

namespace MechWing_Defense_System.Classes
{
    class Boss
    {
        public int bossHealth;
        int timeSinceShotBullet;
        int timeSinceShotLaser;
        public BouncingSprite bossSprite;
        HealthBar bossHealthBar;

        public SpriteManager bossBullets;
        Texture2D bulletTex;

        public Boss()
        {
        }

        public Boss(int startHealth, Texture2D bossTexture, Texture2D bossHealthBarTexture, Game game, Texture2D bulletTexture)
        {
            bossHealth = startHealth;
            bossSprite = new BouncingSprite(bossTexture, new Vector2(800, 200), new Point(bossTexture.Width, bossTexture.Height), 10, new Point(0, 0),
                new Point(1, 1), new Vector2(0, 10), 4000, 50, 2000);

            bossBullets = new SpriteManager(game, 2);
            bossBullets.LoadContent(2);
            bulletTex = bulletTexture;

            timeSinceShotBullet = 0;
            timeSinceShotLaser = 0;

            bossHealthBar = new HealthBar(bossHealthBarTexture, 4000);
        }

        public void DealDamage(int damage)
        {
            bossHealth = bossHealth - damage;
        }

        public void Update(GameTime gameTime, UserControlledSprite player, Game game, int gameState)
        {
            if (bossSprite.position.Y < 0)
            {
                bossSprite.position.Y += 10;
            }

            if (bossSprite.position.Y > 800)
            {
                bossSprite.position.Y += -10;
            }

            timeSinceShotBullet += gameTime.ElapsedGameTime.Milliseconds;

            if (timeSinceShotBullet > 1000)
            {
                ShootAtPlayer(player);

                timeSinceShotBullet = 0;
            }

            bossSprite.Update(gameTime, game.Window.ClientBounds);

            bossHealthBar.Update(gameTime, bossHealth);

            bossBullets.Update(gameTime, gameState, player);
                
        }

        public void ShootAtPlayer(UserControlledSprite player)
        {
            Vector2 newBulletVector;
            Vector2 shotVector = new Vector2(bossSprite.position.X + 30, bossSprite.position.Y + 75);
            newBulletVector = player.position - shotVector;

            if (newBulletVector != Vector2.Zero)
            {
                newBulletVector.Normalize();
            }

            bossBullets.AddSpriteToList(new AutomatedSprite(bulletTex, shotVector, new Point(bulletTex.Width, bulletTex.Height), 3, new Point(1,1),
                new Point(1,1), newBulletVector, 20, 30, 0));

            shotVector = new Vector2(bossSprite.position.X + 30, bossSprite.position.Y + 205);
            newBulletVector = player.position - shotVector;

            if (newBulletVector != Vector2.Zero)
            {
                newBulletVector.Normalize();
            }

            bossBullets.AddSpriteToList(new Bullet(bulletTex, shotVector, new Point(bulletTex.Width, bulletTex.Height), 3, new Point(1, 1),
                new Point(1, 1), newBulletVector, 20, 30, 0));
        }




        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Sprite player)
        {
            bossHealthBar.Draw(gameTime, spriteBatch, 1);
            bossSprite.Draw(gameTime, spriteBatch);
            spriteBatch.End();
            bossBullets.Draw(gameTime, 8);
            


            
        }
        
        
    }
}
