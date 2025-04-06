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
        public Menu GameOver = new Menu(); 
        
        
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
            //tileMap.LoadPremadeMap("LoadedMap1.txt");
            //tileMap.ConvertToMap(mString, tileMap.multidimensionalMap);
            oldState = Keyboard.GetState();         
                               
            
            mainMenu.buttons.Add(new UIButton(button01, mySpriteFont, "Start Game", new Vector2(325, 200), 160, 64, UIButton.ButtonType.StartGame));
            mainMenu.textList.Add(new UIText("A Soldier 2D Mission", mySpriteFont, new Vector2(300, 100), Color.Black));
            mainMenu.buttons.Add(new UIButton(button01, mySpriteFont, "Quit", new Vector2(325, 300), 160, 64, UIButton.ButtonType.Exit));
            mainMenu.backgroundColor = Color.Lerp(Color.Gray, Color.Honeydew, 0.5f);
            EndScreen.textList.Add(new UIText("The End", mySpriteFont, new Vector2(Window.ClientBounds.Width/2 - 50, 200), Color.Black));
            EndScreen.textList.Add(new UIText("Congratulation, you won!", mySpriteFont, new Vector2(Window.ClientBounds.Width/2 - 125, 100), Color.Black));
            EndScreen.backgroundColor = Color.Lerp(Color.Gray, Color.Honeydew, 0.75f); 
            GameOver.textList.Add(new UIText("Game Over", mySpriteFont, new Vector2(Window.ClientBounds.Width / 2 - 50, 50), Color.White));
            GameOver.buttons.Add(new UIButton(button01, mySpriteFont, "Try Again", new Vector2(325, 200), 160, 64, UIButton.ButtonType.StartGame));
            GameOver.buttons.Add(new UIButton(button01, mySpriteFont, "Exit", new Vector2(325, 300), 160, 64, UIButton.ButtonType.Exit)); 
            GameOver.backgroundColor = Color.Lerp(Color.Black, Color.Honeydew, 0.25f); 

            GameScenes.Add(mainMenu);  
            GameScenes.Add(new Level(1));
            GameScenes.Add(new Level(2));
            GameScenes.Add(new Level(3));
            GameScenes.Add(new Level(4));
            GameScenes.Add(EndScreen);
            GameScenes.Add(GameOver);

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
