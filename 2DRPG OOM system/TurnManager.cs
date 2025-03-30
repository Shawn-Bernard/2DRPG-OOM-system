using _2DRPG_OOM_system;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class TurnManager
{
    public int characterTurn = 0;
        

    public void UpdateTurnManager(GameTime gameTime) 
    {
        
        if (characterTurn < Game1.characters.Count) 
        {
            Actor turnCharacter = Game1.characters[characterTurn];    
            
            if(turnCharacter.turn)
            {
                turnCharacter.TurnUpdate(gameTime); 
            }
            else
            {
                characterTurn++; 
            }
            
        }
        else 
        {            
            characterTurn = 0;
            for (int i = 0; i < Game1.characters.Count; i++)
            {
                Game1.characters[i].turn = true;
                if (Game1.characters[i]._healthSystem.invincibility)
                    Game1.characters[i]._healthSystem.invincibility = false;
            }
        }
    }



}

