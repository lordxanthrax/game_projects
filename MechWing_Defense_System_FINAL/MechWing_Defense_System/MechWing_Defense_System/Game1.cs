/*
 * Just For Fun
 * Andersen, Corey; Gan, Jie; Olson, Darwin; Zhang, Shujian
 * CSCI 313   Section #1
 * 5/9/2014
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MechWing_Defense_System.Classes;
using MechWing_Defense_System.Classes.SpriteClasses;

namespace MechWing_Defense_System
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // Variable to handle random factor; Added on 4/15/14
        public Random rnd { get; private set; }

        /*VERY IMPORTANT
        1 -- title screen
        2 -- loading content
        3 -- level one splash screen
        4 -- level one
        5 -- level two splash
        6 -- level two
        7 -- level three splash
        8 -- level three
        9 -- ending?
        100 -- game over
        */
        int gameState;

        double level1SpawnTimer;
        double timeSinceMissileLastShot;
        
        // Variable to store a song; Added on 4/15/14
        Song backgroundMusic;

        // Variable to say wether or not the song should loop; Added on 4/15/14
        public static bool IsRepeating { get; set; }

        //container for all of the objects 
        SpriteManager enemySprites;
        SpriteManager playerBullets;
        SpriteManager explosions;
        SpriteManager powerups;
        
        //some random mouse stuff
        MouseState currMouseState;
        MouseState prevMouseState;

        //keyboard for missiles
        KeyboardState keyboardPrevState;

        //player stuff
        UserControlledSprite player;
        MouseSprite crosshairs;
        Player playerLogic;

        //boss stuff
        Boss gameBoss;
        

        //bullet stuff
        Texture2D bulletSprite;
        Texture2D missileSprite;
        Texture2D droneSprite;
        

        //enemy sprites
        Texture2D enemyFighter1Sprite;
        Texture2D enemyFighter2Sprite;

        // More enemy sprites added on 4/24/14
        Texture2D enemyFighter3Sprite;
        Texture2D enemyFighter4Sprite;
        Texture2D enemyFighter5Sprite;
        Texture2D levelBoss2;
        Texture2D bossSprite;


        //explosion
        Texture2D explosionSprite;

        //healthbar
        Texture2D healthBarSprite;

        //score
        int playerScore;
        SpriteFont scoreFont;

        // Game over screen variable added on 4/21/14
        GameOverScreen gameOverScreen;
        int gameOverMusicPlayed;

        //randommusiccrap
        Boolean level2MusicIsPlaying;
        Boolean titleScreenMusicIsPlaying;
        Boolean bossMusicPlaying;
        Boolean endingMusicPlaying;

        //titlescreencrap
        Rectangle titleScreenBox;
        Texture2D titleScreenSprite;
        Rectangle titleScreenSelectorBox;

        Rectangle levelOneSplashScreenBox;
        Texture2D levelOneSplashScreenSprite;

        Rectangle levelTwoSplashScreenBox;
        Texture2D levelTwoSplashScreenSprite;

        Rectangle levelBossSplashScreenBox;
        Texture2D levelBossSplashScreenSprite;

        Rectangle endingSplashScreenBox;
        Texture2D endingSplashScreenSprite;

        Rectangle instructionsBox;
        Texture2D instructionsSprite;

        int selectedOption;
        Boolean drawInstructions;

        Texture2D yellowRectangle;

        //bg infos
        ScrollingBackground levelOneBackground;

        ScrollingBackground levelTwoBackground;

        Texture2D levelBossBackground;
        Rectangle levelBossBGRectangle;

        //more stuff i need to make a class for
        Vector2 bulletVector;

        //placeholders
        Sprite hitEnemy;
        Sprite hitBullet;
        Sprite hitExplosion;
        Sprite hitPowerup;

        //soundeffects
        SoundEffect explosionSFX;
        // Game over sound effect added 4/24/14
        //private SoundEffect gameOver;
        //private SoundEffectInstance gameOver1;

        private AudioEngine audioEngine;
        private WaveBank waveBank;
        private SoundBank soundBank;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 800;
            graphics.PreferredBackBufferWidth = 1280;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();

            Content.RootDirectory = "Content";

            // Initialize the Random object; Added on 4/15/14
            rnd = new Random();
            
            //set initial gamestate
            gameState = 1;

            //score
            playerScore = 0;

            //timerstuff
            timeSinceMissileLastShot = 2001;

            //randomplaceholder
            gameOverMusicPlayed = 0;
            titleScreenMusicIsPlaying = false;
            level2MusicIsPlaying = false;
            endingMusicPlaying = false;

            //titlestuff
            titleScreenBox = new Rectangle(0, 0, 1280, 800);
            titleScreenSelectorBox = new Rectangle(384, 686, 470, 40);
            selectedOption = 0;

            //splash screen rectangles
            levelOneSplashScreenBox = new Rectangle(0, 0, 1280, 800);
            levelTwoSplashScreenBox = new Rectangle(0, 0, 1280, 800);
            levelBossSplashScreenBox = new Rectangle(0, 0, 1280, 800);
            endingSplashScreenBox = new Rectangle(0, 0, 1280, 800);

            levelBossBGRectangle = new Rectangle(0, 0, 1280, 800);

            instructionsBox = new Rectangle(66, 365, 668, 247);

            
            
           
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
           
            //make sure everything is set to something so we don't get null errors
            currMouseState = new MouseState();
            prevMouseState = new MouseState();

            keyboardPrevState = new KeyboardState();

            enemySprites = new SpriteManager(this, 1);
            playerBullets = new SpriteManager(this, 2);
            explosions = new SpriteManager(this, 3);
            powerups = new SpriteManager(this, 4);

            Components.Add(enemySprites);
            Components.Add(playerBullets);
            Components.Add(explosions);
            Components.Add(powerups);
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

            //healthbar
            healthBarSprite = Content.Load<Texture2D>("Images/healthbar");

            //score font
            scoreFont = Content.Load<SpriteFont>("ScoreFont");

            //player stuff
            player = new UserControlledSprite(
                    Content.Load<Texture2D>("Images/main_char_spritesheet"),
                    Vector2.Zero, new Point(105, 130), 10, new Point(0, 0),
                    new Point(6, 1), new Vector2(7, 7), 100, 100, 0);

            crosshairs = new MouseSprite(Content.Load<Texture2D>("Images/crosshairs"),
                Vector2.Zero, new Point(50, 50), 5, new Point(0, 0),
                new Point(1, 1), new Vector2(0, 0), 100, 100, 0);


            bossSprite = Content.Load<Texture2D>("Images/boss");
           

            //titlescreen stuff
            titleScreenSprite = Content.Load<Texture2D>("Images/mechwing_titlescreen");
            instructionsSprite = Content.Load<Texture2D>("Images/instructions");
            levelOneSplashScreenSprite = Content.Load<Texture2D>("Images/splash/mechwing1");
            levelTwoSplashScreenSprite = Content.Load<Texture2D>("Images/splash/mechwing2");
            levelBossSplashScreenSprite = Content.Load<Texture2D>("Images/splash/mechwing3");
            endingSplashScreenSprite = Content.Load<Texture2D>("Images/splash/mechwing4");

            //titlescreen selector
            yellowRectangle = new Texture2D(GraphicsDevice, 1, 1);
            yellowRectangle.SetData(new[] { Color.Yellow });

            //backgrounds stuff
            levelOneBackground = new ScrollingBackground();
            Texture2D levelOneBGSprite = Content.Load<Texture2D>("Images/cityscape");
            levelOneBackground.Load(GraphicsDevice, levelOneBGSprite);

            levelTwoBackground = new ScrollingBackground();
            Texture2D levelTwoBGSprite = Content.Load<Texture2D>("Images/skybackground");
            levelTwoBackground.Load(GraphicsDevice, levelTwoBGSprite);

            
            
            //SFX
            explosionSFX = Content.Load<SoundEffect>("Audio/ExploClassic");

            // Call to load sound effect; Added on 4/25/14
            LoadAudioContent();

            // Load game over sound effect; Added on 4/24/14
            /*gameOver = Content.Load<SoundEffect>("Audio/smb_mariodie");
            gameOver1 = gameOver.CreateInstance();
            // Added to try to get the damn sound effect to play only once
            gameOver1.IsLooped = false;*/

            //bullet stuff
            bulletSprite = Content.Load<Texture2D>("Images/bullet");
            missileSprite = Content.Load<Texture2D>("Images/missile");

            droneSprite = Content.Load<Texture2D>("Images/drone");

            //explosion stuff
            explosionSprite = Content.Load<Texture2D>("Images/explosionSprites");

            
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            backgroundMusic.Dispose();
        }

        private void LoadAudioContent()
        {
            audioEngine = new AudioEngine("Content/Audio/GameOver.xgs");
            waveBank = new WaveBank(audioEngine, "Content/Audio/Wave Bank.xwb");
            soundBank = new SoundBank(audioEngine, "Content/Audio//Sound Bank.xsb");
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {

            //title screen
            if (gameState == 1)
            {
                if (titleScreenMusicIsPlaying == false)
                {
                    backgroundMusic = Content.Load<Song>("Audio/Vengeance");
                    MediaPlayer.Stop();
                    MediaPlayer.Play(backgroundMusic);
                    MediaPlayer.IsRepeating = true;
                    titleScreenMusicIsPlaying = true;                   
                }

                DoTitleScreen(gameTime);
            }

            //load level 1 stuff
            if (gameState == 2)
            {
                // Plays the backgroundMusic on a loop; Added on 4/15/14
                titleScreenMusicIsPlaying = false;
                backgroundMusic = Content.Load<Song>("Audio/Nullsleep - Dirty ROM Dance Mix");
                MediaPlayer.Stop();
                MediaPlayer.Play(backgroundMusic);
                MediaPlayer.IsRepeating = true;

                enemyFighter1Sprite = Content.Load<Texture2D>("Images/enemyFighter1");
                // Initialize the enemyFighter2Sprite; Added on 4/15/14
                enemyFighter2Sprite = Content.Load<Texture2D>("Images/enemyFighter2");
                // Initialize enemies; Added on 4/25/14
                enemyFighter3Sprite = Content.Load<Texture2D>("Images/enemyFighter3");
                enemyFighter4Sprite = Content.Load<Texture2D>("Images/enemyFighter4");
                enemyFighter5Sprite = Content.Load<Texture2D>("Images/enemyFighter5");
                levelBoss2 = Content.Load<Texture2D>("Images/levelBoss2");

                missileSprite = Content.Load<Texture2D>("Images/missile");

                enemySprites.LoadContent(gameState);
                playerLogic = new Player(100, 100, 50, 10, healthBarSprite);
                gameState = 3;
            }

            //go past splash screen
            if (gameState == 3)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    gameState = 4;
                }
            }

            //logic for level 1
            if(gameState == 4)
            {
                 playerLogic.Update(gameTime);

                level1SpawnTimer += 16.6667;
                timeSinceMissileLastShot += 16.6667;
                CheckForSpawning(level1SpawnTimer);

                DoPlayerInput();

                //update the level's background sprite
                player.Update(gameTime, this.Window.ClientBounds, player);
                crosshairs.Update(gameTime, this.Window.ClientBounds);
                enemySprites.Update(gameTime, gameState, player);
                playerBullets.Update(gameTime, gameState, player);
                explosions.Update(gameTime, gameState, player);
                powerups.Update(gameTime, gameState, player);

                levelOneBackground.Update(5);

                foreach(Sprite up in powerups.spriteList)
                {
                    if(up.collisionRect.Intersects(player.collisionRect))
                    {
                        hitPowerup = up;
                    }
                }

                if(hitPowerup != null)
                {
                    playerLogic.Heal(hitPowerup.damage);
                    powerups.spriteList.Remove(hitPowerup);
                }

                hitPowerup = null;

                foreach(Sprite es in enemySprites.spriteList)
                {                    

                    foreach(Sprite bs in playerBullets.spriteList)
                    {                  

                        if(es.collisionRect.Intersects(bs.collisionRect))
                        {
                           
                            explosions.AddSpriteToList(new SelfTerminatingSprite(explosionSprite, es.position, new Point(50,50), 0, new Point(1,1), new Point(9,1), Vector2.Zero, 33, 100, 100, 0));
                            
                            hitEnemy = es;
                            hitBullet = bs;
                            
                        }
                    }
                }

                if (hitEnemy != null && hitBullet != null)
                {
                    int index = enemySprites.spriteList.IndexOf(hitEnemy);
                    enemySprites.spriteList.ElementAt(index).DealDamage(hitBullet.damage);

                    if (enemySprites.spriteList.ElementAt(index).health <= 0)
                    {
                        AddScore(enemySprites.spriteList.ElementAt(index).playerScore);
                        enemySprites.spriteList.Remove(hitEnemy);
                        //playerScore += 25;
                    }

                    playerBullets.spriteList.Remove(hitBullet);
                    explosionSFX.Play(0.2f, 0.0f, 0.0f);

                }

                hitEnemy = null;
                hitBullet = null;

                enemySprites.CheckPlayerCollisions(player, ref playerLogic);

                foreach (SelfTerminatingSprite expS in explosions.spriteList)
                {
                    if (expS.animationFinished)
                    {
                        hitExplosion = expS;
                    }
                }

                if (hitExplosion != null)
                {
                    explosions.spriteList.Remove(hitExplosion);
                }

                hitExplosion = null;

                if (level1SpawnTimer > 45000)
                {
                    MediaPlayer.Stop();
                    enemySprites.Dispose();
                    enemySprites = new SpriteManager(this, 1);
                    enemySprites.LoadContent(2);

                    explosions.Dispose();
                    explosions = new SpriteManager(this, 3);
                    explosions.LoadContent(2);

                    playerBullets.Dispose();
                    playerBullets = new SpriteManager(this, 2);
                    playerBullets.LoadContent(2);

                    gameState = 5;
                    level1SpawnTimer = 0;
                }
               
            }

            //set up level 2
            if (gameState == 5)
            {
                if (level2MusicIsPlaying == false)
                {
                    MediaPlayer.Stop();
                    backgroundMusic = Content.Load<Song>("Audio/Incident");
                    MediaPlayer.Play(backgroundMusic);
                    level2MusicIsPlaying = true;
                }

                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    gameState = 6;
                }
            }

            //logic for level 2
            if (gameState == 6)
            {
                level2MusicIsPlaying = false;

                //update the player
                playerLogic.Update(gameTime);

                level1SpawnTimer += 16.6667;
                timeSinceMissileLastShot += 16.6667;
                CheckForSpawning(level1SpawnTimer);

                DoPlayerInput();

                //update all of the sprites
                player.Update(gameTime, this.Window.ClientBounds, player);
                crosshairs.Update(gameTime, this.Window.ClientBounds);
                enemySprites.Update(gameTime, gameState, player);
                playerBullets.Update(gameTime, gameState, player);
                explosions.Update(gameTime, gameState, player);
                powerups.Update(gameTime, gameState, player);

                levelTwoBackground.Update(5);

                foreach(Sprite up in powerups.spriteList)
                {
                    if(up.collisionRect.Intersects(player.collisionRect))
                    {
                        hitPowerup = up;
                    }
                }

                if(hitPowerup != null)
                {
                    playerLogic.Heal(hitPowerup.damage);
                    powerups.spriteList.Remove(hitPowerup);
                }

                hitPowerup = null;

                foreach (Sprite es in enemySprites.spriteList)
                {

                    foreach (Sprite bs in playerBullets.spriteList)
                    {

                        if (es.collisionRect.Intersects(bs.collisionRect))
                        {

                            explosions.AddSpriteToList(new SelfTerminatingSprite(explosionSprite, es.position, new Point(50, 50), 0, new Point(1, 1), new Point(9, 1), Vector2.Zero, 33, 100, 100, 0));

                            hitEnemy = es;
                            hitBullet = bs;

                        }
                    }
                }

                if (hitEnemy != null && hitBullet != null)
                {
                    int index = enemySprites.spriteList.IndexOf(hitEnemy);
                    enemySprites.spriteList.ElementAt(index).DealDamage(hitBullet.damage);

                    if (enemySprites.spriteList.ElementAt(index).health <= 0)
                    {
                        AddScore(enemySprites.spriteList.ElementAt(index).playerScore);
                        enemySprites.spriteList.Remove(hitEnemy);
                        

                        // I want to get the score value from each different enemy but, I am having trouble figuring it out.
                        
                    }

                    playerBullets.spriteList.Remove(hitBullet);
                    explosionSFX.Play(0.2f, 0.0f, 0.0f);
                    
                }

                hitEnemy = null;
                hitBullet = null;

                enemySprites.CheckPlayerCollisions(player, ref playerLogic);

                foreach (SelfTerminatingSprite expS in explosions.spriteList)
                {
                    if (expS.animationFinished)
                    {
                        hitExplosion = expS;
                    }
                }

                if (hitExplosion != null)
                {
                    explosions.spriteList.Remove(hitExplosion);
                }

                hitExplosion = null;

                if (level1SpawnTimer > 45000)
                {
                    MediaPlayer.Stop();
                    enemySprites.Dispose();
                    enemySprites = new SpriteManager(this, 1);
                    enemySprites.LoadContent(2);

                    explosions.Dispose();
                    explosions = new SpriteManager(this, 3);
                    explosions.LoadContent(2);

                    playerBullets.Dispose();
                    playerBullets = new SpriteManager(this, 2);
                    playerBullets.LoadContent(2);

                    gameState = 7;
                    level1SpawnTimer = 0;
                }

                
            }

            //load stuff for boss level
            if (gameState == 7)
            {
                if(bossMusicPlaying == false)
                {                   
                    MediaPlayer.Stop();
                    backgroundMusic = Content.Load<Song>("Audio/Nemesis");
                    MediaPlayer.Play(backgroundMusic);
                    bossMusicPlaying = true;
                    gameBoss = new Boss(4000, bossSprite, healthBarSprite, this, droneSprite);
                    
                    
                    

                    levelBossBackground = Content.Load<Texture2D>("Images/nebula");
                }

                if(Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    gameState = 8;
                }
            }

            //boss level
            if (gameState == 8)
            {
                bossMusicPlaying = false;
                //update player stuff
                playerLogic.Update(gameTime);
                timeSinceMissileLastShot += 16.6667;

                DoPlayerInput();

                //update all of the sprites
                player.Update(gameTime, this.Window.ClientBounds, player);
                crosshairs.Update(gameTime, this.Window.ClientBounds);
                playerBullets.Update(gameTime, gameState, player);
                explosions.Update(gameTime, gameState, player);
                gameBoss.Update(gameTime, player, this, gameState);

                gameBoss.bossBullets.CheckPlayerCollisions(player, ref playerLogic);

                if (player.collisionRect.Intersects(gameBoss.bossSprite.collisionRect))
                {
                    player.position += new Vector2(-50, 0);
                    playerLogic.DealDamage(20);
                }


                foreach (Sprite bs in playerBullets.spriteList)
                {

                    if (gameBoss.bossSprite.collisionRect.Intersects(bs.collisionRect))
                    {

                        explosions.AddSpriteToList(new SelfTerminatingSprite(explosionSprite, bs.position, new Point(50, 50), 0, new Point(1, 1), new Point(9, 1), Vector2.Zero, 33, 100, 100, 0));

                        
                        hitBullet = bs;

                    }
                }

                if (hitBullet != null)
                {
                    gameBoss.DealDamage(hitBullet.damage);
                    playerBullets.spriteList.Remove(hitBullet);
                    explosionSFX.Play(0.2f, 0.0f, 0.0f);
                }

                hitBullet = null;

                foreach (Sprite bb in gameBoss.bossBullets.spriteList)
                {
                    foreach (Sprite pb in playerBullets.spriteList)
                    {
                        if (bb.collisionRect.Intersects(pb.collisionRect))
                        {
                            hitBullet = pb;
                            hitEnemy = bb;
                        }
                    }
                }

                if (hitBullet != null && hitEnemy != null)
                {
                    playerBullets.spriteList.Remove(hitBullet);
                    gameBoss.bossBullets.spriteList.Remove(hitEnemy);
                }

                hitBullet = null;
                hitEnemy = null;

                foreach (SelfTerminatingSprite expS in explosions.spriteList)
                {
                    if (expS.animationFinished)
                    {
                        hitExplosion = expS;
                    }
                }

                if (hitExplosion != null)
                {
                    explosions.spriteList.Remove(hitExplosion);
                }

                hitExplosion = null;

                if (gameBoss.bossHealth <= 0)
                {
                    playerScore += 2000;
                    gameState = 9;
                }
            }

            //do ending thing
            if (gameState == 9)
            {
                if (endingMusicPlaying == false)
                {
                    MediaPlayer.Stop();
                    backgroundMusic = Content.Load<Song>("Audio/Ignite");
                    MediaPlayer.Play(backgroundMusic);

                    bossMusicPlaying = false;

                    endingMusicPlaying = true;
                }

                if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                {
                    this.Exit();
                }
            }

            if (playerLogic != null && gameState != 100)
            {
                if (playerLogic.playerHealth <= 0)
                {
                    //reset gates for music
                    titleScreenMusicIsPlaying = false;
                    level2MusicIsPlaying = false;
                    bossMusicPlaying = false;
                    gameOverMusicPlayed = 0;

                    //reset some stuff
                    gameState = 100;
                    playerScore = 0;
                    // Added to stop the background music upon the player's death; Added 4/24/14
                    MediaPlayer.Stop();                   
                }
            }

            if (gameState == 100)
            {
                EndGame();
                DoPlayerInput();
            }

            audioEngine.Update();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            

            if (gameState == 1)
            {
                DrawTitleScreen();
            }

            if (gameState == 3)
            {
                spriteBatch.Draw(levelOneSplashScreenSprite, levelOneSplashScreenBox, Color.White);
                spriteBatch.End();
            }

            if (gameState == 4)
            {
                levelOneBackground.Draw(spriteBatch);
                spriteBatch.End();
                enemySprites.Draw(gameTime, gameState);
                playerBullets.Draw(gameTime, gameState);
                explosions.Draw(gameTime, gameState);
                powerups.Draw(gameTime, gameState);

                spriteBatch.Begin();
                player.Draw(gameTime, spriteBatch);
                crosshairs.Draw(gameTime, spriteBatch);
                playerLogic.healthBar.Draw(gameTime, spriteBatch);
                spriteBatch.DrawString(scoreFont, playerScore.ToString(), new Vector2(1100, 35), Color.White);
                spriteBatch.End();            
            }

            if (gameState == 5)
            {
                spriteBatch.Draw(levelTwoSplashScreenSprite, levelTwoSplashScreenBox, Color.White);
                spriteBatch.End();
            }

            if (gameState == 6)
            {
                levelTwoBackground.Draw(spriteBatch);
                spriteBatch.End();
                enemySprites.Draw(gameTime, gameState);
                playerBullets.Draw(gameTime, gameState);
                explosions.Draw(gameTime, gameState);
                powerups.Draw(gameTime, gameState);

                spriteBatch.Begin();
                player.Draw(gameTime, spriteBatch);
                crosshairs.Draw(gameTime, spriteBatch);
                playerLogic.healthBar.Draw(gameTime, spriteBatch);
                spriteBatch.DrawString(scoreFont, playerScore.ToString(), new Vector2(1100, 35), Color.White);
                spriteBatch.End();
            }

            if (gameState == 7)
            {
                spriteBatch.Draw(levelBossSplashScreenSprite, levelBossSplashScreenBox, Color.White);
                spriteBatch.End();
            }

            if (gameState == 8)
            {
                spriteBatch.Draw(levelBossBackground, levelBossBGRectangle, Color.White);
                gameBoss.Draw(gameTime, spriteBatch, player);
                spriteBatch.Begin();
                player.Draw(gameTime, spriteBatch);
                crosshairs.Draw(gameTime, spriteBatch);
                playerLogic.healthBar.Draw(gameTime, spriteBatch);
                spriteBatch.DrawString(scoreFont, playerScore.ToString(), new Vector2(1100, 35), Color.White);
                spriteBatch.End();
                playerBullets.Draw(gameTime, gameState);
                explosions.Draw(gameTime, gameState);
            }

            if (gameState == 9)
            {
                spriteBatch.Draw(endingSplashScreenSprite, endingSplashScreenBox, Color.White);
                spriteBatch.DrawString(scoreFont, playerScore.ToString(), new Vector2(1100, 35), Color.White);
                spriteBatch.End();
            }

            

            // Added to draw the game over screen; Added on 4/21/14
            if (gameState == 100)
            {
                gameOverScreen.Draw(spriteBatch);
                spriteBatch.End();
            }
            
            base.Draw(gameTime);
        }

        public void DoTitleScreen(GameTime gameTime)
        {
            
            if (selectedOption == 3)
            {
                gameState = 2; 
            }

            else
            {
                if (Keyboard.GetState().IsKeyDown(Keys.W))
                {
                    selectedOption = 0;
                    titleScreenSelectorBox.Y = 686;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.S))
                {
                    selectedOption = 1;
                    titleScreenSelectorBox.Y = 739;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Enter) && selectedOption == 0)
                {
                    selectedOption = 3;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Enter) && selectedOption == 1)
                {
                    drawInstructions = true;
                }
            }                      
        }

        //draw the title screen
        public void DrawTitleScreen()
        {
            spriteBatch.Draw(titleScreenSprite, titleScreenBox, Color.White);
            spriteBatch.Draw(yellowRectangle, titleScreenSelectorBox, Color.Yellow * 0.3f);
            

            //show instructions on screen
            if (drawInstructions == true)
            {
                spriteBatch.Draw(instructionsSprite, instructionsBox, Color.White);
            }

            spriteBatch.End();
        }

        //used to shoot bullets
        protected void DoPlayerInput()
        {
            currMouseState = Mouse.GetState();

            //code for shooting the bullets
            if (currMouseState.LeftButton == ButtonState.Released && prevMouseState.LeftButton == ButtonState.Pressed)
            {
                bulletVector = this.GetBulletVector(new Vector2(currMouseState.X, currMouseState.Y), player.position);
                playerBullets.AddSpriteToList(new Bullet(bulletSprite, new Vector2(player.position.X + 45, player.position.Y + 20), new Point(15, 15), 2, new Point(1, 1), new Point(1, 1), bulletVector, 1, 50, 0)); 
            }

            //code for shooting missiles
            if (currMouseState.RightButton == ButtonState.Released && prevMouseState.RightButton == ButtonState.Pressed)
            {
                if (timeSinceMissileLastShot > 2000)
                {
                    playerBullets.AddSpriteToList(new Bullet(missileSprite, new Vector2(player.position.X + 30, player.position.Y + 40), new Point(65, 15), 2, new Point(1, 1), new Point(1, 1), new Vector2(2, 0), 1, 100, 0));
                    timeSinceMissileLastShot = 0;
                }
            }
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            

            prevMouseState = currMouseState;

            

            keyboardPrevState = Keyboard.GetState();


            if (gameState == 100)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                {
                    this.Exit();
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    this.Initialize();
                    gameState = 1;
                    this.ResetElapsedTime();
                }
            }
        }

        private Vector2 GetBulletVector(Vector2 mousePosition, Vector2 playerPosition)
        {
            Vector2 newBulletVector;
            newBulletVector = mousePosition - playerPosition;

            if (newBulletVector != Vector2.Zero)
            {
                newBulletVector.Normalize();
            }

            return newBulletVector;
        }

        private void CheckForSpawning(double levelTimer)
        {
            if ((levelTimer % 4000) < 16 && (levelTimer % 4000 > 0))
            {
                int goldSpawn = rnd.Next(100);

                if (gameState == 4)
                {
                    enemySprites.AddSpriteToList(new AutomatedSprite(enemyFighter1Sprite, new Vector2(1280, 400), new Point(117, 44), 5, new Point(1, 1), new Point(1, 1), new Vector2(-4, -2), 50, 15, 25));
                    enemySprites.AddSpriteToList(new AutomatedSprite(enemyFighter1Sprite, new Vector2(1280, 600), new Point(117, 44), 5, new Point(1, 1), new Point(1, 1), new Vector2(-3, -1), 50, 15, 25));
                    enemySprites.AddSpriteToList(new AutomatedSprite(enemyFighter1Sprite, new Vector2(1280, 200), new Point(117, 44), 5, new Point(1, 1), new Point(1, 1), new Vector2(-5, -2), 50, 15, 25));

                    /* Added on 4/15/14
                    enemySprites.AddSpriteToList(new AutomatedSprite(enemyFighter2Sprite, new Vector2(1080, 400), new Point(63, 38), 5, new Point(1, 1), new Point(1, 1), new Vector2(-4, -2), 100, 40, 50));
                    enemySprites.AddSpriteToList(new AutomatedSprite(enemyFighter2Sprite, new Vector2(670, 100), new Point(63, 38), 5, new Point(1, 1), new Point(1, 1), new Vector2(-6, -1), 100, 40, 50));
                    enemySprites.AddSpriteToList(new AutomatedSprite(enemyFighter2Sprite, new Vector2(1280, 550), new Point(63, 38), 5, new Point(1, 1), new Point(1, 1), new Vector2(-5, -5), 100, 40, 50));*/

                    if (goldSpawn > 75)
                    {
                        Vector2 tempPosition = new Vector2(640, 400);

                        Vector2 tempSpeed = new Vector2(-(rnd.Next(
                        4,
                        8)), 0);

                        powerups.AddSpriteToList(
                            new BouncingSprite(
                                Content.Load<Texture2D>("Images/Gold_Chunk"),
                                tempPosition, new Point(75, 75), 10, new Point(0, 0),
                                new Point(1, 1), tempSpeed, 20, 50, 100));
                    }
                }

                if (gameState == 6)
                {
                    enemySprites.AddSpriteToList(new AutomatedSprite(enemyFighter4Sprite, new Vector2(1280, 700), new Point(186, 35), 5, new Point(1, 1), new Point(1, 1), new Vector2(-2, -0.5f), 150, 25, 100));
                    enemySprites.AddSpriteToList(new AutomatedSprite(enemyFighter4Sprite, new Vector2(1280, 100), new Point(186, 35), 5, new Point(1, 1), new Point(1, 1), new Vector2(-2, 0.5f), 150, 25, 100));
                    enemySprites.AddSpriteToList(new AutomatedSprite(enemyFighter4Sprite, new Vector2(1280, 400), new Point(186, 35), 5, new Point(1, 1), new Point(1, 1), new Vector2(-2, 0), 150, 25,100));
                    enemySprites.AddSpriteToList(new AutomatedSprite(enemyFighter5Sprite, new Vector2(1280, 300), new Point(198, 53), 5, new Point(1, 1), new Point(1, 1), new Vector2(-5, 0), 150, 25, 100));

                    if (goldSpawn > 75)
                    {
                        Vector2 tempPosition = new Vector2(640, 400);

                        Vector2 tempSpeed = new Vector2(-(rnd.Next(
                        4,
                        8)), 0);

                        powerups.AddSpriteToList(
                            new BouncingSprite(
                                Content.Load<Texture2D>("Images/Gold_Chunk"),
                                tempPosition, new Point(75, 75), 10, new Point(0, 0),
                                new Point(1, 1), tempSpeed, 20, 50, 100));
                    }
                }
            }
        }

        // method to handle end of game added on 4/21/14
        public void EndGame()
        {
            if (gameOverMusicPlayed == 0)
            {
                soundBank.PlayCue("smb_mariodie");
                gameOverMusicPlayed = 1;
            }
            
            // Initializes a new Game Over Screen; Added on 4/24/14
            gameOverScreen = new GameOverScreen(this);

            // Calls update method of GameOverScreen
            gameOverScreen.Update();
        }

        public void AddScore(int score)
        {
            playerScore += score;
        }
    }
}
