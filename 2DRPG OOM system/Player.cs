using Microsoft.Xna.Framework;


public class Player : Actor
{
    public HealthSystem _healthSystem = new HealthSystem();

    public Player(int hp, int atk, int shld, int iLife, int iPosX, int iPosY)
    {
        _healthSystem.health = hp;
        _healthSystem.power = atk;
        _healthSystem.shield = shld;
        _healthSystem.life = iLife;
        tilemap_PosX = iPosX;
        tilemap_PosY = iPosY;
        _healthSystem.isStunned = false; 
        active = true;

        _healthSystem.setMaxHP(hp);
        _healthSystem.setMaxShield(shld);
    }    

}

