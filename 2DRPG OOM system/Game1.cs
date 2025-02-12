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
        private static Vector2 playerPos = new Vector2(100, 100); 
        private Texture2D mapTexture; 

        public Player myPlayer = new Player(50, 30, 50, 3, playerPos);

        public Sprite _playerSprite = new Sprite();

        private Dictionary<Vector2, int> tileMap;
        private List<Rectangle> textureStore;

        public int tileSize = 16;

        SpriteFont mySpriteFont; 

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            textureStore = new() { new Rectangle(0, 0, 8, 8) }; 
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            //Debug.Print(myPlayer._healthSystem.health.ToString());
            mapTexture = Content.Load<Texture2D>("The_Tilemap");
            _playerSprite._player = Content.Load<Texture2D>("The_Tilemap");
            _playerSprite.collider = new Rectangle((int)playerPos.X, (int)playerPos.Y, 32, 32);
            mySpriteFont = Content.Load<SpriteFont>("Font");
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

            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.A)) 
            {
                myPlayer.Movement(new Vector2(-4, 0)); 
            }

            if (keyboardState.IsKeyDown(Keys.D)) 
            {
                myPlayer.Movement(new Vector2(4, 0));
            }

            if (keyboardState.IsKeyDown(Keys.W)) 
            {
                myPlayer.Movement(new Vector2(0, -4));
            }

            if (keyboardState.IsKeyDown(Keys.S)) 
            {
                myPlayer.Movement(new Vector2(0, 4));
            }

                // TODO: Add your update logic here

                base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);            

            // TODO: Add your drawing code here

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            //_spriteBatch.Draw(mapTexture, myPlayer.position, Color.White); 

            for(int i = 0; i < 50; i++) 
            {
                for (int j = 5; j < 29; j++)
                {
                    _spriteBatch.Draw(mapTexture, new Rectangle(i * tileSize, j * tileSize, tileSize * 2, tileSize * 2), new Rectangle(1 * tileSize, 4 * tileSize, tileSize, tileSize), Color.White);
                }
            }

            _spriteBatch.Draw(_playerSprite._player, new Rectangle((int)myPlayer.position.X, (int)myPlayer.position.Y, tileSize * 2, tileSize * 2), new Rectangle(1 * tileSize, 8 * tileSize, tileSize, tileSize), Color.White);

            _spriteBatch.DrawString(mySpriteFont, "HP: " + myPlayer._healthSystem.health, new Vector2(0, 0), Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        
    }
}
