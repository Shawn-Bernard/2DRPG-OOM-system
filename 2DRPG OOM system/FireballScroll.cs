using _2DRPG_OOM_system;
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
            Game1.projectiles.Add(new FireBall(new Vector2(Game1.characters[0].tilemap_PosX + Game1.characters[0].facingDir.X,
                                                             Game1.characters[0].tilemap_PosY + Game1.characters[0].facingDir.Y),
                                                             Game1.characters[0].facingDir, Color.Red));
                                                             

            isUsed = true;
        }
    }


}

