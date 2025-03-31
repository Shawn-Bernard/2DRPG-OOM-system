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
        iColor = Color.White;
    }

    public override void itemEffect() 
    {
        if (!isUsed) 
        {
            if (Game1.characters[0] is Player)
            {
                // only creates a fire ball if there is no fireball. If it's already exists, the player should be able to make one
                if (((Player)Game1.characters[0]).fireBall == null)
                {
                    ((Player)Game1.characters[0]).fireBall = new FireBall(new Vector2(Game1.characters[0].tilemap_PosX + Game1.characters[0].facingDir.X,
                                                                     Game1.characters[0].tilemap_PosY + Game1.characters[0].facingDir.Y),
                                                                     Game1.characters[0].facingDir, 5, Color.Red);

                    ((Player)Game1.characters[0]).fireBall.isFromPlayer = true; 
                    ((Player)Game1.characters[0]).shot = true; 
                }
            }

            isUsed = true;
        }
    }


}

