using Microsoft.Xna.Framework;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;


public class Enemy : Actor
{
    public HealthSystem _healthSystem = new HealthSystem();

    public Enemy(int hp, int atk, int shld, int iPosX, int iPosY)
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
    }

    public string eMovement;
    public bool waitingPhase = false;
    public float waitingTime = 0;

    public string TypeOfMovement() 
    {
        // This is the AI for the movement for the enemies which is at random
        string _typeOfMovement = "";
        System.Random random = new System.Random();
        int accN = random.Next(0, 4);

        switch (accN) 
        {
            case 0:                
                _typeOfMovement = "Right";
                break;
            case 1:
                _typeOfMovement = "Left";
                break;
            case 2:
                _typeOfMovement = "Down";
                break;
            case 3:
                _typeOfMovement = "Up"; 
                break;
            default:
                //Nothing Happens
                break;                 
        }

        return _typeOfMovement;

    }

    public async void waitHandler()
    {
        //This makes a little delay to prevent the enemy to move inmediatly after the player moves
        await Task.Delay(2000);
    }

    public void FinishTurn()
    {
        turn = false;
        waitingPhase = true;
        waitingTime = 0;
    }

    public void UpdateEnemyMovement(GameTime gameTime, Tilemap tileMap, Player myPlayer) 
    {
        if (turn)
        {
            // Checks if the enemy is still alive
            if (active && !_healthSystem.isStunned)
            {
                _healthSystem.defaultStatus();
                waitHandler();
                eMovement = TypeOfMovement(); // Get a random movement base on the AI method

                switch (eMovement)
                {
                    //Base on the direction, the enemy will move in the corresponding direction
                    case "Right":
                        if (checkingForCollision(tileMap, '#', this, 1, 0))
                        {
                            //It doesn't move
                        }
                        else
                        {
                            Movement(1, 0);                           
                            //Here checks if after moving, collides with the player
                            if (myPlayer.enemyCollision(this, myPlayer))
                            {
                                myPlayer._healthSystem.TakeDamage(_healthSystem.power);

                                //The enemy steps back after attacking
                                Movement(-1, 0);
                                
                            }
                            FinishTurn();
                        }
                        break;
                    case "Left":
                        if (checkingForCollision(tileMap, '#', this, -1, 0))
                        {
                            //It doesn't move
                        }
                        else
                        {
                            Movement(-1, 0);                           
                            //Here checks if after moving, collides with the player
                            if (myPlayer.enemyCollision(this, myPlayer))
                            {
                                myPlayer._healthSystem.TakeDamage(_healthSystem.power);
                                
                                //The enemy steps back after attacking
                                Movement(1, 0);
                               
                            }
                            FinishTurn();
                        }
                        break;
                    case "Up":
                        if (checkingForCollision(tileMap, '#', this, 0, -1))
                        {
                            //It doesn't move
                        }
                        else
                        {
                            Movement(0, -1);                            
                            //Here checks if after moving, collides with the player
                            if (myPlayer.enemyCollision(this, myPlayer))
                            {
                                myPlayer._healthSystem.TakeDamage(_healthSystem.power);
                                
                                Movement(0, 1);
                                
                            }
                            FinishTurn();
                        }
                        break;
                    case "Down":
                        if (checkingForCollision(tileMap, '#', this, 0, 1))
                        {
                            //It doesn't move
                        }
                        else
                        {
                            Movement(0, 1);                            
                            //Here checks if after moving, collides with the player
                            if (myPlayer.enemyCollision(this, myPlayer))
                            {
                                myPlayer._healthSystem.TakeDamage(_healthSystem.power);
                                
                                //The enemy steps back after attacking
                                Movement(0, -1);
                                
                            }
                            FinishTurn();
                        }
                        break;
                    default:
                        { // do nothing
                            break;
                        }
                }
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
            waitingTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (waitingTime > 2f)
            {
                myPlayer.turn = true;
                waitingPhase = false;
                waitingTime = 0;
            }
        }
    }
    

}

