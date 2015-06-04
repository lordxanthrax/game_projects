using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Clockwork.Classes.MapClasses
{
    class Map
    {
        int mapHeight;
        int mapWidth;
        MapBlock[,] mapBlockArray;

        public Map(int newMapHeight, int newMapWidth)
        {
            mapHeight = newMapHeight;
            mapWidth = newMapWidth;
            mapBlockArray = new MapBlock[mapHeight, mapWidth];
        }

        public void CreateMap(List<MapBlock> tempBlockArray)
        {
            int i = 0;
            int j = 0;
            foreach (MapBlock tempBlock in tempBlockArray)
            {
                mapBlockArray[i, j] = tempBlock;
                i++;

                if (i == mapHeight)
                {
                    i = 0;
                    j++;
                }
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            for (int i = 0; i < mapHeight; i++)
            {
                for (int j = 0; j < mapWidth; j++)
                {
                    mapBlockArray[i, j].Draw(gameTime, spriteBatch);
                }
            }


        }
    }
}
