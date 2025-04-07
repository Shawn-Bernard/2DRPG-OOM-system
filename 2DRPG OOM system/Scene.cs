using _2DRPG_OOM_system;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;



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
        // set the scene where the first level is
        Game1.currentScene = 1;
        goToFirstLevel(); 
    }      

    public void quitGame()
    {
        System.Environment.Exit(0);
    }

    public void goToMenu() 
    {
        // go to the scene where the menu is
        Game1.currentScene = 0; 
        ClearAllLists();
    }

    public void resetLevel()
    {
        // reset all the values when the player enters into a new level

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
        // Setting the first level

        Game1.characters.Clear();
        Game1.placementManager.AddEnemiesLevel1();
        Game1.itemsOnMap.Clear();

        Game1.tileMap.LoadPremadeMap("LoadedMap1.txt");   // obtaining the string from a text file
        Game1.tileMap.BorderWithWall();         

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
        // set the second level
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
        // sete the third level
        resetLevel();

        Game1.placementManager.AddEnemiesLevel3();

        Game1.turnManager.resetTurns();

        // Place new items in the third map
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
        // set the four level (boss level)
        resetLevel();

        Game1.placementManager.AddEnemiesBossLevel();        

        // place new items in the forth map (Boss level)
        for (int i = 0; i < 3; i++) 
        {
            Game1.tempPoints.Add(Game1.placementManager.GetWalkablePoint(Game1.tileMap));
        }

        Game1.itemsOnMap.Add(new Potion(Game1.tempPoints[0]));
        Game1.itemsOnMap.Add(new FireballScroll(Game1.tempPoints[1]));
        Game1.itemsOnMap.Add(new LightningScroll(Game1.tempPoints[2])); 
        
    }

    public void goToGameOver() 
    {
        // go to the scene where the game over is
        Game1.currentScene = 6;
        ClearAllLists(); 
    }

    public void ClearAllLists() 
    {
        // Clearing all the list when the game is restarted or ended
        Game1.itemsOnMap.Clear();
        Game1.tempPoints.Clear();
        Game1.characters.Clear();
    }
}

