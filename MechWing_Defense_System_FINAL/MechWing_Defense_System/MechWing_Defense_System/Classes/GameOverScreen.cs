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
using MechWing_Defense_System.Classes.SpriteClasses;
using MechWing_Defense_System.Classes;

namespace MechWing_Defense_System
{
    class GameOverScreen
    {
        private Texture2D texture;
        private Game1 game;
        private KeyboardState lastState;

        Rectangle gameOverScreenBox = new Rectangle(0, 0, 1280, 800);

        public GameOverScreen(Game1 game)
        {
            this.game = game;
            texture = game.Content.Load<Texture2D>("Images/GameOverScreen");
            lastState = Keyboard.GetState();
        }

        public void Update()
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Enter) && lastState.IsKeyUp(Keys.Enter))
            {
                game.DrawTitleScreen();
            }
            else if (keyboardState.IsKeyDown(Keys.Escape) && lastState.IsKeyUp(Keys.Escape))
            {
                game.Exit();
            }

            lastState = keyboardState;
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            if (texture != null)
                spriteBatch.Draw(texture, gameOverScreenBox, Color.White);
        }
    }
}
