using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Clockwork.Classes.LogicalClasses
{
    public class Inventory
    {
        int maxInventorySlots;
        int currentUsedSlots;
        int currentFreeSlots;

        public List<Item> currentInventory;

        public Weapon currentWeapon;

        public bool inventoryOpen;

        public bool notEnoughRoomMessage;

        public Inventory()
        {
            maxInventorySlots = 10;
            currentUsedSlots = 0;
            currentFreeSlots = 10;

            currentInventory = new List<Item>();

            currentWeapon = new Weapon();
            inventoryOpen = false;
            notEnoughRoomMessage = false;
        }

        public bool PickUpItem(Item newPickedUpItem)
        {
            //check the space requirements and see if there's room for it before adding it
            if (newPickedUpItem.slotsTaken <= currentFreeSlots)
            {
                currentInventory.Add(newPickedUpItem);
                currentFreeSlots -= newPickedUpItem.slotsTaken;
                currentUsedSlots += newPickedUpItem.slotsTaken;
                return true;
            }
            else 
            {
                notEnoughRoomMessage = true;
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
            if(inventoryOpen)
            {
                //start at the first one
                int selectedItemKey = 1;

                KeyboardState keystate = Keyboard.GetState();

                //check all the buttons to move to other items or use one
                if(keystate.IsKeyDown(Keys.W))
                {
                    if(selectedItemKey > 1)
                    {
                        selectedItemKey -= 1;
                    }
                }
                if(keystate.IsKeyDown(Keys.S))
                {
                    if(selectedItemKey < maxInventorySlots)
                    {
                        selectedItemKey += 1;
                    }
                }
                //"use" the item...or equip it.  i'll have to figure out how to "do" that later here.
                if(keystate.IsKeyDown(Keys.K))
                {
                    currentInventory.ElementAt(selectedItemKey).Use();
                }

                //close inventory here
                if(keystate.IsKeyDown(Keys.Enter))
                {
                    inventoryOpen = false;
                }
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
