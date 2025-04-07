using _2DRPG_OOM_system;
using Microsoft.Xna.Framework;


public class TurnManager
{
    public int characterTurn = 0;
        

    public void UpdateTurnManager(GameTime gameTime) 
    {
        
        if (characterTurn < Game1.characters.Count) 
        {
            Actor turnCharacter = Game1.characters[characterTurn];    // Take the actor who has the current turn
            
            if(turnCharacter.turn)
            {
                turnCharacter.TurnUpdate(gameTime); 
            }
            else
            {
                characterTurn++;     // When the actor finishes its turn, the turn goes to the next actor 
                for (int i = 0; i < Game1.characters.Count; i++)
                    Game1.characters[i].feedback = "";   // When a turn ends, all feedback return to default values
            }
            
        }
        else 
        {            
            characterTurn = 0;
            for (int i = 0; i < Game1.characters.Count; i++)
            {
                Game1.characters[i].turn = true;
                if (Game1.characters[i]._healthSystem.invincibility)
                    Game1.characters[i]._healthSystem.invincibility = false;  // In case an actor is in invincibility mode, it turn off since only last 1 turn
            }
        }
    }

    public void resetTurns() 
    {
        // Set turn to all actor to false

        for(int i = 0; i < Game1.characters.Count; i++) 
        {
            Game1.characters[i].turn = false;
        }
    }

}

