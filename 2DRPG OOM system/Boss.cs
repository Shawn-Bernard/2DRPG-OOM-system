using _2DRPG_OOM_system;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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

        switch (tempNum) 
        {
            case 1:
                FinishTurn(); // Do Nothing
                break;
            case 2:
                NormalMovement();
                FinishTurn(); 
                break;
            case 3:
                shootAtPlayer();
                break;
            case 4:
                moveThreeTiles();
                break;
            default:
                FinishTurn(); 
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

        if (checkingForCollision(Game1.tileMap, '#', this, mvX, mvY) || checkingForCollision(Game1.tileMap, '$', this, mvX, mvY))
        {
            mvX = 0;
            mvY = 0;
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
                VenomBreath = new FireBall(new Vector2(tilemap_PosX, tilemap_PosY), new Vector2(0, 1), 5, Color.DarkOliveGreen);
                VenomBreath.isFromPlayer = false;
                mvX = 0;
                mvY = 0;
                shot = true;
            }

        }
        else if (Game1.characters[0].tilemap_PosY - tilemap_PosY < 0)
        {
            // to the dark mage to shot, check if he hasn't shot already in the current turn and if there is no miasma ball already 
            if (!shot && VenomBreath == null)
            {
                VenomBreath = new FireBall(new Vector2(tilemap_PosX, tilemap_PosY), new Vector2(0, -1), 5, Color.DarkOliveGreen);
                VenomBreath.isFromPlayer = false;
                mvX = 0;
                mvY = 0;
                shot = true;
            }
        }

        if (Game1.characters[0].tilemap_PosX - tilemap_PosX > 0)
        {
            // to the dark mage to shot, check if he hasn't shot already in the current turn and if there is no miasma ball already 
            if (!shot && VenomBreath == null)
            {
                VenomBreath = new FireBall(new Vector2(tilemap_PosX, tilemap_PosY), new Vector2(1, 0), 5, Color.DarkOliveGreen);
                VenomBreath.isFromPlayer = false;
                mvX = 0;
                mvY = 0;
                shot = true;
            }
        }
        else if (Game1.characters[0].tilemap_PosX - tilemap_PosX < 0)
        {
            // to the dark mage to shot, check if he hasn't shot already in the current turn and if there is no miasma ball already 
            if (!shot && VenomBreath == null)
            {
                VenomBreath = new FireBall(new Vector2(tilemap_PosX, tilemap_PosY), new Vector2(-1, 0), 5, Color.DarkOliveGreen);
                VenomBreath.isFromPlayer = false;
                mvX = 0;
                mvX = 0;
                shot = true;
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

}

