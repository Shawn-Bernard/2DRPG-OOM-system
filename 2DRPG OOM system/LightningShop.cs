using _2DRPG_OOM_system;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

public class LightningShop  : Shop
{
    public LightningShop(Vector2 shopPos)
    {
        cropPosX = 4;
        cropPosY = 8;
        cost = 10;
        name = "Lightning Shop";
        description = $"Lightning scrolls cost ${cost}";
        shopPosition = shopPos;
        iColor = Color.White;
        item = new LightningScroll(shopPos);
    }
}
