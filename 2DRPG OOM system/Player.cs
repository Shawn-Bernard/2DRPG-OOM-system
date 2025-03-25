using _2DRPG_OOM_system;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


public class Player : Actor
{    

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
    public bool hasMoved = false; 

    private void FinishTurn()
    {        
        waitingPhase = true;
        waitingTime = 0;
        hasMoved = true; 
    }

    public override void TurnUpdate(GameTime gameTime)
    {
        KeyboardState keyboardState = Keyboard.GetState();


        if (turn & !hasMoved)
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
                if(checkingForCollision(Game1.tileMap, '#', this, (int)moveDir.X, (int)moveDir.Y) || checkingForCollision(Game1.tileMap, '$', this, (int)moveDir.X, (int)moveDir.Y)) 
                {
                    moveDir = new Vector2(0, 0);
                    
                }
                else 
                {
                    for (int i = 1; i < Game1.characters.Count; i++)
                    {
                        if (CheckForObjCollision(tilemap_PosX + (int)moveDir.X, tilemap_PosY + (int)moveDir.Y, Game1.characters[i].tilemap_PosX, Game1.characters[i].tilemap_PosY))
                        {                           
                            Game1.characters[i]._healthSystem.TakeDamage(this._healthSystem.power);
                            moveDir = new Vector2(0, 0);
                        }
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
            if(waitingTime > 2f)             {
                
                waitingPhase = false;
                turn = false;
                hasMoved = false;
                waitingTime = 0;
            }
        }
                

        oldState = keyboardState;
    }

    public override void DrawStats(SpriteBatch _spriteBatch, int num, int posY) 
    {
        _spriteBatch.DrawString(Game1.mySpriteFont, "Player: ", new Vector2(0, posY), Color.White);
        _spriteBatch.DrawString(Game1.mySpriteFont, "HP: " + _healthSystem.health, new Vector2(0, posY + 25), Color.White);
        _spriteBatch.DrawString(Game1.mySpriteFont, "Shield: " + _healthSystem.shield, new Vector2(0, posY + 50), Color.White);
        _spriteBatch.DrawString(Game1.mySpriteFont, "Lives: " + _healthSystem.life, new Vector2(0, posY +75), Color.White);
    }

}

