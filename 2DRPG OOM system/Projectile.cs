using _2DRPG_OOM_system;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Projectile
{
    public Vector2 position;
    public int X => (int)position.X;
    public int Y => (int)position.Y;

    public Vector2 direction;

    public bool hit;

    public int cropX;
    public int cropY;

    public Color pColor;   // the color of the projectile

    
    public void ProjectileUpdate(GameTime gameTime) 
    {
        // the projectile moves until it hit a wall
        if(!hit)
        position += direction * (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (collidingWithWall(Game1.tileMap, '#') || collidingWithWall(Game1.tileMap, '$')) 
            hit = true; 

        // the projectile moves until it hit a wall
        for(int i = 0; i < Game1.characters.Count; i++) 
        {
            if (collidingWithActor(Game1.characters[i]))
            {
                hit = true;
                Game1.characters[i]._healthSystem.TakeDamage(5); 
            }
        }

    }

    public bool collidingWithWall(Tilemap _tilemap, char col)
    {
        return _tilemap.MapToChar(_tilemap.multidimensionalMap, X, Y ) == col;
    }

    public bool collidingWithActor(Actor actor) 
    {
        return actor.tilemap_PosX == X && actor.tilemap_PosY == Y;  
    }
}

