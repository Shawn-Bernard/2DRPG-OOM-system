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

        
        // This determine the size the tiles are going to have        
        public static int tileSize = 16;

        public static SpriteFont mySpriteFont;
        // Creating the Tilemap 
        public static Tilemap tileMap = new Tilemap();
        public static string mString;
        private KeyboardState oldState;

        // This string will contain the text from the text file
        private string pathToMyFile;
        public static string whosTurn = "";
       

        public static TurnManager turnManager = new TurnManager();
        public static List<Item> itemsOnMap = new List<Item>();

        public static List<Projectile> projectiles = new List<Projectile>();

        public static PlacementManager placementManager = new PlacementManager();
        public static List<Vector2> tempPoints = new List<Vector2>();

        public List<Scene> GameScenes = new List<Scene>();

      
        public static int maxNumLevel;
        public static int currentScene; 
        public Menu mainMenu = new Menu();
        public Texture2D button01;
        public Menu EndScreen = new Menu(); 
        
        
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
            currentScene = 0; 
            mapTexture = Content.Load<Texture2D>("The_Tilemap");            
            mySpriteFont = Content.Load<SpriteFont>("Font");
            button01 = Content.Load<Texture2D>("button_brown"); 
            //mString = tileMap.GenerateMapString(25, 10);
            tileMap.LoadPremadeMap("LoadedMap1.txt");
            //tileMap.ConvertToMap(mString, tileMap.multidimensionalMap);
            oldState = Keyboard.GetState();
            

            // Set up the player and enemies for the first level
            characters.Add(new Player(3, 3));
            characters.Add(new DarkMage(20, 6));
           
           
           

            for (int i = 0; i < 10; i++)
            {
                // Generating random points, to place the item later
                tempPoints.Add(placementManager.GetWalkablePoint(tileMap));
            }

            // Place each item in each point generated
            placementManager.initializeItems(itemsOnMap, tempPoints);

            mainMenu.buttons.Add(new UIButton(button01, mySpriteFont, "Start Game", new Vector2(325, 200), 160, 64));
            mainMenu.textList.Add(new UIText("A Soldier 2D Mission", mySpriteFont, new Vector2(300, 100), Color.Black));
            mainMenu.buttons.Add(new UIButton(button01, mySpriteFont, "Quit", new Vector2(325, 300), 160, 64));
            mainMenu.backgroundColor = Color.Lerp(Color.Gray, Color.Honeydew, 0.5f);
            EndScreen.textList.Add(new UIText("The End", mySpriteFont, new Vector2(Window.ClientBounds.Width/2 - 50, 200), Color.Black));
            EndScreen.textList.Add(new UIText("Congratulation, you won!", mySpriteFont, new Vector2(Window.ClientBounds.Width/2 - 125, 100), Color.Black));
            EndScreen.backgroundColor = Color.Lerp(Color.Gray, Color.Honeydew, 0.75f); 

            GameScenes.Add(mainMenu);            
            GameScenes.Add(new Level(1));
            GameScenes.Add(new Level(2));
            GameScenes.Add(new Level(3));
            GameScenes.Add(new Level(4));
            GameScenes.Add(EndScreen); 
            
            maxNumLevel = GameScenes.Count; 
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

            GameScenes[currentScene].SceneUpdate(gameTime);           
            
            

            base.Update(gameTime);
            
        }
               

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(GameScenes[currentScene].backgroundColor);
            //GraphicsDevice.Clear(Color.DarkBlue); 

            // TODO: Add your drawing code here

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);


            GameScenes[currentScene].DrawScene(gameTime, _spriteBatch); 

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        
    }
}
