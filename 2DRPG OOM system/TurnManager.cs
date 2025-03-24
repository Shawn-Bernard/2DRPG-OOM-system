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
        

    public void UpdateTurnManager(GameTime gameTime, List<Actor> _characters) 
    {
        if (characterTurn < _characters.Count) 
        {
            Actor turnCharacter = _characters[characterTurn];            
            _characters[characterTurn].TurnUpdate(gameTime); 
        }
    }



}

