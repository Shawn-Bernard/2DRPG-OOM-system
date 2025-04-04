using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;


public class UIButton
{

    public UIButton(Texture2D bTexture, SpriteFont bFont, string bText, Vector2 bPosition, float bWidth, float bHeight) 
    {
        buttonTexture = bTexture; 
        textFont = bFont;
        buttonText = bText;
        buttonPosition = bPosition;
        width = bWidth;
        height = bHeight;
    }

    public Texture2D buttonTexture;
    public SpriteFont textFont; 
    public string buttonText;
    public bool focused = false;
    public Vector2 buttonPosition;
    public float width;
    public float height;

    public void DrawUI(GameTime gameTime, SpriteBatch _spritebatch) 
    {
        _spritebatch.Draw(buttonTexture, new Rectangle((int)buttonPosition.X, (int)buttonPosition.Y, (int)width, (int)height), Color.White);
        _spritebatch.DrawString(textFont, buttonText, new Vector2(buttonPosition.X + 20, buttonPosition.Y + height/3), Color.Black);
    }


    
}

