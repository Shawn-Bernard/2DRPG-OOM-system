using _2DRPG_OOM_system;
using Microsoft.Xna.Framework;
using System;



public class Boss : Enemy
{
    public Boss (int iPosX, int iPosY) 
    {
        _healthSystem.health = 20;
        _healthSystem.power = 3;
        _healthSystem.shield = 0;
        _healthSystem.life = 1;
        tilemap_PosX = iPosX;
        tilemap_PosY = iPosY;
        _healthSystem.setMaxHP(20);
        _healthSystem.setMaxShield(0);
        _healthSystem.isStunned = false;
        active = true;
        turn = true;
        ismyTurn = false;
        _healthSystem.status = "Normal";
        cropPositionX = 1;
        cropPositionY = 9;
        AColor = Color.White;
        shot = false;

    }

    private int mvX;
    private int mvY;
    private bool shot;
    public Projectile VenomBreath = null;



    public override void TurnUpdate(GameTime gameTime)
    {
        if (active && !_healthSystem.isStunned) 
        { 
            if (turn && !hasMoved)
            {
            // Checks if the enemy is still alive
                if (active && !_healthSystem.isStunned && !shot)
                {
                ismyTurn = true;
                AColor = Color.White;
                mvX = 0;
                mvY = 0;

                ChooseAMove();

                }
            }
        }
        else if (_healthSystem.isStunned)
        {
            //When the enemy is stunned, enemy's turn is skipped and make the enemy unstunned
            AColor = Color.Yellow;
            _healthSystem.makeUnstunned();
            FinishTurn();

        }

        if (shot)
        {
            if (VenomBreath.hit)
            {
                // waits for the ball to collide to finish the turn
                shot = false;
                VenomBreath = null;
                FinishTurn();
            }
        }

        if (waitingPhase)
        {
            waitingTurnToFinish(1f, gameTime);
        }

    }

    public void ChooseAMove() 
    {
        Random rndNum = new Random();
        int tempNum = rndNum.Next(1, 5);

        // based on random, the boos will take one of these four actions

        switch (tempNum) 
        {
            case 1:
                FinishTurn(); // Do Nothing
                feedback = "Nothing";
                break;
            case 2:
                NormalMovement();
                feedback = "Normal"; 
                FinishTurn(); 
                break;
            case 3:
                shootAtPlayer();
                feedback = "Shoot"; 
                break;
            case 4:
                moveThreeTiles();
                feedback = "3Mv";
                break;
            default:
                FinishTurn();
                feedback = "Nthg"; 
                //Do Nothinkg
                break; 


        }

    }

    public void NormalMovement() 
    {
        // this checks for the player position, based on that this will tell if the dark mage should go right or left
        if (Game1.characters[0].tilemap_PosX - tilemap_PosX > 0)
            mvX = 1;
        else if (Game1.characters[0].tilemap_PosX - tilemap_PosX < 0)
            mvX = -1;

        // this checks for the player position, based on that this will tell if the Dark mage should go up or down
        if (Game1.characters[0].tilemap_PosY - tilemap_PosY > 0)
            mvY = 1;
        else if (Game1.characters[0].tilemap_PosY - tilemap_PosY < 0)
            mvY = -1;

        // This disable the Dark Mage for moving diagonally 
        if (Math.Abs(mvX) == 1 && Math.Abs(mvY) == 1)
        {
            // Check if is going to collide with a wall in each direaction
            if (CheckForUnWalkable(mvX, 0))
            {
                mvX = 0;
            }
            else if (CheckForUnWalkable(0, mvY))
            {
                mvY = 0;
            }
            else
            {
                mvX = 0;
            }
        }

        if (CheckForUnWalkable(mvX, mvY))
        {
            mvX = 0;
            mvY = 0;
        }

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

        Movement(mvX, mvY);
        

    }

    public void shootAtPlayer()
    {
        if (Game1.characters[0].tilemap_PosY - tilemap_PosY > 0)
        {
            // to the dark mage to shot, check if he hasn't shot already in the current turn and if there is no miasma ball already 
            if (!shot && VenomBreath == null)
            {
                ShootVenomBall(0, 1); 
            }

        }
        else if (Game1.characters[0].tilemap_PosY - tilemap_PosY < 0)
        {
            // to the dark mage to shot, check if he hasn't shot already in the current turn and if there is no miasma ball already 
            if (!shot && VenomBreath == null)
            {
                ShootVenomBall(0, -1); 
            }
        }

        if (Game1.characters[0].tilemap_PosX - tilemap_PosX > 0)
        {
            // to the dark mage to shot, check if he hasn't shot already in the current turn and if there is no miasma ball already 
            if (!shot && VenomBreath == null)
            {
                ShootVenomBall(1, 0); 
            }
        }
        else if (Game1.characters[0].tilemap_PosX - tilemap_PosX < 0)
        {
            // to the dark mage to shot, check if he hasn't shot already in the current turn and if there is no miasma ball already 
            if (!shot && VenomBreath == null)
            {
                ShootVenomBall(-1, 0);                 
            }
        }


        
    }

    public void moveThreeTiles() 
    {
        for(int i = 0; i < 3; i++) 
        {
            NormalMovement(); 
        }

        FinishTurn(); 
    }

    private void ShootVenomBall(int dx, int dy) 
    {
        VenomBreath = new FireBall(new Vector2(tilemap_PosX, tilemap_PosY), new Vector2(dx, dy), 5, Color.DarkOliveGreen);
        VenomBreath.isFromPlayer = false;
        mvX = 0;
        mvX = 0;
        shot = true;
    }

}

