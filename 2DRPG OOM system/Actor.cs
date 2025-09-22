using _2DRPG_OOM_system;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Runtime.Intrinsics.X86;



public class Actor 
{
    // This is the X position of the actor
    private int Tilemap_PosX;    
   
    public int tilemap_PosX 
    {
        get { return Tilemap_PosX; }
        set { Tilemap_PosX = Math.Max(0, Math.Min(29, value)); }
    }

    // This is the Y position of the actor
    private int Tilemap_PosY;

    public int tilemap_PosY 
    {
        get { return Tilemap_PosY; }
        set { Tilemap_PosY = Math.Max(0, Math.Min(10, value)); }
    }

    public HealthSystem _healthSystem = new HealthSystem();

    // This will check if the actor is alive in the game
    public bool active;
    
    // This is the actor turn
    public bool turn;  
    public bool ismyTurn;  // This will tell the UI if it's this actor's current turn
    public float waitingTime = 0;
    public bool waitingPhase;
    public bool hasMoved = false;   // This tell if the actor used its turn and has already moved

    // This determine the coordinates to crop the texture image to get the correct sprite for the actor
    public int cropPositionX;  
    public int cropPositionY; 


    // Movement
    public Vector2 moveDir;   // This is the direction of the actor next move
    public Vector2 facingDir;  // where the actor is facing
     
    public Color AColor;  // The color of the actor, this may change to show the status

    public float CountingTime = 0;
    public bool isDamage;

    public string feedback = "";  // When any actor receives damage or healing, this will show up as a feedback visualization

    // This check if two objects are colliding, is will ask two position (x,y)
    public bool CheckForObjCollision(int Xo, int Yo, int Xt, int Yt) 
    {
        return Xo == Xt && Yo == Yt; 
    }

   // This moves the actor
    public void Movement(int mvX, int mvY)
    {
        tilemap_PosX += mvX;
        tilemap_PosY += mvY;
    }

    // This check for collision, is accesing the tilemap to check if the actor will be in a certain char position
    public bool checkingForCollision(Tilemap _tilemap, char _char, Actor _actor, int x, int y) 
    {
        return _tilemap.MapToChar(_tilemap.multidimensionalMap, _actor.tilemap_PosX + x, _actor.tilemap_PosY + y) == _char;
    }

    // this check if the actor will collide with another actor which will trigger an attack
    public bool enemyCollision(Actor _attacker, Actor _attacked) 
    {
        return _attacker.tilemap_PosX == _attacked.tilemap_PosX && _attacker.tilemap_PosY == _attacked.tilemap_PosY;
    }

    public virtual void TurnUpdate(GameTime gameTime) 
    {

    }

    public virtual void DrawStats(SpriteBatch _spriteBatch, int num, int posY) 
    {
        
    }

    public virtual void FinishTurn()
    {
        // The actor finish the turn, and now enable the transition between turns
        waitingPhase = true;
        waitingTime = 0;
        hasMoved = true;        
        
    }

    public void waitingTurnToFinish(float _time, GameTime _gameTime) 
    {
        // How much time between turns
        waitingTime += 10; //(float)_gameTime.ElapsedGameTime.TotalSeconds;
        if (waitingTime > _time)
        {

            waitingPhase = false;
            turn = false;
            hasMoved = false;
            waitingTime = 0;
            ismyTurn = false;
        }
    }
    
    public void damageVisualization(int damage) 
    {
        // The actor turn red to visualize that it get hit
        AColor = Color.Red;
        isDamage = true;
        feedback = "- " + damage.ToString(); 
    }
        

    public void damageTiming(float _time, GameTime _gameTime) 
    {
        // Time for make the actor return to the original colors in a certain period of time
        CountingTime += (float)_gameTime.ElapsedGameTime.TotalSeconds;
        if(CountingTime > _time) 
        {
            AColor = Color.White;
            isDamage = false;
            feedback = ""; 
        }
    }
    protected bool CheckForUnWalkable(int dx, int dy)
    {
        return checkingForCollision(Game1.tileMap, '#', this, dx, dy) || checkingForCollision(Game1.tileMap, '$', this, dx, dy); 
    }
}




