using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

class Enemy : Actor
{
    public HealthSystem _healthSystem = new HealthSystem();

    public Enemy(int hp, int atk, int shld, Vector2 startingPlace)
    {
        _healthSystem.health = hp;
        _healthSystem.power = atk;
        _healthSystem.shield = shld;
        _healthSystem.life = 1;
        position = startingPlace;
    }

    private string[] State = new string[] { "walk", "chase", "attack" };

    public float calculatingDistance(Player _player) 
    {
        return _player.position.X;  
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


    
}

