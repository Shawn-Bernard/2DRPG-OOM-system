using System;
using System.Numerics;



class Actor 
{
    private int Health; 
    public int health 
    {
        get { return Health;  }
        set { Health = Math.Max(0, Math.Max(100, value));  }
    }

    private int Power;
    public int power 
    {
        get { return Power;  }
        set { Power = Math.Max(0, value);  }
    }

    private int Shield;
    public int shield 
    {
        get { return Shield; }
        set { Shield = Math.Max(0, value);  }
    }

    private int Life; 
    public int life 
    {
        get { return Life; }
        set { Life = Math.Max(0, value);  }
    }

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




