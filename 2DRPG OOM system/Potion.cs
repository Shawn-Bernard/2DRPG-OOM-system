using _2DRPG_OOM_system;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Potion : Item
{
    public Potion(Vector2 itemPos) 
    {
        cropPosX = 8;
        cropPosY = 9;
        name = "Potion";
        description = "The potion heals the player 5 hp";
        isPickUp = false;
        isUsed = false;
        itemPosition = itemPos; 
    }

    public override void itemEffect()
    {
        if (!isUsed)
        {
            // the player recovers 5 hp
            if (Game1.characters[0] is Player)
                Game1.characters[0]._healthSystem.RecoverHealth(5);

            isUsed = true;
        }
    }

}

