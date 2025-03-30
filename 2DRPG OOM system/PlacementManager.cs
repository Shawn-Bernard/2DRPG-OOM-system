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
    }

    private bool isPlaced; 


    public Vector2 GetWalkablePoint(Tilemap tilemap) 
    {
        int pX = 0;
        int pY = 0;

        while (!isPlaced) 
        {
            // this generates a random coordinates (x,y)
            Random rnd1 = new Random();
            Random rnd2 = new Random();

            pX = rnd1.Next(0, tilemap.multidimensionalMap.GetLength(0));
            pY = rnd2.Next(0, tilemap.multidimensionalMap.GetLength(1));

            // Check if the generated coordinate is not in the same position as a wall or a non-walkable  tile
            if (!(tilemap.MapToChar(tilemap.multidimensionalMap, pX, pY) == '#') && !(tilemap.MapToChar(tilemap.multidimensionalMap, pX, pY) == '$') )
            {
                // Once checked, the loop ends and the coordinate is passed
                isPlaced = true;
            }


        }

        isPlaced = false; 
        return new Vector2( pX, pY );
    }
}

