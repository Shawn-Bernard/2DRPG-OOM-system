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

    public void PlayerTurn(Player player) 
    {
        turn = false;
        player.turn = true;
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
                            PlayerTurn(myPlayer);
                            //Here checks if after moving, collides with the player
                            if (myPlayer.enemyCollision(this, myPlayer))
                            {
                                myPlayer._healthSystem.TakeDamage(_healthSystem.power);

                                if(!myPlayer._healthSystem.isStunned)  //Check if the player is not stunned when is dealing damage, stun the enemy in case is true
                                    myPlayer._healthSystem.makeStunned();
                                //The enemy steps back after attacking
                                Movement(-1, 0);
                                
                            }
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
                            PlayerTurn(myPlayer);
                            //Here checks if after moving, collides with the player
                            if (myPlayer.enemyCollision(this, myPlayer))
                            {
                                myPlayer._healthSystem.TakeDamage(_healthSystem.power);
                                if (!myPlayer._healthSystem.isStunned)  //Check if the player is not stunned when is dealing damage, stun the enemy in case is true
                                    myPlayer._healthSystem.makeStunned();
                                //The enemy steps back after attacking
                                Movement(1, 0);
                               
                            }
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
                            PlayerTurn(myPlayer);
                            //Here checks if after moving, collides with the player
                            if (myPlayer.enemyCollision(this, myPlayer))
                            {
                                myPlayer._healthSystem.TakeDamage(_healthSystem.power);
                                if (!myPlayer._healthSystem.isStunned)  //Check if the player is not stunned when is dealing damage, stun the enemy in case is true
                                    myPlayer._healthSystem.makeStunned();
                                //The enemy steps back after attacking
                                Movement(0, 1);
                                
                            }
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
                            PlayerTurn(myPlayer);
                            //Here checks if after moving, collides with the player
                            if (myPlayer.enemyCollision(this, myPlayer))
                            {
                                myPlayer._healthSystem.TakeDamage(_healthSystem.power);
                                if (!myPlayer._healthSystem.isStunned)  //Check if the player is not stunned when is dealing damage, stun the enemy in case is true
                                    myPlayer._healthSystem.makeStunned();
                                //The enemy steps back after attacking
                                Movement(0, -1);
                                
                            }
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
                PlayerTurn(myPlayer);                
                
            }
        }
    }


    //Methods I plan to use in the future

    /*private string[] State = new string[] { "walk", "chase", "attack" };

   public float calculatingDistance(Player _player) 
   {
       return 4f; 
   }

   public string GetState(string[] _state, int i) 
   {
       return _state[i];
   }


   private void Enemy_Movement(string[] _state, int i) 
   {
       switch (_state[i]) 
       {
           // Depend of the state, the enemy movement IA will change. I still have to think about it
           case "walk":
               Console.WriteLine("Enemy walk normally");
               break;
           case "chase":
               Console.WriteLine("Enemy chase the player");
               break;
           case "attack":
               Console.WriteLine("Enemy attacks");
               break;
           default:
               Console.WriteLine("Nothing");
               break;
       }


   } */

}

