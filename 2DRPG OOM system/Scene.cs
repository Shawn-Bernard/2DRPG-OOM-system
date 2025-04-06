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

    public void StartGame() 
    {
        Game1.currentScene = 1;
        goToFirstLevel(); 
    }      

    public void quitGame()
    {
        System.Environment.Exit(0);
    }

    public void resetLevel()
    {
        for (int i = 0; i < Game1.characters.Count; i++)
        {
            if (!(Game1.characters[i] is Player))
            {
                Game1.characters.Remove(Game1.characters[i]);
            }
        }

        Game1.itemsOnMap.Clear();
        Game1.tempPoints.Clear();

        ((Player)Game1.characters[0]).tilemap_PosX = 3;
        ((Player)Game1.characters[0]).tilemap_PosY = 3;
        ((Player)Game1.characters[0]).levelComplition = false;

        // generate another map based on random
        Game1.mString = Game1.tileMap.GenerateMapString(25, 10);
        Game1.tileMap.ConvertToMap(Game1.mString, Game1.tileMap.multidimensionalMap);
    }
    public void goToFirstLevel()
    {
        Game1.characters.Clear();
        Game1.placementManager.AddEnemiesLevel1();
        Game1.itemsOnMap.Clear();

        Game1.tileMap.LoadPremadeMap("LoadedMap1.txt");

        Game1.turnManager.resetTurns();

        for (int i = 0; i < 10; i++)
        {
            // Generating random points, to place the item later
            Game1.tempPoints.Add(Game1.placementManager.GetWalkablePoint(Game1.tileMap));
        }

        // Place each item in each point generated
        Game1.placementManager.initializeItems(Game1.itemsOnMap, Game1.tempPoints);
    }

    public void goToSecondLevel()
    {
        resetLevel();


        // Place new enemies at random positions
        Game1.placementManager.AddEnemiesLevel2();

        Game1.turnManager.resetTurns();

        // Place new items in the second map
        for (int i = 0; i < 5; i++)
        {
            Game1.tempPoints.Add(Game1.placementManager.GetWalkablePoint(Game1.tileMap));
        }

        Game1.itemsOnMap.Add(new Potion(Game1.tempPoints[0]));
        Game1.itemsOnMap.Add(new FireballScroll(Game1.tempPoints[1]));
        Game1.itemsOnMap.Add(new FireballScroll(Game1.tempPoints[2]));
        Game1.itemsOnMap.Add(new LightningScroll(Game1.tempPoints[3]));
        Game1.itemsOnMap.Add(new LightningScroll(Game1.tempPoints[4]));

    }

    public void goToThirdLevel()
    {
        resetLevel();

        Game1.placementManager.AddEnemiesLevel3();

        // Place new items in the second map
        for (int i = 0; i < 5; i++)
        {
            Game1.tempPoints.Add(Game1.placementManager.GetWalkablePoint(Game1.tileMap));
        }

        Game1.itemsOnMap.Add(new Potion(Game1.tempPoints[0]));
        Game1.itemsOnMap.Add(new FireballScroll(Game1.tempPoints[1]));
        Game1.itemsOnMap.Add(new FireballScroll(Game1.tempPoints[2]));
        Game1.itemsOnMap.Add(new LightningScroll(Game1.tempPoints[3]));
        Game1.itemsOnMap.Add(new LightningScroll(Game1.tempPoints[4]));

    }

    public void goToFourLevel()
    {
        resetLevel();

        Game1.placementManager.AddEnemiesBossLevel();
    }

    public void goToGameOver() 
    {
        Game1.currentScene = 6;
        Game1.itemsOnMap.Clear();
        Game1.tempPoints.Clear();
        Game1.characters.Clear();
    }
}

