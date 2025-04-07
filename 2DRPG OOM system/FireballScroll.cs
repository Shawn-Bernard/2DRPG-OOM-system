using _2DRPG_OOM_system;
using Microsoft.Xna.Framework;



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
                    // The player enters into aiming move, which allows the player to choose the direction the fire ball will take
                    ((Player)Game1.characters[0]).aimingMode = true;  
                    ((Player)Game1.characters[0]).feedback = "Aim";   // this tells to the player that is in aiming mode
                }
            }

            isUsed = true;
        }
    }


}

