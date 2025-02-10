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

        public Player myPlayer = new Player(50, 30, 50, 3);

        private Dictionary<Vector2, int> tileMap;
        private List<Rectangle> textureStore; 

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
            mapTexture = Content.Load<Texture2D>("tilemap"); 
            
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

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();

            _spriteBatch.Draw(mapTexture, new Vector2(0, 0), Color.White); 
            

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        
    }
}
