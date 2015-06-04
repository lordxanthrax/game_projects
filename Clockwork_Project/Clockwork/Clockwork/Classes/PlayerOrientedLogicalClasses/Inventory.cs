using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Clockwork.Classes.LogicalClasses
{
    public class Inventory
    {
        int maxInventorySlots;
        int currentUsedSlots;
        int currentFreeSlots;

        public List<Item> currentInventory;

        public Weapon currentWeapon;

        bool inventoryOpen;

        public Inventory()
        {
            maxInventorySlots = 10;
            currentUsedSlots = 0;
            currentFreeSlots = 10;

            currentInventory = new List<Item>();

            currentWeapon = new Weapon();
        }

        public bool PickUpItem(Item newPickedUpItem)
        {
            //check the space requirements and see if there's room for it before adding it
            if (newPickedUpItem.slotsTaken <= currentFreeSlots)
            {
                currentInventory.Add(newPickedUpItem);
                currentFreeSlots -= newPickedUpItem.slotsTaken;

                return true;
            }
            else 
            { 
                return false; 
            }
        }

        public void OpenInventory()
        {
            inventoryOpen = true;
        }

        public void CloseInventory()
        {
            inventoryOpen = false;
        }

        public void Update(GameTime gameTime, Rectangle clientBounds, int facing)
        {
            if (currentWeapon != null)
            {
                currentWeapon.Update(gameTime, clientBounds, facing);
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (inventoryOpen)
            {

            }
        }
    }
}
