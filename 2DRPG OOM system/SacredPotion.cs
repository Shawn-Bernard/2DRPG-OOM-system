using _2DRPG_OOM_system;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class SacredPotion : Item
{
    public SacredPotion(Vector2 itemPos) 
    {
        cropPosX = 5;
        cropPosY = 9;
        name = "Sacred Potion";
        description = "The player recover fully hp and receive 0 damage for 1 turn";
        isPickUp = false;
        isUsed = false;
        itemPosition = itemPos;
    }

    public override void itemEffect()
    {
        if (!isUsed)
        {
            // the player recovers all its health and become invincible for the rest of the turn
            if (Game1.characters[0] is Player)
            {
                Game1.characters[0]._healthSystem.invincibility = true;
                Game1.characters[0]._healthSystem.RecoverHealth(Game1.characters[0]._healthSystem.maxHealth); 
            }

            isUsed = true;
        }
    }

}

