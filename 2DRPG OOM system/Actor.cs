using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;



public class Actor 
{

    private int Tilemap_PosX;
    
    public int tilemap_PosX 
    {
        get { return Tilemap_PosX; }
        set { Tilemap_PosX = Math.Max(0, Math.Min(29, value)); }
    }

    private int Tilemap_PosY;

    public int tilemap_PosY 
    {
        get { return Tilemap_PosY; }
        set { Tilemap_PosY = Math.Max(0, Math.Min(10, value)); }
    }
        

    public bool CheckForCollision(int Xo, int Yo, int Xt, int Yt) 
    {
        return Xo == Xt && Yo == Yt; 
    }

    public virtual void Attack() 
    {
        Console.WriteLine("There is an attack!"); 
    }

    public void Movement(int mvX, int mvY)
    {
        tilemap_PosX += mvX;
        tilemap_PosY += mvY;
    }

    public bool checkingForCollision(Tilemap _tilemap, char _char, Actor _actor, int x, int y) 
    {
        return _tilemap.MapToChar(_tilemap.multidimensionalMap, _actor.tilemap_PosX + x, _actor.tilemap_PosY + y) == _char;
    }

    public bool enemyCollision(Actor _attacker, Actor _attacked) 
    {
        return _attacker.tilemap_PosX == _attacked.tilemap_PosX && _attacker.tilemap_PosY == _attacked.tilemap_PosY;
    }

}




