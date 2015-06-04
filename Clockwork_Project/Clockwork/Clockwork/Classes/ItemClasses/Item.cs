using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Clockwork.Classes.LogicalClasses
{
    public class Item
    {
        public String itemName;
        public int slotsTaken;

        public Texture2D inventoryImage;

        public Item()
        {
            itemName = "Default Item;  Don't try to use it.";
            slotsTaken = 1;
            inventoryImage = null;
        }

        public Item(String newItemName, int newSlotsTaken, Texture2D newInventoryImage)
        {
            itemName = newItemName;
            slotsTaken = newSlotsTaken;
            inventoryImage = newInventoryImage;
        }

        public void Update()
        {
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
        }
    }
}
