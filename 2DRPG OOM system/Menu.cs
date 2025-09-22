using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2DRPG_OOM_system;


public class Menu : Scene
{
    public List<UIButton> buttons = new List<UIButton>(); 

    public List<UIText> textList = new List<UIText>(); 

    public Menu()
    {
        Game1.GameMenus.Add(this);
    }

    public override void SceneUpdate(GameTime gameTime)
    {
        KeyboardState keyboardState = Keyboard.GetState();
        MouseState currentMouseState = Mouse.GetState();

        if (keyboardState.IsKeyDown(Keys.Q)) 
        {
            changeNextScene(); 
        }

        for (int i = 0; i < buttons.Count; i++) 
        {
            if (buttons[i].WithInBounds(currentMouseState)) 
            {
                buttons[i].buttonColor = Color.Gray;
                buttons[i].focused = true;
            }
            else 
            {
                buttons[i].buttonColor = Color.White;
                buttons[i].focused = false;
            }

            if(currentMouseState.LeftButton == ButtonState.Pressed && buttons[i].focused) 
            {
                DoButtonAction(buttons[i].Type.ToString()); 
            }           
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

    public void DoButtonAction(string actionString)
    {
        switch (actionString)
        {
            case "StartGame":
                StartGame();
                break;            
            case "Exit":
                quitGame(); 
                break;
            case "ToMenu":
                goToMenu();
                break;
        }
    }


}

