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
using Clockwork.Classes.CameraClasses;
using Clockwork.Classes.StaticItemClasses;
using Clockwork.Classes.PhysicsClasses;

namespace Clockwork
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        //graphics stuff
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Camera playerCamera;

        //actors
        Player playerOne;

        //static items
        StaticItemMapList staticItems;

        //lighting things
        double maxLightRange = 300;
               
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);          
            Content.RootDirectory = "Content"; 
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            staticItems = new StaticItemMapList();
            Texture2D tempChairSprite = Content.Load<Texture2D>("MapSprites/SP_Chair_Bed");
            StaticItem randomChair = new StaticItem(new Vector2(500, 400), new Rectangle(234, 154, 59, 82), "It's a bed.", true);
            StaticItem randomCouch = new StaticItem(new Vector2(300, 150), new Rectangle(392, 73, 68, 35), "It's a couch.", true);
            StaticItemMap tempMap = new StaticItemMap(tempChairSprite);
            tempMap.AddStaticItem(randomChair);
            tempMap.AddStaticItem(randomCouch);
            staticItems.AddToList(tempMap);

            Texture2D playerSpriteTex = Content.Load<Texture2D>("CharacterSprites/MainCharacterLarge");
            Texture2D tempBulletTex = Content.Load<Texture2D>("BulletSprites/bulletSprites");
            Texture2D tempGunTex = Content.Load<Texture2D>("GunSprites/ShotgunLarge");
            Texture2D tempGunTex2 = Content.Load<Texture2D>("GunSprites/HandgunLarge");

            UserControlledSprite tempPlayerSprite = new UserControlledSprite(playerSpriteTex, new Vector2(300, 300), new Point(playerSpriteTex.Width / 4, playerSpriteTex.Height / 4),
                3, new Point(0, 0), new Point(4, 4), new Vector2(2));

            playerOne = new Player(100, 100, 15, tempPlayerSprite, tempBulletTex);

            Viewport tempViewport = GraphicsDevice.Viewport;
            tempViewport.Height = 480;
            tempViewport.Width = 720;
            playerCamera = new Camera(tempViewport, playerSpriteTex);

            Weapon shotgun = new AmmoBasedWeapon("Shotgun", 2, tempGunTex, tempGunTex, 50, 1000, new Point(1, 1), new Point(tempGunTex.Width / 5, tempGunTex.Height / 4), new Point(5, 4), 5, 5, 7, tempBulletTex);
            Weapon handgun = new AmmoBasedWeapon("Handgun", 1, tempGunTex2, tempGunTex2, 10, 300, new Point(1, 1), new Point(tempGunTex2.Width / 4, tempGunTex2.Height / 4), new Point(4, 4), 10, 10, 10, tempBulletTex);

            playerOne.playerInventory.currentWeapon = shotgun;

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();


                playerOne.Update(gameTime, this.Window.ClientBounds);
                CollisionChecker.CheckCollisions(playerOne, staticItems);
                playerCamera.LookAt(playerOne.playerSprite.position);
            
            base.Update(gameTime);

        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            // TODO: Add your drawing code here

            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, playerCamera.GetViewMatrix(new Vector2(1.0f)));
            foreach(StaticItemMap map in staticItems.staticItemMapList)
            {
                foreach(StaticItem item in map.mapItems)
                {
                    double opacity = 0;

                    /*foreach(StaticLight light in StaticLightMap)
                    {
                    }
                    */

                    float centerX = item.position.X + (item.itemFrame.Width / 2);
                    float centerY = item.position.Y + (item.itemFrame.Height / 2);
                    float playerX = playerOne.playerSprite.position.X + (playerOne.playerSprite.frameSize.X / 2);
                    float playerY = playerOne.playerSprite.position.Y + (playerOne.playerSprite.frameSize.Y / 2);
                    double distX = playerX - centerX;
                    double distY = playerY - centerY;
                    double dist = Math.Sqrt((distX * distX) + (distY * distY));

                    if(dist > maxLightRange)
                    {
                        opacity = 0;
                    }

                    else
                    {
                        opacity = 1 - (dist / maxLightRange);
                    }

                    item.Draw(gameTime, spriteBatch, map.spriteSheet, opacity);
                }
            }
            //staticItems.Draw(gameTime, spriteBatch);
            playerOne.Draw(gameTime, spriteBatch);
            
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
