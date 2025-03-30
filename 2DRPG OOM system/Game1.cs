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
        public static string mString;
        private KeyboardState oldState;

        // This string will contain the text from the text file
        private string pathToMyFile;
        public string whosTurn;
        public string turnDisplay;

        public TurnManager turnManager = new TurnManager();
        public static List<Item> itemsOnMap = new List<Item>();

        public static List<Projectile> projectiles = new List<Projectile>();
        
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
            //characters.Add( new Spider(10, 1, 0, 22, 5));
            characters.Add(new DarkMage(10, 1, 0, 20, 6));
            characters.Add(new Ghost(10, 5, 0, 15, 8));
            characters.Add(new Ghost(10, 1, 0, 22, 5));

            itemsOnMap.Add(new Potion(new Vector2(4, 6)));
            itemsOnMap.Add(new Potion(new Vector2(8, 7)));
            itemsOnMap.Add(new FireballScroll(new Vector2(10, 3)));
            itemsOnMap.Add(new LightningScroll(new Vector2(2, 8)));

            //projectiles.Add(new FireBall(new Vector2(4, 3), new Vector2(1, 0), Color.Red));

            /*
            for(int i = 0; i < characters.Count; i++) 
            {
                if (characters[i] is Spider)
                {
                    ((Spider)characters[i])._pathfinder.InitializePathfinding(tileMap.multidimensionalMap, new Point(characters[i].tilemap_PosX, characters[i].tilemap_PosY), new Point(characters[0].tilemap_PosX, characters[0].tilemap_PosY));
                }
            }*/

             
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

            for (int i = 0; i < projectiles.Count; i++)
                projectiles[i].ProjectileUpdate(gameTime);

            for (int i = 0; i < projectiles.Count; i++) {
                if (projectiles[i].hit) 
                {
                    projectiles.Remove(projectiles[i]);
                }
            }


            for (int i = 0; i < characters.Count; i++) 
            {
                if (characters[i]._healthSystem.life <= 0)
                    characters.Remove(characters[i]); 
            }

            if (characters[0]._healthSystem.life > 0)
            {
                if (((Player)characters[0]).turn && !((Player)characters[0]).hasMoved)
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
                    _spriteBatch.Draw(mapTexture, new Rectangle(characters[i].tilemap_PosX * tileSize * 2, (characters[i].tilemap_PosY + 5) * tileSize * 2, tileSize * 2, tileSize * 2), new Rectangle(characters[i].cropPositionX * tileSize, characters[i].cropPositionY * tileSize, tileSize, tileSize), characters[i].AColor);
            }

            _spriteBatch.DrawString(mySpriteFont, whosTurn, new Vector2(300f, 60f), Color.White);
                                   

            characters[0].DrawStats(_spriteBatch, 1, 0);


            // Draw all the items that are in the tilemap
            for(int i = 0; i < itemsOnMap.Count; i++) 
            {
                itemsOnMap[i].DrawItem(_spriteBatch); 
            } 

            //for(int i = 0; i < ((Spider)characters[1])._pathfinder.exploredNodes.Count; i++)
                //{
                //_spriteBatch.Draw(mapTexture, new Rectangle(((Spider)characters[1])._pathfinder.exploredNodes[i].X * tileSize * 2, (((Spider)characters[1])._pathfinder.exploredNodes[i].Y + 5) * tileSize * 2, tileSize * 2, tileSize * 2), new Rectangle(10 * tileSize, 6 * tileSize, tileSize, tileSize), Color.White); 
            //}

            // Draw the projectiles
            for(int i = 0; i < projectiles.Count; i++)
            _spriteBatch.Draw(mapTexture, new Rectangle(projectiles[i].X * tileSize * 2, (projectiles[i].Y + 5) * tileSize * 2, tileSize * 2, tileSize * 2), new Rectangle(projectiles[i].cropX * tileSize, projectiles[i].cropY * tileSize, tileSize, tileSize), projectiles[i].pColor); 


            for (int i = 1; i < characters.Count; i++) 
            {
                if (characters[i] is Enemy)
                    ((Enemy)characters[i]).DrawStats(_spriteBatch, i, (i - 1) * 40);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        
    }
}
