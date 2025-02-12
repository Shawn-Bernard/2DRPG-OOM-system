using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;



public class Actor 
{
    

    public Vector2 position; 

    bool CheckForCollision(Vector2 position1, Vector2 position2) 
    {
        return position1 == position2; 
    }

    public virtual void Attack() 
    {
        Console.WriteLine("There is an attack!"); 
    }

}




