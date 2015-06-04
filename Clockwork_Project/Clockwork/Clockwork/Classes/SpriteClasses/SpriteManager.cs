/*
 * Just For Fun
 * Andersen, Corey; Gan, Jie; Olson, Darwin; Zhang, Shujian
 * CSCI 313   Section #1
 * 5/9/2014
*/

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Clockwork.Classes.LogicalClasses;
using Clockwork.Classes.SpriteClasses;


namespace Clockwork.Classes.SpriteClasses
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class SpriteManager : Microsoft.Xna.Framework.DrawableGameComponent
    {
        //SpriteBatch for drawing
        SpriteBatch spriteBatch;

        int type;

        public List<Sprite> spriteList = new List<Sprite>();

        public SpriteManager(Game game, int spriteType)
            : base(game)
        {
            // TODO: Construct any child components here
            type = spriteType;
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // call to ResetSpawnTime method to reset spawn timer; Added on 4/15/14
            ResetSpawnTime();
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            base.LoadContent();
        }

        public void LoadContent(int gamestate)
        {
            

                base.LoadContent();
            
        }

        // Method to set the next spawn time; Added on 4/15/14
        private void ResetSpawnTime()
        {
           
        }
          
        public void AddSpriteToList(Sprite newSprite)
        {
            spriteList.Add(newSprite);
        }

        //REAL UPDATE CODE, PROBABLY
        public void Update(GameTime gameTime, int gameState, Sprite playerSprite)
        {
            base.Update(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public void Draw(GameTime gameTime, int gameState)
        {
           
            base.Draw(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        private Vector2 Shoot(Vector2 player, Vector2 enemyVec)
        {
            Vector2 newBulletVector;
            newBulletVector = enemyVec - player;

            if (newBulletVector != Vector2.Zero)
            {
                newBulletVector.Normalize();
            }

            return newBulletVector;
        }

        public void CheckPlayerCollisions(UserControlledSprite player, ref Player playerLogic)
        {
            for (int i = 0; i < spriteList.Count; ++i)
            {
                Sprite s = spriteList[i];

                if (s.collisionRect.Intersects(player.collisionRect))
                {
                    
                    spriteList.RemoveAt(i);
                }

                
            }
        }
    }
}
