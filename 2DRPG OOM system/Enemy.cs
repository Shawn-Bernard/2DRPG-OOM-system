using Microsoft.Xna.Framework;
using System;
using System.Security.Cryptography.X509Certificates;


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
    }

    public string eMovement; 

    private string[] State = new string[] { "walk", "chase", "attack" };

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


    }

    public string TypeOfMovement() 
    {
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

    





    
}

