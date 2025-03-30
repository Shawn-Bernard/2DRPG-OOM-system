using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class FireBall : Projectile
{
    public FireBall(Vector2 Position, Vector2 Direction, Color _color) 
    {
        position = Position;
        direction = Direction;
        hit = false;
        cropX = 6;
        cropY = 8;
        pColor = _color; 
    }


}

