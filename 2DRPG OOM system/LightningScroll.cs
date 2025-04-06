using _2DRPG_OOM_system;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class LightningScroll : Item
{
    public LightningScroll(Vector2 itemPos) 
    {
        cropPosX = 10;
        cropPosY = 10;
        name = "Scroll of Lightning";
        description = "Throw a lightning, deals 2 damage to all active enemies";
        isPickUp = false;
        isUsed = false;
        itemPosition = itemPos;
        iColor = Color.Yellow;
    }

    public override void itemEffect() 
    {
        if (!isUsed)
        {
            for (int i = 1; i < Game1.characters.Count; i++)
            {
                if (Game1.characters[i] is Enemy)
                {
                    Game1.characters[i]._healthSystem.TakeDamage(3);
                    Game1.characters[i].damageVisualization(3); 
                }
            }
            isUsed = true;

            
        }
    }
}

