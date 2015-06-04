/*
 * Just For Fun
 * Andersen, Corey; Gan, Jie; Olson, Darwin; Zhang, Shujian
 * CSCI 313   Section #1
 * 5/9/2014
*/

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

namespace MechWing_Defense_System.Classes
{
    class ScrollingBackground
    {
       
        private Texture2D bgTexture;
        private Rectangle bgRec1;
        private Rectangle bgRec2;
        private int texWidth;
        private int texHeight;

        public ScrollingBackground()
        {
        }

        public void Load(GraphicsDevice device, Texture2D newBGTexture)
        {
            bgTexture = newBGTexture;
            texWidth = bgTexture.Width;
            texHeight = bgTexture.Height;
            bgRec1 = new Rectangle(0, 0, texWidth, texHeight);
            bgRec2 = new Rectangle(texWidth, 0, texWidth, texHeight);
            
        }

        public void Update(int deltaX)
        {
            if (bgRec1.X + texWidth <= 0)
            {
                bgRec1.X = bgRec2.X + texWidth;
            }
            if (bgRec2.X + texWidth <= 0)
            {
                bgRec2.X = bgRec1.X + texWidth;
            }

            bgRec1.X -= deltaX;
            bgRec2.X -= deltaX;

                      
        }

        public void Draw(SpriteBatch batch)
        {
            
            batch.Draw(bgTexture, bgRec1, Color.White);
            batch.Draw(bgTexture, bgRec2, Color.White);
            
        }

    }
}
