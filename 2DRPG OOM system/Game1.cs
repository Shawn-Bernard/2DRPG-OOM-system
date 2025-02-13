using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace _2DRPG_OOM_system
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;        
        private Texture2D mapTexture; 

        public Player myPlayer = new Player(50, 8, 50, 3, 3, 3);
        public Enemy theEnemy = new Enemy(30, 5, 0, 22, 5);

        public bool myTurn = true;

        //private Dictionary<Vector2, int> tileMap;
        //private List<Rectangle> textureStore;

        public int tileSize = 16;

        SpriteFont mySpriteFont;

        private Tilemap tileMap = new Tilemap();
        private string mString;
        private string whosTurn;
        private KeyboardState oldState;
        

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            //textureStore = new() { new Rectangle(0, 0, 8, 8) }; 
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            //Debug.Print(myPlayer._healthSystem.health.ToString());
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

            if (myTurn)
            {
               
               
                if (keyboardState.IsKeyDown(Keys.A))
                {
                    if (!oldState.IsKeyDown(Keys.A))
                    {
                        if (myPlayer.checkingForCollision(tileMap, '#', myPlayer, -1, 0) || myPlayer.checkingForCollision(tileMap, '$', myPlayer, -1, 0))
                        {
                            //It doesn't move
                        }
                        else
                        {
                            myPlayer.Movement(-1, 0);
                            if(myPlayer.enemyCollision(myPlayer, theEnemy)) 
                            {
                                theEnemy._healthSystem.TakeDamage(myPlayer._healthSystem.power);
                                myPlayer.Movement(1, 0);
                            }
                            
                            myTurn = false;
                            System.Threading.Thread.Sleep(100);
                        }
                    }


                }
                else if (keyboardState.IsKeyDown(Keys.D))
                {
                    if (!oldState.IsKeyDown(Keys.D))
                    {
                        if (myPlayer.checkingForCollision(tileMap, '#', myPlayer, +1, 0) || myPlayer.checkingForCollision(tileMap, '$', myPlayer, +1, 0))
                        {
                            //It doesn't move
                        }
                        else
                        {
                            myPlayer.Movement(1, 0);
                            if (myPlayer.enemyCollision(myPlayer, theEnemy))
                            {
                                theEnemy._healthSystem.TakeDamage(myPlayer._healthSystem.power);
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
                        if (myPlayer.checkingForCollision(tileMap, '#', myPlayer, 0, -1) || myPlayer.checkingForCollision(tileMap, '$', myPlayer, 0, -1))
                        {
                            //It doesn't move
                        }
                        else
                        {
                            myPlayer.Movement(0, -1);
                            if (myPlayer.enemyCollision(myPlayer, theEnemy))
                            {
                                theEnemy._healthSystem.TakeDamage(myPlayer._healthSystem.power);
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
                        if (myPlayer.checkingForCollision(tileMap, '#', myPlayer,0, 1) || myPlayer.checkingForCollision(tileMap, '$', myPlayer, 0, 1))
                        {
                            //It doesn't move
                        }
                        else
                        {
                            myPlayer.Movement(0, 1);
                            if (myPlayer.enemyCollision(myPlayer, theEnemy))
                            {
                                theEnemy._healthSystem.TakeDamage(myPlayer._healthSystem.power);
                                myPlayer.Movement(0, -1);
                            }
                            myTurn = false;
                            System.Threading.Thread.Sleep(100);
                        }
                    }

                }
                
            }
            else 
            {

                theEnemy.eMovement = theEnemy.TypeOfMovement();
                switch (theEnemy.eMovement) 
                {
                    case "Right":
                        if(theEnemy.checkingForCollision(tileMap, '#', theEnemy, 1, 0)) 
                        {
                            //It doesn't move
                        }
                        else 
                        {
                            theEnemy.tilemap_PosX++;
                            if (myPlayer.enemyCollision(theEnemy, myPlayer))
                            {
                                myPlayer._healthSystem.TakeDamage(theEnemy._healthSystem.power);
                                myPlayer.Movement(-1, 0);
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
                            theEnemy.tilemap_PosX--;
                            if (myPlayer.enemyCollision(theEnemy, myPlayer))
                            {
                                myPlayer._healthSystem.TakeDamage(theEnemy._healthSystem.power);
                                myPlayer.Movement(1, 0);
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
                            theEnemy.tilemap_PosY--;
                            if (myPlayer.enemyCollision(theEnemy, myPlayer))
                            {
                                myPlayer._healthSystem.TakeDamage(theEnemy._healthSystem.power);
                                myPlayer.Movement(0, 1);
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
                            theEnemy.tilemap_PosY++;
                            if (myPlayer.enemyCollision(theEnemy, myPlayer))
                            {
                                myPlayer._healthSystem.TakeDamage(theEnemy._healthSystem.power);
                                myPlayer.Movement(0, 1);
                            }
                        }
                        break;
                }

                myTurn = true;
               
            }

            oldState = keyboardState;
            base.Update(gameTime);
            
        }



        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            //_spriteBatch.Draw(mapTexture, myPlayer.position, Color.White); 

            for (int i = 0; i < 25; i++)
            {
                for (int j = 0 + 5; j < 10 + 5; j++)
                {
                    tileMap.getTheIndexes(tileMap.MapToChar(tileMap.multidimensionalMap, i, j - 5));
                    _spriteBatch.Draw(mapTexture, new Rectangle(i * tileSize * 2, j * tileSize * 2, tileSize * 2, tileSize * 2), new Rectangle(tileMap.horizontalIndex * tileSize, tileMap.verticalIndex * tileSize, tileSize, tileSize), Color.White);
                }
            }

            if(myPlayer._healthSystem.life > 0)
            _spriteBatch.Draw(mapTexture, new Rectangle(myPlayer.tilemap_PosX * tileSize * 2, (myPlayer.tilemap_PosY + 5) * tileSize * 2, tileSize * 2, tileSize * 2), new Rectangle(1 * tileSize, 8 * tileSize, tileSize, tileSize), Color.White);
            else
                _spriteBatch.DrawString(mySpriteFont, "You Lose!!", new Vector2(300, 0), Color.White);

            if (theEnemy._healthSystem.life > 0)
                _spriteBatch.Draw(mapTexture, new Rectangle(theEnemy.tilemap_PosX * tileSize * 2, (theEnemy.tilemap_PosY + 5) * tileSize * 2, tileSize * 2, tileSize * 2), new Rectangle(2 * tileSize, 10 * tileSize, tileSize, tileSize), Color.White);
            else
                _spriteBatch.DrawString(mySpriteFont, "YOU WON!!", new Vector2(300, 0), Color.White); 

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
