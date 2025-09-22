using _2DRPG_OOM_system;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

public class FireballShop : Shop
{
    public FireballShop(Vector2 shopPos)
    {
        cropPosX = 0;
        cropPosY = 7;
        cost = 5;
        name = "Fireball Shop";
        description = $"Fireballs cost ${cost}";
        shopPosition = shopPos;
        iColor = Color.White;
        item = new FireballScroll(shopPos);
    }
}
