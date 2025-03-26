using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;


namespace _2DRPG_OOM_system
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;        
        public static Texture2D mapTexture; 
        // Creating the Actors: The player and the enemy
        //public static Player myPlayer = new Player(10, 1, 3, 3, 3, 3);
        //public Enemy theEnemy = new Enemy(10, 1, 0, 22, 5);
        public static List<Actor> characters = new List<Actor>(); 

        public bool myTurn = true;
        // This determine the size the tiles are going to have        
        public static int tileSize = 16;

        public static SpriteFont mySpriteFont;
        // Creating the Tilemap 
        public static Tilemap tileMap = new Tilemap();
        private string mString;
        private KeyboardState oldState;

        // This string will contain the text from the text file
        private string pathToMyFile;
        public string whosTurn;
        public string turnDisplay;

        public TurnManager turnManager = new TurnManager();
        public static List<Item> itemsOnMap = new List<Item>();
        

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

            characters.Add(new Player(10, 1, 3, 3, 3, 3)); 
            characters.Add( new Spider(10, 1, 0, 22, 5));
            characters.Add(new Spider(10, 1, 0, 20, 6));
            characters.Add(new Ghost(10, 1, 0, 15, 8));

            itemsOnMap.Add(new Potion(new Vector2(4, 6)));
            itemsOnMap.Add(new Potion(new Vector2(8, 7)));
            

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

                      
            // This is the logic for the turns                        
            turnManager.UpdateTurnManager(gameTime);


            // Here it checks if the player collides with the door to generate a new map
            if (characters[0].checkingForCollision(tileMap, '@', characters[0], 0, 0))
            {
                characters[0].tilemap_PosX = 3;
                characters[0].tilemap_PosY = 3;
                mString = tileMap.GenerateMapString(25, 10);
                tileMap.ConvertToMap(mString, tileMap.multidimensionalMap);
            }

            for(int i = 0; i < characters.Count; i++) 
            {
                if (characters[i]._healthSystem.life <= 0)
                    characters.Remove(characters[i]); 
            }

            if (characters[0]._healthSystem.life > 0)
            {
                if (!((Player)characters[0]).hasMoved)
                    whosTurn = "Player Turn";
                else
                    whosTurn = "Enemies Turn";
            }

            for(int i = 0; i < itemsOnMap.Count; i++) 
            {
                if (itemsOnMap[i].isUsed)
                    itemsOnMap.Remove(itemsOnMap[i]); 
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
            if (characters[0] is Player)
            {
                if (characters[0]._healthSystem.life > 0)
                    _spriteBatch.Draw(mapTexture, new Rectangle(characters[0].tilemap_PosX * tileSize * 2, (characters[0].tilemap_PosY + 5) * tileSize * 2, tileSize * 2, tileSize * 2), new Rectangle(characters[0].cropPositionX * tileSize, characters[0].cropPositionY * tileSize, tileSize, tileSize), Color.White);
                else
                {
                    _spriteBatch.DrawString(mySpriteFont, "You Lose!!", new Vector2(300, 0), Color.White);  // Write the message if you lose
                    characters[0].active = false;  // Neither the player nor the enemy cannot move.
                    characters[1].active = false;
                }
            }

            // Draw the enemy           

            for(int i = 1; i < characters.Count; i++) 
            {
                if (characters[i]._healthSystem.life > 0 && characters[i] is Enemy)
                    _spriteBatch.Draw(mapTexture, new Rectangle(characters[i].tilemap_PosX * tileSize * 2, (characters[i].tilemap_PosY + 5) * tileSize * 2, tileSize * 2, tileSize * 2), new Rectangle(characters[i].cropPositionX * tileSize, characters[i].cropPositionY * tileSize, tileSize, tileSize), Color.White);
            }

            _spriteBatch.DrawString(mySpriteFont, whosTurn, new Vector2(300f, 60f), Color.White);
                                   

            characters[0].DrawStats(_spriteBatch, 1, 0);



            for(int i = 0; i < itemsOnMap.Count; i++) 
            {
                itemsOnMap[i].DrawItem(_spriteBatch); 
            } 
                        

            for(int i = 1; i < characters.Count; i++) 
            {
                if (characters[i] is Enemy)
                    ((Enemy)characters[i]).DrawStats(_spriteBatch, i, (i - 1) * 40);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        
    }
}
