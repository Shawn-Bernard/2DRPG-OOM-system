using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


 public class UIText
{
    public UIText(string tText, SpriteFont tFont, Vector2 tPosition, Color tColor) 
    {
        Text = tText;
        textFont = tFont;
        textPosition = tPosition;
        textColor = tColor;
    }


    public string Text;
    public SpriteFont textFont;
    public Vector2 textPosition;
    public Color textColor;
        
    public void DrawText(GameTime gameTime, SpriteBatch _spriteBatch) 
    {
        _spriteBatch.DrawString(textFont, Text, textPosition, textColor); 
    }

}
