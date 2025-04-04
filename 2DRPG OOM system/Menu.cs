using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Menu : Scene
{
    public List<UIButton> buttons = new List<UIButton>(); 

    public List<UIText> textList = new List<UIText>();

    public SpriteFont txtFont;

    public override void SceneUpdate(GameTime gameTime)
    {
        KeyboardState keyboardState = Keyboard.GetState();

        if (keyboardState.IsKeyDown(Keys.Q)) 
        {
            changeNextScene(); 
        }
    }

    public override void DrawScene(GameTime gameTime, SpriteBatch _spriteBatch)
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].DrawUI(gameTime, _spriteBatch);

        }

        for (int i = 0; i < textList.Count; i++) 
        {
            textList[i].DrawText(gameTime, _spriteBatch);
        }

    }

   
}

