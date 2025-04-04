using _2DRPG_OOM_system;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Scene
{
    public Color backgroundColor; 

    

    public virtual void SceneUpdate(GameTime gameTime) 
    {

    }

    public virtual void DrawScene(GameTime gameTime, SpriteBatch _spriteBatch) 
    {

    }

    public void changeNextScene() 
    {
        Game1.currentScene++;

    }
}

