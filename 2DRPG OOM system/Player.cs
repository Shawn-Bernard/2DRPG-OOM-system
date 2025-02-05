using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

class Player : Actor
{
    public Player(int hp, int atk, int shld, int iLife, Vector2 startingPlace) 
    {
        health = hp;
        power = atk;
        shield = shld;
        life = iLife;
        position = startingPlace; 
    }

    public int score = 0; 

    public void Movement(Vector2 dir) 
    {
        position += dir; 
    }

    public void GettingScore(int _score) 
    {
        score += _score; 
    }


}

