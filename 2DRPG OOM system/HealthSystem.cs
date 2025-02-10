using System;

public class HealthSystem
{

    private int Health;
    private int maxHealth = 100; 

    public int health
    {
        get { return Health; }
        set { Health = Math.Max(0, Math.Max(maxHealth, value)); }
    }

    private int Power;
    public int power
    {
        get { return Power; }
        set { Power = Math.Max(0, value); }
    }

    private int Shield;
    private int maxShield = 100;
    public int shield
    {
        get { return Shield; }
        set { Shield = Math.Max(0, Math.Max(maxShield, value)); }
    }

    private int Life;
    public int life
    {
        get { return Life; }
        set { Life = Math.Max(0, value); }
    }




    public void TakeDamage(int damage) 
	{
        if(damage > shield) 
        {
            if(damage - shield > health) 
            {
                Revive();
            }
            else
            {
                health -= damage - shield;
                shield = 0;
            }
                       
        }
        else 
        {
            shield -= damage; 
        }
	}

    public void RecoverHealth(int healing) 
    {
        health += healing; 
    }

    public void RecoverShield(int healing) 
    {
        shield += healing;
    }

    public void Revive() 
    {
        if(life > 0) 
        {
            life--;
            health = maxHealth; 
            shield -= maxShield;
        }
    }

}
