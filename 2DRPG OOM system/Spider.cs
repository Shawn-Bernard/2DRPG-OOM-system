using _2DRPG_OOM_system;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Spider : Enemy
{
    public Spider(int hp, int atk, int shld, int iPosX, int iPosY) 
    {
        _healthSystem.health = hp;
        _healthSystem.power = atk;
        _healthSystem.shield = shld;
        _healthSystem.life = 1;
        tilemap_PosX = iPosX;
        tilemap_PosY = iPosY;
        _healthSystem.setMaxHP(hp);
        _healthSystem.setMaxShield(shld);
        _healthSystem.isStunned = false;
        active = true;
        turn = true;
        ismyTurn = false; 
        _healthSystem.status = "Normal";
        cropPositionX = 2;
        cropPositionY = 10;
        AColor = Color.White; 
    }

    public PathNode nextMove = new PathNode();  
    public Pathfinder _pathfinder = new Pathfinder();

    public override void TurnUpdate(GameTime gameTime)
    {
        eMovement = TypeOfMovement();

        
        if (_healthSystem.isStunned)
            AColor = Color.Yellow;
        else
            AColor = Color.White; 


        if (turn && !hasMoved)
        {
            // Checks if the enemy is still alive
            if (active && !_healthSystem.isStunned)
            {
                ismyTurn = true; 
                if (CheckForObjCollision(tilemap_PosX + nextMove.X, tilemap_PosY + nextMove.Y, Game1.characters[0].tilemap_PosX, Game1.characters[0].tilemap_PosY))
                {
                    Game1.characters[0]._healthSystem.TakeDamage(_healthSystem.power);
                    Game1.characters[0].damageVisualization(); 
                    
                }
                else 
                {
                    Movement((int)moveDir.X, (int)moveDir.Y); 
                }

                FinishTurn();

            }
            else if (_healthSystem.isStunned)
            {
                //When the enemy is stunned, enemy's turn is skipped and make the enemy unstunned
                _healthSystem.makeUnstunned();
               FinishTurn();

            }
        }

        if (waitingPhase)
        {
            waitingTurnToFinish(2f, gameTime);
        }

    }
}

