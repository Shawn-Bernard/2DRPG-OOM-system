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

        public static PlacementManager placementManager = new PlacementManager();
        public List<Vector2> tempPoints = new List<Vector2>(); 
        
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

            // Set up the player and enemies in a set way
            characters.Add(new Player(15, 1, 3, 3, 3, 3));
            characters.Add(new DarkMage(10, 1, 0, 20, 6));
            characters.Add(new DarkMage(10, 5, 0, 15, 8));
            characters.Add(new Ghost(10, 1, 0, 22, 5));
           

            for (int i = 0; i < 10; i++)
            {
                // Generating random points, to place the item later
                tempPoints.Add(placementManager.GetWalkablePoint(tileMap));
            }

            // Place each item in each point generated
            placementManager.initializeItems(itemsOnMap, tempPoints); 
            

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
                if (characters[i] is DarkMage)
                {
                    if (((DarkMage)characters[i]).miasmaFire != null)
                    {
                        ((DarkMage)characters[i]).miasmaFire.ProjectileUpdate(gameTime);
                    }
                }

                if (characters[i] is Player)
                {
                    if (((Player)characters[i]).fireBall != null)
                    {
                        ((Player)characters[i]).fireBall.ProjectileUpdate(gameTime);
                    }
                }

                if (characters[i]._healthSystem.life <= 0)
                    characters.Remove(characters[i]);
                                
            }

            if (characters[0] is Player)
            {
                if (characters[0]._healthSystem.life > 0)
                {
                    if (((Player)characters[0]).turn && !((Player)characters[0]).hasMoved)
                        whosTurn = "Player Turn";
                    else
                        whosTurn = "Enemies Turn";
                    
                }

                for (int i = 0; i < itemsOnMap.Count; i++)
                {
                    if (itemsOnMap[i].isUsed)
                        itemsOnMap.Remove(itemsOnMap[i]);
                }
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

            // Draw all the items that are in the tilemap
            for (int i = 0; i < itemsOnMap.Count; i++)
            {
                itemsOnMap[i].DrawItem(_spriteBatch);
            }


            // Draw the player
            if (characters[0] is Player)
            {
                if (characters[0]._healthSystem.life > 0)
                {
                    _spriteBatch.Draw(mapTexture, new Rectangle(characters[0].tilemap_PosX * tileSize * 2, (characters[0].tilemap_PosY + 5) * tileSize * 2, tileSize * 2, tileSize * 2), new Rectangle(characters[0].cropPositionX * tileSize, characters[0].cropPositionY * tileSize, tileSize, tileSize), characters[0].AColor);

                    if (((Player)characters[0]).levelComplition)
                        _spriteBatch.DrawString(mySpriteFont, "Level Completed", new Vector2(300, 0), Color.White);
                }
                
            }
            else
            {
                _spriteBatch.DrawString(mySpriteFont, "You Lost", new Vector2(300, 0), Color.White);
                // All enemies disappear. They went to a party to celebrate the player's defeat.
                for (int i = 0; i < characters.Count; i++)
                {
                    characters[i].active = false;
                }
            }

            // Draw the enemy           

            for (int i = 1; i < characters.Count; i++) 
            {
                if (characters[i]._healthSystem.life > 0 && characters[i] is Enemy)
                    _spriteBatch.Draw(mapTexture, new Rectangle(characters[i].tilemap_PosX * tileSize * 2, (characters[i].tilemap_PosY + 5) * tileSize * 2, tileSize * 2, tileSize * 2), new Rectangle(characters[i].cropPositionX * tileSize, characters[i].cropPositionY * tileSize, tileSize, tileSize), characters[i].AColor);
            }

            _spriteBatch.DrawString(mySpriteFont, whosTurn, new Vector2(300f, 60f), Color.White);
                                   

            characters[0].DrawStats(_spriteBatch, 1, 0);                       
            
            
            // Draw the projectiles if exits. Only player or dark mages can creates projectiles, so it check only those two actors
            for (int i = 0; i < characters.Count; i++) 
            {
                if (characters[i] is DarkMage) 
                {
                    if (((DarkMage)characters[i]).miasmaFire != null)
                        _spriteBatch.Draw(mapTexture, new Rectangle( ((DarkMage)characters[i]).miasmaFire.X * tileSize * 2,  ( ((DarkMage)characters[i]).miasmaFire.Y + 5)  * tileSize * 2, tileSize * 2, tileSize * 2), new Rectangle(((DarkMage)characters[i]).miasmaFire.cropX * tileSize, ((DarkMage)characters[i]).miasmaFire.cropY * tileSize, tileSize, tileSize), ((DarkMage)characters[i]).miasmaFire.pColor); 
                }
                
                if( characters[i] is Player) 
                {
                    if (((Player)characters[i]).fireBall != null) 
                    {
                        _spriteBatch.Draw(mapTexture, new Rectangle(((Player)characters[i]).fireBall.X * tileSize * 2, (((Player)characters[i]).fireBall.Y + 5) * tileSize * 2, tileSize * 2, tileSize * 2), new Rectangle(((Player)characters[i]).fireBall.cropX * tileSize, ((Player)characters[i]).fireBall.cropY * tileSize, tileSize, tileSize), ((Player)characters[i]).fireBall.pColor);
                    }
                }
               
                
            }

            // Draw the enemies stats 
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
