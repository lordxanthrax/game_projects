using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Clockwork.Classes.LogicalClasses;
using Microsoft.Xna.Framework.Graphics;

namespace Clockwork.Classes.PlayerOrientedLogicalClasses
{
    class Flashlight : Item
    {
        int currentFacing;
        bool flashlightOn;

        Battery flashlightBattery;

        public Flashlight(String newItemName, int newSlotsTaken, Texture2D newInventoryImage, int newFlashlightCharge)
            : base(newItemName, newSlotsTaken, newInventoryImage)
        {
        }

        public void TurnOnFlashlight()
        {
            if (flashlightBattery.GetCharge() > 0)
            {
                flashlightOn = true;
            }
        }

        public void Update()
        {
            if(flashlightOn)
            {
                flashlightBattery.Drain();
            }

            base.Update();
        }
    }
}
