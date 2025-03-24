using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


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
        turn = true;
        _healthSystem.status = "Normal";
        _healthSystem.setMaxHP(hp);
        _healthSystem.setMaxShield(shld);
        keyPress = false;
    }

    private KeyboardState oldState;
    public bool waitingPhase = false;
    public float waitingTime = 0; 

    private void FinishTurn()
    {
        turn = false;
        waitingPhase = true;
        waitingTime = 0; 
    }

    public void UpdatePlayerInput(GameTime gameTime, Tilemap tileMap, Enemy enemy)
    {
        KeyboardState keyboardState = Keyboard.GetState();

        if (turn)
        {
            if (active && !_healthSystem.isStunned)
            {
                moveDir = new Vector2(0, 0); 
                _healthSystem.defaultStatus();
                if (keyboardState.IsKeyDown(Keys.A))
                {
                    if (!oldState.IsKeyDown(Keys.A))
                    {
                        moveDir = new Vector2(-1, 0); 
                        keyPress = true;
                    }
                    
                }
                else if (keyboardState.IsKeyDown(Keys.D))
                {
                    if (!oldState.IsKeyDown(Keys.D))
                    {
                        moveDir = new Vector2(1, 0); 
                        keyPress = true;
                    }
                }
                else if (keyboardState.IsKeyDown(Keys.W))
                {
                    if (!oldState.IsKeyDown(Keys.W))
                    {
                        moveDir = new Vector2(0, -1); 
                        keyPress = true;
                    }
                        
                    
                }
                else if (keyboardState.IsKeyDown(Keys.S))
                {
                    if (!oldState.IsKeyDown(Keys.S))
                    {
                        moveDir = new Vector2(0, 1); 
                        keyPress = true;
                    }
                 
                }

            }
            else if (_healthSystem.isStunned)
            {
                //When you are stunned, your turn is skipped but your make unstunned
                _healthSystem.makeUnstunned();
                FinishTurn();
                _healthSystem.defaultStatus();
                keyPress = false;
            }

            if (keyPress) 
            {
                if(checkingForCollision(tileMap, '#', this, (int)moveDir.X, (int)moveDir.Y) || checkingForCollision(tileMap, '$', this, (int)moveDir.X, (int)moveDir.Y)) 
                {
                    moveDir = new Vector2(0, 0);
                    
                }
                else 
                {
                    if(CheckForCollision(tilemap_PosX + (int)moveDir.X, tilemap_PosY + (int)moveDir.Y, enemy.tilemap_PosX, enemy.tilemap_PosY)) 
                    {
                        enemy._healthSystem.TakeDamage(this._healthSystem.power); 
                        moveDir = new Vector2(0, 0);
                    }
                    Movement((int)moveDir.X, (int)moveDir.Y);                    
                    FinishTurn();
                }
                keyPress = false;
                
            }

        }        


        if (waitingPhase) 
        {
            waitingTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if(waitingTime > 2f) 
            {
                enemy.turn = true; 
                waitingPhase = false;
                waitingTime = 0;
            }
        }
                

        oldState = keyboardState;
    }
}

