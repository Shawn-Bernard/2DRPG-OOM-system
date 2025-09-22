using _2DRPG_OOM_system;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Ghost : Enemy
{
    public Ghost(int iPosX, int iPosY) 
    {
        enemyType = EnemyType.Ghost;
        _healthSystem.health = 7;
        _healthSystem.power = 3;
        _healthSystem.shield = 0;
        _healthSystem.life = 1;
        tilemap_PosX = iPosX;
        tilemap_PosY = iPosY;
        _healthSystem.setMaxHP(7);
        _healthSystem.setMaxShield(3);
        _healthSystem.isStunned = false;
        active = true;
        turn = true;
        ismyTurn = false; 
        _healthSystem.status = "Normal";
        cropPositionX = 1;
        cropPositionY = 10;
        AColor = Color.White; 
    }

    int mvX;
    int mvY;

    public override void TurnUpdate(GameTime gameTime)
    {
        // visualization if the ghost has been damaged
        if (isDamage)
        {
            damageTiming(0.25f, gameTime);
        }


        if (turn && !hasMoved)
        {
            // Checks if the enemy is still alive or is not stunned
            if (active && !_healthSystem.isStunned)
            {
                ismyTurn = true; 
                AColor = Color.White;

                // this checks for the player position, based on that this will tell if the ghost should go right or left
                if (Game1.characters[0].tilemap_PosX - tilemap_PosX > 0)
                    mvX = 1;
                else if (Game1.characters[0].tilemap_PosX - tilemap_PosX < 0)
                    mvX = -1;
                else
                    mvX = 0;

                // this checks for the player position, based on that this will tell if the ghost should go up or down
                if (Game1.characters[0].tilemap_PosY - tilemap_PosY > 0)
                    mvY = 1;
                else if (Game1.characters[0].tilemap_PosY - tilemap_PosY < 0)
                    mvY = -1;
                else
                    mvY = 0;

                // This disable the ghost for moving diagonally 
                if (Math.Abs(mvX) == 1 && Math.Abs(mvY) == 1)
                    mvX = 0;

                // Check if its going to collide with the player, so inflict damage instead of moving
                for (int i = 0; i < Game1.characters.Count; i++)
                {
                    if (CheckForObjCollision(tilemap_PosX + mvX, tilemap_PosY + mvY, Game1.characters[i].tilemap_PosX, Game1.characters[i].tilemap_PosY) && Game1.characters[i] is Player)
                    {
                        Game1.characters[0]._healthSystem.TakeDamage(_healthSystem.power);
                        Game1.characters[0].damageVisualization(_healthSystem.power); 
                        mvX = 0;
                        mvY = 0;
                    }
                }

                Movement(mvX, mvY);  // it moves
                FinishTurn();       
            }
            else if (_healthSystem.isStunned)
            {
                //When the enemy is stunned, enemy's turn is skipped and make the enemy unstunned
                AColor = Color.Yellow;
                _healthSystem.makeUnstunned();
                FinishTurn();

            }
        }

        if (waitingPhase)
        {
            waitingTurnToFinish(1f, gameTime);
        }

    }
}

