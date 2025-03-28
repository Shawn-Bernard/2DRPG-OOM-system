using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class FireballScroll : Item
{
    public FireballScroll(Vector2 itemPos) 
    {
        cropPosX = 7;
        cropPosY = 10;
        name = "Scroll of Fireball";
        description = "Throw a fire ball, deals 5 damage if hits an enemy";
        isPickUp = false;
        isUsed = false;
        itemPosition = itemPos;
    }

    public override void itemEffect() 
    {
        if (!isUsed) 
        {

        }
    }


}

