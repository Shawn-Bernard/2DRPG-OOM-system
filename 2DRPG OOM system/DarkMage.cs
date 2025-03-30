using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2DRPG_OOM_system;
using Microsoft.Xna.Framework;


public class DarkMage : Enemy
{
    public DarkMage(int hp, int atk, int shld, int iPosX, int iPosY) 
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
        _healthSystem.status = "Normal";
        cropPositionX = 3;
        cropPositionY = 9;
        AColor = Color.White;
    }

    int mvX;
    int mvY;

    public override void TurnUpdate(GameTime gameTime)
    {


        if (turn && !hasMoved)
        {
            // Checks if the enemy is still alive
            if (active && !_healthSystem.isStunned)
            {
                AColor = Color.White;

                if (Game1.characters[0].tilemap_PosX - tilemap_PosX > 0)
                    mvX = 1;
                else if (Game1.characters[0].tilemap_PosX - tilemap_PosX < 0)
                    mvX = -1;
                else
                    mvX = 0;


                if (Game1.characters[0].tilemap_PosY - tilemap_PosY > 0)
                    mvY = 1;
                else if (Game1.characters[0].tilemap_PosY - tilemap_PosY < 0)
                    mvY = -1;
                else
                    mvY = 0;

                if (Math.Abs(mvX) == 1 && Math.Abs(mvY) == 1) 
                {
                    if (checkingForCollision(Game1.tileMap, '#', this, mvX, 0) || checkingForCollision(Game1.tileMap, '$', this, mvX, 0))
                    {
                        mvX = 0;
                    }
                    else if (checkingForCollision(Game1.tileMap, '#', this, 0, mvY) || checkingForCollision(Game1.tileMap, '$', this, 0, mvY))
                    {
                        mvY = 0;
                    }
                    else 
                    {
                        mvX = 0; 
                    }
                }
                    


                if (CheckForObjCollision(tilemap_PosX + mvX, tilemap_PosY + mvY, Game1.characters[0].tilemap_PosX, Game1.characters[0].tilemap_PosY) && Game1.characters[0] is Player)
                {
                    Game1.characters[0]._healthSystem.TakeDamage(_healthSystem.power);
                    mvX = 0;
                    mvY = 0;
                }

                if(checkingForCollision(Game1.tileMap, '#', this, mvX, mvY) || checkingForCollision(Game1.tileMap, '$', this, mvX, mvY)) 
                {
                    mvX = 0;
                    mvY = 0; 
                }

                Movement(mvX, mvY);
                FinishTurn();
            }
            else if (_healthSystem.isStunned)
            {
                //When the enemy is stunned, enemy's turn is skipped and make the enemy unstunned
                AColor = Color.Yellow;
                _healthSystem.makeUnstunned();
                FinishTurn();

            }
            Game1.projectiles.Add(new FireBall(new Vector2(tilemap_PosX + mvX, tilemap_PosY + mvY), new Vector2(mvX, mvY), Color.Purple));
        }

        if (waitingPhase)
        {
            waitingTurnToFinish(1f, gameTime);
        }

    }

}

