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
        turn = false;
        _healthSystem.status = "Normal";
        cropPositionX = 2;
        cropPositionY = 10;
    }

    public override void TurnUpdate(GameTime gameTime)
    {
        if (turn)
        {
            // Checks if the enemy is still alive
            if (active && !_healthSystem.isStunned)
            {
                _healthSystem.defaultStatus();
                eMovement = TypeOfMovement(); // Get a random movement base on the AI method

                switch (eMovement)
                {
                    //Base on the direction, the enemy will move in the corresponding direction
                    case "Right":
                        moveDir = new Vector2(1, 0);
                        break;
                    case "Left":
                        moveDir = new Vector2(-1, 0);
                        break;
                    case "Up":
                        moveDir = new Vector2(0, -1);
                        break;
                    case "Down":
                        moveDir = new Vector2(0, 1);
                        break;
                    default:
                        { // do nothing
                            moveDir = new Vector2(0, 0);
                            break;
                        }
                }

                if (checkingForCollision(Game1.tileMap, '#', this, (int)moveDir.X, (int)moveDir.Y) || checkingForCollision(Game1.tileMap, '$', this, (int)moveDir.X, (int)moveDir.Y))
                {
                    moveDir = new Vector2(0, 0);
                }
                else
                {
                    if (CheckForObjCollision(tilemap_PosX + (int)moveDir.X, tilemap_PosY + (int)moveDir.Y, Game1.characters[0].tilemap_PosX, Game1.characters[0].tilemap_PosY))
                    {
                        Game1.characters[0]._healthSystem.TakeDamage(_healthSystem.power);
                        moveDir = new Vector2(0, 0);
                    }

                    Movement((int)moveDir.X, (int)moveDir.Y);
                    turn = false;
                }

            }
            else if (_healthSystem.isStunned)
            {
                //When the enemy is stunned, enemy's turn is skipped and make the enemy unstunned
                _healthSystem.makeUnstunned();
                turn = false;

            }
        }


    }
}

