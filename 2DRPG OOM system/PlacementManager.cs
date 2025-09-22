using _2DRPG_OOM_system;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class PlacementManager
{
    public PlacementManager() 
    {
        isPlaced = false;
        pX = 0;
        pY = 0; 
    }

    private bool isPlaced;

    int pX;
    int pY;

    public Vector2 GetWalkablePoint(Tilemap tilemap) 
    {
        pX = 0;
        pY = 0;

        while (!isPlaced) 
        {
            // this generates a random coordinates (x,y)
            Random rnd1 = new Random();
            Random rnd2 = new Random();

            pX = rnd1.Next(0, tilemap.multidimensionalMap.GetLength(0));
            pY = rnd2.Next(0, tilemap.multidimensionalMap.GetLength(1));

            CheckPlacement(tilemap); 
        }

        isPlaced = false; 
        return new Vector2( pX, pY );
    }

    public void initializeItems(List<Item> listOfItems, List<Vector2> listOfVectors) 
    {        
        listOfItems.Add(new Potion(listOfVectors[0]));
        listOfItems.Add(new Potion(listOfVectors[1]));
        listOfItems.Add(new FireballScroll(listOfVectors[2]));
        listOfItems.Add(new LightningScroll(listOfVectors[3]));
        listOfItems.Add(new SacredPotion(listOfVectors[4]));
        listOfItems.Add(new LightningScroll(listOfVectors[5]));
        listOfItems.Add(new FireballScroll(listOfVectors[6]));
        listOfItems.Add(new Potion(listOfVectors[7]));
        listOfItems.Add(new LightningScroll(listOfVectors[8]));
        listOfItems.Add(new FireballScroll(listOfVectors[9]));
    }

    public void initializeItems(List<Item> listOfItems, List<Vector2> listOfVectors)
    {
        listOfItems.Add(new Potion(listOfVectors[0]));
        listOfItems.Add(new Potion(listOfVectors[1]));
        listOfItems.Add(new FireballScroll(listOfVectors[2]));
        listOfItems.Add(new LightningScroll(listOfVectors[3]));
        listOfItems.Add(new SacredPotion(listOfVectors[4]));
        listOfItems.Add(new LightningScroll(listOfVectors[5]));
        listOfItems.Add(new FireballScroll(listOfVectors[6]));
        listOfItems.Add(new Potion(listOfVectors[7]));
        listOfItems.Add(new LightningScroll(listOfVectors[8]));
        listOfItems.Add(new FireballScroll(listOfVectors[9]));
    }

    public void


    public void CheckPlacement(Tilemap tilemap) 
    {
        // Check if the generated coordinate is not in the same position as a wall or a non-walkable  tile
        if (!(tilemap.MapToChar(tilemap.multidimensionalMap, pX, pY) == '#') && !(tilemap.MapToChar(tilemap.multidimensionalMap, pX, pY) == '$'))
        {
            // Once checked, the loop ends and the coordinate is passed
            isPlaced = true;
        }
    }

    public void AddEnemiesLevel1() 
    {
        // Set up the player and enemies for the first level in a set position
        Game1.characters.Add(new Player(3, 3));
        Game1.characters.Add(new DarkMage(20, 6));
       
    }

    public void AddEnemiesLevel2() 
    {
        // Place two enemies in a random positions
        Vector2 tempVector;

        tempVector = GetWalkablePoint(Game1.tileMap); 
        Game1.characters.Add(new DarkMage((int)tempVector.X, (int)tempVector.Y));        

        tempVector = GetWalkablePoint(Game1.tileMap);
        Game1.characters.Add(new Ghost((int)tempVector.X, (int)tempVector.Y));

        Game1.turnManager.resetTurns();
    }

    public void AddEnemiesLevel3() 
    {
        // place three enemies in a random position

        Vector2 tempVector;

        tempVector = GetWalkablePoint(Game1.tileMap);
        Game1.characters.Add(new DarkMage((int)tempVector.X, (int)tempVector.Y));

        tempVector = GetWalkablePoint(Game1.tileMap);
        Game1.characters.Add(new Ghost((int)tempVector.X, (int)tempVector.Y));

        tempVector = GetWalkablePoint(Game1.tileMap);
        Game1.characters.Add(new Ghost((int)tempVector.X, (int)tempVector.Y));

        Game1.turnManager.resetTurns();
    }

    public void AddEnemiesBossLevel() 
    {
        // Adding the boss
        Game1.characters.Add(new Boss(16, 6));
        Game1.turnManager.resetTurns();
    }
}

