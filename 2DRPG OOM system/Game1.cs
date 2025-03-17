using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace _2DRPG_OOM_system
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;        
        private Texture2D mapTexture; 
        // Creating the Actors: The player and the enemy
        public Player myPlayer = new Player(10, 1, 3, 3, 3, 3);
        public Enemy theEnemy = new Enemy(10, 1, 0, 22, 5);

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
        public string whosTurn;
        public string turnDisplay; 
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

                       
            //Based on the button pressed, the player will move
            myPlayer.UpdatePlayerInput(gameTime, tileMap, theEnemy); 


            //The enemy movement when is its turn
            theEnemy.UpdateEnemyMovement(gameTime, tileMap, myPlayer);


            // Here it checks if the player collides with the door to generate a new map
            if(myPlayer.checkingForCollision(tileMap, '@', myPlayer, 0, 0)) 
            {
                myPlayer.tilemap_PosX = 3;
                myPlayer.tilemap_PosY = 3;
                mString = tileMap.GenerateMapString(25, 10);
                tileMap.ConvertToMap(mString, tileMap.multidimensionalMap);
            }
           
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


            // Draw the UI, stats for the player 
            _spriteBatch.DrawString(mySpriteFont, "Player: ", new Vector2(0, 0), Color.White); 
            _spriteBatch.DrawString(mySpriteFont, "HP: " + myPlayer._healthSystem.health + " Shield: " + myPlayer._healthSystem.shield + " Life: " +
                myPlayer._healthSystem.life ,  new Vector2(0, 30), Color.White);
            _spriteBatch.DrawString(mySpriteFont, "Status: " + myPlayer._healthSystem.status, new Vector2(0, 60), Color.White);

            // Draw the UI, stats for the enemy           
            _spriteBatch.DrawString(mySpriteFont, "Enemy: ", new Vector2(600, 0), Color.White);
            _spriteBatch.DrawString(mySpriteFont, "HP: " + theEnemy._healthSystem.health + " Shield: " + theEnemy._healthSystem.shield, 
                new Vector2(600, 30), Color.White);
            _spriteBatch.DrawString(mySpriteFont, "Status: " + theEnemy._healthSystem.status, new Vector2(600, 60), Color.White); 
                        

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        
    }
}
