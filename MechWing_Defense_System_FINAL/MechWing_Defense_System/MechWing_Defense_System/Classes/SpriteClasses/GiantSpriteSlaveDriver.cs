using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MechWing_Defense_System.Classes.SpriteClasses
{
    class GiantSpriteSlaveDriver
    {
        public SpriteManager playerSprites;
        public SpriteManager enemySprites;
        public SpriteManager playerBulletSprites;
        public SpriteManager enemyBulletSprites;
        public SpriteManager selfTerminatingSprites;

        public GiantSpriteSlaveDriver()
        {
        }

        public GiantSpriteSlaveDriver(SpriteManager players, SpriteManager enemies, SpriteManager ggBullets, SpriteManager bgBullets, SpriteManager boomThings)
        {
            playerSprites = players;
            enemySprites = enemies;
            playerBulletSprites = ggBullets;
            enemyBulletSprites = bgBullets;
            selfTerminatingSprites = boomThings;
        }




    }
}
