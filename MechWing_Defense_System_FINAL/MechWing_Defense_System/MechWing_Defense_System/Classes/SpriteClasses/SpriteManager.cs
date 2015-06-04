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


namespace MechWing_Defense_System.Classes.SpriteClasses
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class SpriteManager : Microsoft.Xna.Framework.DrawableGameComponent
    {
        //SpriteBatch for drawing
        SpriteBatch spriteBatch;

        Texture2D bulletSprite;
        //A sprite for the player and a list of automated sprites
        //UserControlledSprite player;
        //UserControlledSprite crosshair;

        int type;

        // Variables for spawning enemyFighters; Added on 4/15/14
        int enemyFighterSpawnMinMilliseconds = 1000;
        int enemyFighterSpawnMaxMilliseconds = 2000;
        int enemyFighterMinSpeed = 4;
        int enemyFighterMaxSpeed = 10;
        int nextSpawnTime = 0;

        // Variables for randomly spawned enemies; Added on 5/1/14
        int likelihoodAutomated = 75;
        int likelihoodChasing = 20;
        int likelihoodEvading = 5;


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
            

            //Load the player sprite
            if (gamestate == 2)
            {

                /*spriteBatch = new SpriteBatch(Game.GraphicsDevice);

                // Instantiate a player object; Added on 4/15/14
                //player = new UserControlledSprite(
                        Game.Content.Load<Texture2D>("Images/main_char_spritesheet"),
                        Vector2.Zero, new Point(105, 130), 10, new Point(0, 0),
                        new Point(6, 1), new Vector2(7, 7));

                // Instantiate a crosshair object; Added on 4/15/14
                //crosshair = new MouseSprite(Game.Content.Load<Texture2D>("Images/crosshair"),
                    Vector2.Zero, new Point(50, 50), 5, new Point(0, 0),
                    new Point(1, 1), new Vector2(0, 0));
                
                /*player = new UserControlledSprite(
                    Game.Content.Load<Texture2D>("main_char_spritesheet"),
                    Vector2.Zero, new Point(105, 130), 10, new Point(0, 0),
                    new Point(6, 1), new Vector2(7, 7));

               // crosshair = new MouseSprite(Game.Content.Load<Texture2D>("crosshair"),
                    Vector2.Zero, new Point(50, 50), 5, new Point(0, 0),
                    new Point(1, 1), new Vector2(0, 0));
                spriteList.Add(player);
                spriteList.Add(crosshair);

                //Load several different automated sprites into the list
                spriteList.Add(new AutomatedSprite(
                    Game.Content.Load<Texture2D>(@"Images/skullball"),
                    new Vector2(150, 150), new Point(75, 75), 10, new Point(0, 0),
                    new Point(6, 8), Vector2.Zero));
                spriteList.Add(new AutomatedSprite(
                    Game.Content.Load<Texture2D>(@"Images/skullball"),
                    new Vector2(300, 150), new Point(75, 75), 10, new Point(0, 0),
                    new Point(6, 8), Vector2.Zero));
                spriteList.Add(new AutomatedSprite(
                    Game.Content.Load<Texture2D>(@"Images/skullball"),
                    new Vector2(150, 300), new Point(75, 75), 10, new Point(0, 0),
                    new Point(6, 8), Vector2.Zero));
                spriteList.Add(new AutomatedSprite(
                    Game.Content.Load<Texture2D>(@"Images/skullball"),
                    new Vector2(600, 400), new Point(75, 75), 10, new Point(0, 0),
                    new Point(6, 8), Vector2.Zero));

                /*
                 * 
                 * This code was added on 3/1/2014
                 * It loads two skullball and two plus BouncingSprites into the list
                 * 
            
                spriteList.Add(new BouncingSprite(
                    Game.Content.Load<Texture2D>(@"Images/skullball"),
                    new Vector2(150, 150), new Point(75, 75), 10, new Point(0, 0),
                    new Point(6, 8), new Vector2(1,1)));
                spriteList.Add(new BouncingSprite(
                    Game.Content.Load<Texture2D>(@"Images/skullball"),
                    new Vector2(300, 150), new Point(75, 75), 10, new Point(0, 0),
                    new Point(6, 8), new Vector2(1,1)));
                spriteList.Add(new BouncingSprite(
                    Game.Content.Load<Texture2D>(@"Images/plus"),
                    new Vector2(150, 300), new Point(75, 75), 10, new Point(0, 0),
                    new Point(6, 4), new Vector2(1,1)));
                spriteList.Add(new BouncingSprite(
                    Game.Content.Load<Texture2D>(@"Images/plus"),
                    new Vector2(600, 400), new Point(75, 75), 10, new Point(0, 0),
                    new Point(6, 4), new Vector2(1,1)));
                */

                bulletSprite = Game.Content.Load<Texture2D>("Images/enemyBullet");
                spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            }
           

            else
            {
                base.LoadContent();
            }
        }

        // Method to set the next spawn time; Added on 4/15/14
        private void ResetSpawnTime()
        {
            nextSpawnTime = ((Game1)Game).rnd.Next(
                enemyFighterSpawnMinMilliseconds,
                enemyFighterSpawnMaxMilliseconds);
        }

        // Method to spawn an enemy; Added on 4/15/14
        private void SpawnEnemy(int gameState)
        {
            Vector2 speed = Vector2.Zero;
            Vector2 position = Vector2.Zero;

            // Default frame size
            Point frameSize = new Point(75, 75);
            // Randomly choose which side of the screen to place enemy,
            // then randomly create a position along that side of the screen
            // and randomly choose a speed for the enemy
            /*switch (((Game1)Game).rnd.Next(4))
            {
                case 0: // Left to right
                    position = new Vector2(
                        -frameSize.X, ((Game1)Game).rnd.Next(0,
                        Game.GraphicsDevice.PresentationParameters.BackBufferHeight
                        - frameSize.Y));

                    speed = new Vector2(((Game1)Game).rnd.Next(
                        enemyFighterMinSpeed,
                        enemyFighterMaxSpeed), 0);
                    break;*/

                //case 1: // Right to left
                    position = new Vector2(
                        Game.GraphicsDevice.PresentationParameters.BackBufferWidth,
                        ((Game1)Game).rnd.Next(0,
                        Game.GraphicsDevice.PresentationParameters.BackBufferHeight
                        - frameSize.Y));

                    speed = new Vector2(-((Game1)Game).rnd.Next(
                        enemyFighterMinSpeed,
                        enemyFighterMaxSpeed), 0);
                    //break;

                /*case 2: // Bottom to top
                    position = new Vector2(((Game1)Game).rnd.Next(0,
                        Game.GraphicsDevice.PresentationParameters.BackBufferWidth
                        - frameSize.X));

                    speed = new Vector2(0, -((Game1)Game).rnd.Next(
                        enemyFighterMinSpeed,
                        enemyFighterMaxSpeed));
                    break;

                case 3: // Top to bottom
                    position = new Vector2(
                        ((Game1)Game).rnd.Next(0,
                        Game.GraphicsDevice.PresentationParameters.BackBufferWidth
                        - frameSize.X), -frameSize.Y);

                    speed = new Vector2(0, ((Game1)Game).rnd.Next(
                        enemyFighterMinSpeed,
                        enemyFighterMaxSpeed));
                    break;

            }*/

            /*
             * Added on 5/1/14
             * Used to randomly spawn different types of enemies
             */

            // Get random number from 0 to 99
            int random = ((Game1)Game).rnd.Next(100);

            if (gameState == 4)
            {
                if (random < likelihoodAutomated)
                {
                    // Create an Automated sprite
                    spriteList.Add(
                        new AutomatedSprite(Game.Content.Load<Texture2D>("Images/enemyFighter1"),
                        position, new Point(117, 44), 10, new Point(1, 1),
                        new Point(1, 1), speed, 50, 15, 5));
                }
                else if (random < likelihoodAutomated + likelihoodChasing)
                {
                    // Create a Chasing sprite
                    // Get new random number to determine whether to create an EnemyFighter2 or an EnemyFighter3
                    if (((Game1)Game).rnd.Next(2) == 0)
                    {
                        // Create EnemyFighter2
                        spriteList.Add(
                            new ChasingSprite(
                                Game.Content.Load<Texture2D>("Images/enemyFighter2"),
                                position, new Point(120, 58), 10, new Point(0, 0),
                                new Point(1, 1), speed, 100, 40, 20, this));
                    }
                    else
                    {
                        // Create EnemyFighter3 sprite
                        spriteList.Add(
                            new ChasingSprite(
                                Game.Content.Load<Texture2D>("Images/enemyFighter3"),
                                position, new Point(150, 88), 10, new Point(6, 8),
                                new Point(1, 1), speed, 150, 50, 200, this));
                    }
                }
                else
                {
                    // Get new random number to determine whether to create an evading gold chunk or a bouncing gold chunk
                    /*if (((Game1)Game).rnd.Next(2) == 0)
                    {
                        // Create an EvadingSprite
                        spriteList.Add(
                            new EvadingSprite(
                                Game.Content.Load<Texture2D>("Images/Gold_Chunk"),
                                position, new Point(75, 75), 10, new Point(0, 0),
                                new Point(1, 1), speed, 20, 0, 100, this));
                    }
                    else
                    {
                        // Create an BouncingSprite
                        spriteList.Add(
                            new BouncingSprite(
                                Game.Content.Load<Texture2D>("Images/Gold_Chunk"),
                                position, new Point(75, 75), 10, new Point(0, 0),
                                new Point(1, 1), speed, 20, 0, 100));
                    }
                     */
                }
            }

            if (gameState == 6)
            {
                if (random < likelihoodAutomated)
                {
                    // Create an Automated sprite
                    spriteList.Add(
                        new AutomatedSprite(Game.Content.Load<Texture2D>("Images/enemyFighter1"),
                        position, new Point(117, 44), 10, new Point(1, 1),
                        new Point(1, 1), speed, 50, 15, 5));
                }
                else if (random < likelihoodAutomated + likelihoodChasing)
                {
                    // Create a Chasing sprite
                    // Get new random number to determine whether to create an EnemyFighter2 or an EnemyFighter3
                    if (((Game1)Game).rnd.Next(2) == 0)
                    {
                        // Create EnemyFighter2
                        spriteList.Add(
                            new ChasingSprite(
                                Game.Content.Load<Texture2D>("Images/enemyFighter2"),
                                position, new Point(120, 58), 10, new Point(0, 0),
                                new Point(1, 1), speed, 100, 40, 20, this));
                    }
                    else
                    {
                        // Create EnemyFighter3 sprite
                        spriteList.Add(
                            new ChasingSprite(
                                Game.Content.Load<Texture2D>("Images/enemyFighter3"),
                                position, new Point(150, 88), 10, new Point(6, 8),
                                new Point(1, 1), speed, 150, 50, 200, this));
                    }
                }
                else
                {
                    // Get new random number to determine whether to create an evading gold chunk or a bouncing gold chunk
                    /*if (((Game1)Game).rnd.Next(2) == 0)
                    {
                        // Create an EvadingSprite
                        spriteList.Add(
                            new EvadingSprite(
                                Game.Content.Load<Texture2D>("Images/Gold_Chunk"),
                                position, new Point(75, 75), 10, new Point(0, 0),
                                new Point(1, 1), speed, 20, 0, 100, this));
                    }
                    else
                    {
                        // Create an BouncingSprite
                        spriteList.Add(
                            new BouncingSprite(
                                Game.Content.Load<Texture2D>("Images/Gold_Chunk"),
                                position, new Point(75, 75), 10, new Point(0, 0),
                                new Point(1, 1), speed, 20, 0, 100));
                    }
                     */
                }
            }



            // Create the chasing sprite; Added on 4/15/14
            /*spriteList.Add(new ChasingSprite(Game.Content.Load<Texture2D>("Images/enemyFighter2"),
                position, new Point(75, 75), 10, new Point(0, 0),
                new Point(1,1), speed, 100, 40, 20, this));

            // Create the evading sprite; Added on 4/15/14
            spriteList.Add(new EvadingSprite(Game.Content.Load<Texture2D>("Images/Gold_Chunk"),
                position, new Point(75, 75), 10, new Point(0, 0),
                new Point(1, 1), speed, 50, -10, 0, this));

            // Create the sprite
            spriteList.Add(new AutomatedSprite(Game.Content.Load<Texture2D>("Images/enemyFighter1"),
                position, new Point(63, 38), 10, new Point(1, 1),
                new Point(1, 1), speed, 50, 15, 5));*/
        }

        // method to get player's position so chasingSprite can close on them; Added on 4/15/14
        /*public Vector2 GetPlayerPosition()
        {
            return player.GetPosition;
        }
        */
        //adds a sprite to the sprite list (player, bullet, enemy, whatever)
        public void AddSpriteToList(Sprite newSprite)
        {
            spriteList.Add(newSprite);
        }

        //REAL UPDATE CODE, PROBABLY
        public void Update(GameTime gameTime, int gameState, Sprite playerSprite)
        {

            if (gameState == 4 || gameState == 6 || gameState == 8)
            {

                //spawns enemies if the type is 1
                if (type == 1)
                {
                    nextSpawnTime -= gameTime.ElapsedGameTime.Milliseconds;
                    if (nextSpawnTime < 0)
                    {
                        SpawnEnemy(gameState);

                        //Reset spawn timer
                        ResetSpawnTime();
                    }
                }
                // Update all sprites; Updated on 4/15/14
                for (int i = 0; i < spriteList.Count; ++i)
                {
                    Sprite s = spriteList[i];
                    s.Update(gameTime, Game.Window.ClientBounds, playerSprite);

                    if(s.IsTimeToShoot())
                    {
                        Vector2 bulletVector = Shoot(s.position, playerSprite.position);
                        spriteList.Add(new Bullet(bulletSprite, s.position, new Point(15, 15), 2, new Point(1, 1), new Point(1, 1), bulletVector, 1, s.damage, 0));
                    }
                 
                    // Remove object if it is out of bounds; Added on 4/15/14
                    if (s.IsOutOfBounds(Game.Window.ClientBounds))
                    {
                        spriteList.RemoveAt(i);
                        --i;
                    }

                }
            }
            base.Update(gameTime);
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public void Draw(GameTime gameTime, int gameState)
        {
            if (gameState == 4 || gameState == 6)
            {
                spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);

                // Draw the player
                

                // Draw all sprites
                foreach (Sprite s in spriteList)
                    s.Draw(gameTime, spriteBatch);

                spriteBatch.End();
            }

            else if (gameState == 8)
            {
                spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);

                // Draw the player


                // Draw all sprites
                foreach (Sprite s in spriteList)
                    s.Draw(gameTime, spriteBatch);

                spriteBatch.End();
            }


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
                    playerLogic.DealDamage(s.damage);
                    spriteList.RemoveAt(i);
                }

                
            }
        }
    }
}
