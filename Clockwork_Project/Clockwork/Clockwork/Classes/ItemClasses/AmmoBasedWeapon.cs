using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Clockwork.Classes.LogicalClasses
{
    public class AmmoBasedWeapon : Weapon
    {
        int maxAmmo;
        int currentAmmo;
        public int weaponProjectileSpeed;

        public Texture2D bulletTexture;

        public AmmoBasedWeapon() : base()
        {
            maxAmmo = 0;
            currentAmmo = 0;
            weaponProjectileSpeed = 0;
            bulletTexture = null;
        }

        public AmmoBasedWeapon(String newItemName, int newSlotsTaken, Texture2D newInventoryImage, Texture2D newWeaponSpriteSheet, int newWeaponDamage,
            int newCooldownTime, Point newCurrentFrame, Point newFrameSize, Point newSheetSize,
            int newMaxAmmo, int newCurrentAmmo, int newWeaponProjectileSpeed, Texture2D newBulletTexture)
            : base(newItemName, newSlotsTaken, newInventoryImage, newWeaponSpriteSheet, newWeaponDamage, newCooldownTime,
            newCurrentFrame, newFrameSize, newSheetSize)
        {
            maxAmmo = newMaxAmmo;
            currentAmmo = newCurrentAmmo;
            weaponProjectileSpeed = newWeaponProjectileSpeed;
            bulletTexture = newBulletTexture;
        }

        public override bool Shoot()
        {
            if (currentAmmo > 0)
            {
                animationPlaying = true;
                currentAmmo -= 1;
                //play a BANG BANG BOOM! sound.  lol.
                return true;
            }
            else
            {
                //play an out of ammo sound.
                return false;
            }
        }
    }
}
