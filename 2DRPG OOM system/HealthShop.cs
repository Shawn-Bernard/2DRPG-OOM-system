using _2DRPG_OOM_system;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

public class HealthShop : Shop
{
    public HealthShop(Vector2 shopPos)
    {
        cropPosX = 3;
        cropPosY = 8;
        cost = 5;
        name = "Health Shop";
        description = $"Health potions cost ${cost}";
        shopPosition = shopPos;
        iColor = Color.White;
        item = new Potion(shopPos);
    }
}
