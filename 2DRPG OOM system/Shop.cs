using _2DRPG_OOM_system;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;

public class Shop
{
    public string name { get; set; }

    public string description { get; set; }

    public string poorText { get; set; }

    public int cost { get; set; }

    public Item item { get; set; }

    // This determine the coordinates to crop the texture image to get the correct sprite for the item
    public int cropPosX;
    public int cropPosY;

    public Vector2 shopPosition;
    public Vector2 shopBox = new Vector2 (0,0);
    protected Color iColor;

    public bool isShopOpen = false;
    public virtual Item getItemShop()
    {
        Debug.Write("Getting item from shop");
        return item;
    }

    public bool canAfford(int amountOfMoney)
    {
        if (amountOfMoney >= cost)
        {
            return true;
        }
        else return false;
    }

    public void DrawShop(SpriteBatch _spriteBatch)
    {
        // draw shops on map
        _spriteBatch.Draw(Game1.mapTexture, new Rectangle((int)shopPosition.X * Game1.tileSize * 2, ((int)shopPosition.Y + 5) * Game1.tileSize * 2, Game1.tileSize * 2, Game1.tileSize * 2), new Rectangle(cropPosX * Game1.tileSize, cropPosY * Game1.tileSize, Game1.tileSize, Game1.tileSize), iColor);
        if (isShopOpen)
        {
            _spriteBatch.DrawString(Game1.mySpriteFont, description, new Vector2(500, 130), Color.White);
        }
    }

}
