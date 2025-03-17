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
    }

    private KeyboardState oldState;
    private void EnemyTurn(Enemy enemy)
    {
        turn = false;
        enemy.turn = true;
    }

    public void UpdatePlayerInput(GameTime gameTime, Tilemap tileMap, Enemy enemy)
    {
        KeyboardState keyboardState = Keyboard.GetState();

        if (turn)
        {
            if (active && !_healthSystem.isStunned)
            {
                _healthSystem.defaultStatus();
                if (keyboardState.IsKeyDown(Keys.A))
                {
                    if (!oldState.IsKeyDown(Keys.A))
                    {
                        {
                            if (checkingForCollision(tileMap, '#', this, -1, 0) || checkingForCollision(tileMap, '$', this, -1, 0))
                            {
                                //Nothing Happen
                            }
                            else
                            {
                                Movement(-1, 0);
                                EnemyTurn(enemy); 
                                //Here checks if after moving, collides with the enemy
                                if (enemyCollision(this, enemy))
                                {
                                    enemy._healthSystem.TakeDamage(this._healthSystem.power);

                                    if(!enemy._healthSystem.isStunned) //Check if the enemy is not stunned when is dealing damage, stun the enemy in case is true
                                        enemy._healthSystem.makeStunned();

                                    //The player steps back after attacking
                                    this.Movement(1, 0);
                                    

                                }

                            }
                        }
                    }
                }
                else if (keyboardState.IsKeyDown(Keys.D))
                {
                    if (!oldState.IsKeyDown(Keys.D))
                    {
                        if (checkingForCollision(tileMap, '#', this, 1, 0) || checkingForCollision(tileMap, '$', this, 1, 0))
                        {
                            //Nothing Happen
                        }
                        else
                        {
                            Movement(1, 0);
                            EnemyTurn(enemy);
                            //Here checks if after moving, collides with the enemy
                            if (enemyCollision(this, enemy))
                            {
                                enemy._healthSystem.TakeDamage(this._healthSystem.power);
                                if (!enemy._healthSystem.isStunned) //Check if the enemy is not stunned when is dealing damage, stun the enemy in case is true
                                    enemy._healthSystem.makeStunned();
                                //The player steps back after attacking
                                this.Movement(-1, 0);
                                
                            }

                        }
                    }
                }
                else if (keyboardState.IsKeyDown(Keys.W))
                {
                    if (!oldState.IsKeyDown(Keys.W))
                    {
                        if (checkingForCollision(tileMap, '#', this, 0, -1) || checkingForCollision(tileMap, '$', this, 0, -1))
                        {
                            //Nothing Happen
                        }
                        else
                        {
                            Movement(0, -1);
                            EnemyTurn(enemy);
                            //Here checks if after moving, collides with the enemy
                            if (enemyCollision(this, enemy))
                            {
                                enemy._healthSystem.TakeDamage(this._healthSystem.power);
                                if (!enemy._healthSystem.isStunned) //Check if the enemy is not stunned when is dealing damage, stun the enemy in case is true
                                    enemy._healthSystem.makeStunned();
                                //The player steps back after attacking
                                this.Movement(0, 1);
                                
                            }

                        }
                    }
                }
                else if (keyboardState.IsKeyDown(Keys.S))
                {
                    if (!oldState.IsKeyDown(Keys.S))
                    {
                        if (checkingForCollision(tileMap, '#', this, 0, 1) || checkingForCollision(tileMap, '$', this, 0, 1))
                        {
                            //Nothing Happen
                        }
                        else
                        {
                            Movement(0, 1);
                            EnemyTurn(enemy);
                            //Here checks if after moving, collides with the enemy
                            if (enemyCollision(this, enemy))
                            {
                                enemy._healthSystem.TakeDamage(this._healthSystem.power);
                                if (!enemy._healthSystem.isStunned) //Check if the enemy is not stunned when is dealing damage, stun the enemy in case is true
                                    enemy._healthSystem.makeStunned();
                                //The player steps back after attacking
                                this.Movement(0, -1);
                                
                            }

                        }
                    }
                }
            }
            else if (_healthSystem.isStunned)
            {
                //When you are stunned, your turn is skipped but your make unstunned
                _healthSystem.makeUnstunned();
                EnemyTurn(enemy);
                _healthSystem.defaultStatus();
            }
        }

        
                

        oldState = keyboardState;
    }
}

