using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

class Enemy : Actor
{
    public Enemy(int hp, int atk, int shld, Vector2 startingPlace)
    {
        health = hp;
        power = atk;
        shield = shld;
        life = 1;
        position = startingPlace;
    }

    private string[] State = new string[] { "walk", "chase", "attack" };

    public float calculatingDistance(Player _player) 
    {
        return _player.position.X;  
    }

    
}

