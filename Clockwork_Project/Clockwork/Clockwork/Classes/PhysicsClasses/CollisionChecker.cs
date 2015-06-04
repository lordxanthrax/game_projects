using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Clockwork.Classes.SpriteClasses;
using Clockwork.Classes.StaticItemClasses;
using Microsoft.Xna.Framework;
using Clockwork.Classes.LogicalClasses;

namespace Clockwork.Classes.PhysicsClasses
{
    public class CollisionChecker
    {
   

        public static void CheckCollisions(Player playerOne, StaticItemMapList staticItems)
        {
            foreach (StaticItemMap sim in staticItems.staticItemMapList)
            {
                foreach (StaticItem si in sim.mapItems)
                {
                    if (si.impassible)
                    {
                        if (si.collisionRect.Intersects(playerOne.playerSprite.collisionRect))
                        {
                            playerOne.playerSprite.position = playerOne.oldPosition;
                        }
                    }
                }
            }
                        

        }
    }
}
