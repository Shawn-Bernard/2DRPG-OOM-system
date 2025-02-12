using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class Player : Actor
{
    public HealthSystem _healthSystem = new HealthSystem(); 

    public Player(int hp, int atk, int shld, int iLife, Vector2 startingPlace) 
    {
        _healthSystem.health = hp;
        _healthSystem.power = atk;
        _healthSystem.shield = shld;
        _healthSystem.life = iLife;
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

