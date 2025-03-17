using System;

public class HealthSystem
{
    // variables
    private int Health;   

    public int health
    {
        get { return Health; }
        set { Health = Math.Max(0, Math.Min(MaxHealth, value)); }
    }

    private int MaxHealth = 100;   //The max hp
    public int maxHealth
    {
        get { return MaxHealth; }
        set { MaxHealth = Math.Max(1, Math.Min(100, value)); }
    }

    private int Power;
    public int power
    {
        get { return Power; }
        set { Power = Math.Max(0, value); }
    }

    private int Shield;    
    public int shield
    {
        get { return Shield; }
        set { Shield = Math.Max(0, Math.Min(MaxShield, value)); }
    }

    private int MaxShield = 100;

    public int maxShield 
    {
        get { return MaxShield; }
        set { MaxShield = Math.Max(0, Math.Min(100, value));  }
    }

    private int Life;
    public int life
    {
        get { return Life; }
        set { Life = Math.Max(0, value); }
    }

    public bool isStunned { get; set; }
    
    public string status { get; set; }
    public void TakeDamage(int damage) 
	{
        // This make the calculation when the actor receives damage. Both health and shield can't be negative, the shield should receive the damage first
        if(damage > shield) 
        {
            if(damage - shield > health) 
            {
                // When the actor's hp reaches to 0, revive should be triggered
                health = 0; 
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
        // If the actor has lives, loses one and restore his max hp and max shield
        if (life > 1)
        {
            life--;
            resetStats();
        }
        else
            life = 0; 
    }

    private void resetStats() 
    {
        health = maxHealth;
        shield -= maxShield;
    }

    public void setMaxHP(int _maxhp) 
    {
        // Set the max HP
        maxHealth = _maxhp;
    }

    public void setMaxShield(int _maxshield) 
    {
        // Set the max Shield
        maxShield = _maxshield;
    }

    public void makeStunned() 
    {
        isStunned = true;
        status = "Stunned";
    }

    public void makeUnstunned() 
    {
        isStunned = false;
        
    }

    public void defaultStatus() 
    {
        status = "Normal"; 
    }

}
