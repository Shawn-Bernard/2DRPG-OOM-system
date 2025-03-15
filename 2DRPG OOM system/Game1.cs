using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;

namespace _2DRPG_OOM_system
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;        
        private Texture2D mapTexture; 
        // Creating the Actors: The player and the enemy
        public Player myPlayer = new Player(50, 8, 50, 3, 3, 3);
        public Enemy theEnemy = new Enemy(30, 5, 0, 22, 5);

        public bool myTurn = true;
        // This determine the size the tiles are going to have        
        public int tileSize = 16;

        SpriteFont mySpriteFont;
        // Creating the Tilemap 
        private Tilemap tileMap = new Tilemap();
        private string mString;
        private KeyboardState oldState;

        // This string will contain the text from the text file
        private string pathToMyFile;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;             
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();            
            mapTexture = Content.Load<Texture2D>("The_Tilemap");            
            mySpriteFont = Content.Load<SpriteFont>("Font");
            mString = tileMap.GenerateMapString(25, 10);
            tileMap.ConvertToMap(mString, tileMap.multidimensionalMap);
            oldState = Keyboard.GetState();

            

        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
           
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            // TODO: Add your update logic here
            KeyboardState keyboardState = Keyboard.GetState();
            // this checks whose turn it is, the player or the enemy
            if (myTurn)
            {
                // Here checks if the player is still alive or still active
                if (myPlayer.active && !myPlayer._healthSystem.isStunned)
                {
                    //Based on the button pressed, the player will move
                    if (keyboardState.IsKeyDown(Keys.A))
                    {
                        if (!oldState.IsKeyDown(Keys.A))
                        {
                            //Here checks if is colliding with the wall or the block
                            if (myPlayer.checkingForCollision(tileMap, '#', myPlayer, -1, 0) || myPlayer.checkingForCollision(tileMap, '$', myPlayer, -1, 0))
                            {
                                //It doesn't move
                            }
                            else
                            {
                                myPlayer.Movement(-1, 0);
                                //Here checks if after moving, collides with the enemy
                                if (myPlayer.enemyCollision(myPlayer, theEnemy))
                                {
                                    theEnemy._healthSystem.TakeDamage(myPlayer._healthSystem.power);
                                    theEnemy._healthSystem.makeStunned();
                                    //The player steps back after attacking
                                    myPlayer.Movement(1, 0);
                                }
                                // Its the enemy turn now
                                myTurn = false;
                                System.Threading.Thread.Sleep(100);
                            }
                        }


                    }
                    else if (keyboardState.IsKeyDown(Keys.D))
                    {
                        if (!oldState.IsKeyDown(Keys.D))
                        {
                            //Here checks if is colliding with the wall or the block
                            if (myPlayer.checkingForCollision(tileMap, '#', myPlayer, +1, 0) || myPlayer.checkingForCollision(tileMap, '$', myPlayer, +1, 0))
                            {
                                //It doesn't move
                            }
                            else
                            {
                                myPlayer.Movement(1, 0);
                                //Here checks if after moving, collides with the enemy
                                if (myPlayer.enemyCollision(myPlayer, theEnemy))
                                {
                                    theEnemy._healthSystem.TakeDamage(myPlayer._healthSystem.power);
                                    theEnemy._healthSystem.makeStunned();
                                    //The player steps back after attacking
                                    myPlayer.Movement(-1, 0);
                                }
                                myTurn = false;
                                System.Threading.Thread.Sleep(100);
                            }
                        }

                    }
                    else if (keyboardState.IsKeyDown(Keys.W))
                    {
                        if (!oldState.IsKeyDown(Keys.W))
                        {
                            //Here checks if is colliding with the wall or the block
                            if (myPlayer.checkingForCollision(tileMap, '#', myPlayer, 0, -1) || myPlayer.checkingForCollision(tileMap, '$', myPlayer, 0, -1))
                            {
                                //It doesn't move
                            }
                            else
                            {
                                myPlayer.Movement(0, -1);
                                //Here checks if after moving, collides with the enemy
                                if (myPlayer.enemyCollision(myPlayer, theEnemy))
                                {
                                    theEnemy._healthSystem.TakeDamage(myPlayer._healthSystem.power);
                                    theEnemy._healthSystem.makeStunned();
                                    //The player steps back after attacking
                                    myPlayer.Movement(0, 1);
                                }
                                myTurn = false;
                                System.Threading.Thread.Sleep(100);
                            }
                        }

                    }
                    else if (keyboardState.IsKeyDown(Keys.S))
                    {
                        if (!oldState.IsKeyDown(Keys.S))
                        {
                            //Here checks if is colliding with the wall or the block
                            if (myPlayer.checkingForCollision(tileMap, '#', myPlayer, 0, 1) || myPlayer.checkingForCollision(tileMap, '$', myPlayer, 0, 1))
                            {
                                //It doesn't move
                            }
                            else
                            {
                                myPlayer.Movement(0, 1);
                                //Here checks if after moving, collides with the enemy
                                if (myPlayer.enemyCollision(myPlayer, theEnemy))
                                {
                                    theEnemy._healthSystem.TakeDamage(myPlayer._healthSystem.power);
                                    theEnemy._healthSystem.makeStunned(); 
                                    //The player steps back after attacking
                                    myPlayer.Movement(0, -1);
                                }
                                myTurn = false;
                                System.Threading.Thread.Sleep(100);
                            }
                        }

                    }
                }

                if (myPlayer._healthSystem.isStunned)
                    myPlayer._healthSystem.makeUnstunned(); 
                
            }
            else 
            {
                // Checks if the enemy is still alive
                if (theEnemy.active && !theEnemy._healthSystem.isStunned) { 
                theEnemy.eMovement = theEnemy.TypeOfMovement();  // Get a random movement base on the AI method
                    switch (theEnemy.eMovement)
                    {
                        //Base on the direction, the enemy will move in the corresponding direction
                        case "Right":
                            if (theEnemy.checkingForCollision(tileMap, '#', theEnemy, 1, 0))
                            {
                                //It doesn't move
                            }
                            else
                            {
                                theEnemy.Movement(1, 0);
                                //Here checks if after moving, collides with the player
                                if (myPlayer.enemyCollision(theEnemy, myPlayer))
                                {
                                    myPlayer._healthSystem.TakeDamage(theEnemy._healthSystem.power);
                                    myPlayer._healthSystem.makeStunned();
                                    //The enemy steps back after attacking
                                    theEnemy.Movement(-1, 0);
                                }
                            }
                            break;
                        case "Left":
                            if (theEnemy.checkingForCollision(tileMap, '#', theEnemy, -1, 0))
                            {
                                //It doesn't move
                            }
                            else
                            {
                                theEnemy.Movement(-1, 0);
                                //Here checks if after moving, collides with the player
                                if (myPlayer.enemyCollision(theEnemy, myPlayer))
                                {
                                    myPlayer._healthSystem.TakeDamage(theEnemy._healthSystem.power);
                                    myPlayer._healthSystem.makeStunned();
                                    //The enemy steps back after attacking
                                    theEnemy.Movement(1, 0);
                                }
                            }
                            break;
                        case "Up":
                            if (theEnemy.checkingForCollision(tileMap, '#', theEnemy, 0, -1))
                            {
                                //It doesn't move
                            }
                            else
                            {
                                theEnemy.Movement(0, -1);
                                //Here checks if after moving, collides with the player
                                if (myPlayer.enemyCollision(theEnemy, myPlayer))
                                {
                                    myPlayer._healthSystem.TakeDamage(theEnemy._healthSystem.power);
                                    myPlayer._healthSystem.makeStunned();
                                    //The enemy steps back after attacking
                                    theEnemy.Movement(0, 1);
                                }
                            }
                            break;
                        case "Down":
                            if (theEnemy.checkingForCollision(tileMap, '#', theEnemy, 0, 1))
                            {
                                //It doesn't move
                            }
                            else
                            {
                                theEnemy.Movement(0, 1);
                                //Here checks if after moving, collides with the player
                                if (myPlayer.enemyCollision(theEnemy, myPlayer))
                                {
                                    myPlayer._healthSystem.TakeDamage(theEnemy._healthSystem.power);
                                    myPlayer._healthSystem.makeStunned();
                                    //The enemy steps back after attacking
                                    theEnemy.Movement(0, -1);
                                }
                            }
                            break;
                        default:
                            { // do nothing
                                break;
                            }
                    }
                }

                if (theEnemy._healthSystem.isStunned)
                    theEnemy._healthSystem.makeUnstunned(); 

                myTurn = true;
               
            }

            // Here it checks if the player collides with the door to generate a new map
            if(myPlayer.checkingForCollision(tileMap, '@', myPlayer, 0, 0)) 
            {
                myPlayer.tilemap_PosX = 3;
                myPlayer.tilemap_PosY = 3;
                mString = tileMap.GenerateMapString(25, 10);
                tileMap.ConvertToMap(mString, tileMap.multidimensionalMap);
            }

            oldState = keyboardState;
            base.Update(gameTime);
            
        }



        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            
            // Draw the map
            for (int i = 0; i < 25; i++)
            {
                for (int j = 0 + 5; j < 10 + 5; j++)
                {
                    tileMap.getTheIndexes(tileMap.MapToChar(tileMap.multidimensionalMap, i, j - 5));
                    _spriteBatch.Draw(mapTexture, new Rectangle(i * tileSize * 2, j * tileSize * 2, tileSize * 2, tileSize * 2), new Rectangle(tileMap.horizontalIndex * tileSize, tileMap.verticalIndex * tileSize, tileSize, tileSize), Color.White);
                }
            }

            // Draw the player
            if (myPlayer._healthSystem.life > 0)
                _spriteBatch.Draw(mapTexture, new Rectangle(myPlayer.tilemap_PosX * tileSize * 2, (myPlayer.tilemap_PosY + 5) * tileSize * 2, tileSize * 2, tileSize * 2), new Rectangle(1 * tileSize, 8 * tileSize, tileSize, tileSize), Color.White);
            else
            {
                _spriteBatch.DrawString(mySpriteFont, "You Lose!!", new Vector2(300, 0), Color.White);  // Write the message if you lose
                myPlayer.active = false;  // Neither the player nor the enemy cannot move.
                theEnemy.active = false;
            }

            // Draw the enemy
            if (theEnemy._healthSystem.life > 0)
                _spriteBatch.Draw(mapTexture, new Rectangle(theEnemy.tilemap_PosX * tileSize * 2, (theEnemy.tilemap_PosY + 5) * tileSize * 2, tileSize * 2, tileSize * 2), new Rectangle(2 * tileSize, 10 * tileSize, tileSize, tileSize), Color.White);
            else
            {
                _spriteBatch.DrawString(mySpriteFont, "YOU WON!!", new Vector2(300, 0), Color.White); // Write the message if you win
                theEnemy.active = false;  // Neither the player nor the enemy cannot move.
                myPlayer.active = false;
            }

            // Draw the UI, stats for the player and for the enemy
            _spriteBatch.DrawString(mySpriteFont, "Player: ", new Vector2(0, 0), Color.White); 
            _spriteBatch.DrawString(mySpriteFont, "HP: " + myPlayer._healthSystem.health + " Shield: " + myPlayer._healthSystem.shield + " Life: " +
                myPlayer._healthSystem.life ,  new Vector2(0, 30), Color.White);


            _spriteBatch.DrawString(mySpriteFont, "Enemy: ", new Vector2(600, 0), Color.White);
            _spriteBatch.DrawString(mySpriteFont, "HP: " + theEnemy._healthSystem.health + " Shield: " + theEnemy._healthSystem.shield, 
                new Vector2(600, 30), Color.White);
                        

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        
    }
}
